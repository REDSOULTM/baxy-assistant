**DISEÑO DE ALTO NIVEL, EJECUCIÓN Y CIERRE DEL PROYECTO**

> Documento parcial del Informe Final del proyecto **Baxy**.
> Autor: Emmanuel Villacura Arancibia. Curso: INSW410 — Portafolio de Proyectos (UNAB).
> Profesor guía: Nicolás Caselli. Viña del Mar, Chile, 2026.
> Las cifras citadas en este documento provienen de mediciones reales registradas
> en el repositorio del proyecto (`documentacion/`, `MEMORY.md`, historial git) y
> no de estimaciones aspiracionales.

---

# 6. DISEÑO DE ALTO NIVEL

El diseño de alto nivel del sistema se documenta mediante una **adaptación del modelo
de vistas arquitectónicas "4+1" propuesto por Kruchten (1995)**, el cual describe la
arquitectura de un sistema de software a través de vistas concurrentes y
complementarias —en su forma canónica: Lógica, de Proceso, de Desarrollo y Física,
más la vista de Escenarios (+1)—, cada una orientada a un grupo distinto de
interesados. Dado que el producto se ejecuta sobre un **nodo único** (el equipo del
usuario, sin componentes distribuidos ni concurrencia entre nodos), esta sección
adapta el modelo y presenta cuatro vistas: **Lógica, Física, de Despliegue y de
Escenarios**; la vista de **Proceso** se subsume en las vistas Lógica (el *hot-path*
del turno) y de Despliegue (los procesos `llama-server`/`server`/UI), y la vista de
**Desarrollo** se refleja en la organización en sub-paquetes descrita en la vista
Lógica (`agent_core`, `routing`, `voice`, `computer_use_pkg`, `domain_tools/`). Se
reconoce explícitamente esta adaptación para no presentarla como el 4+1 estándar. La
arquitectura aquí descrita no
es de diseño teórico: fue **verificada leyendo el código y midiendo en vivo**
durante una auditoría de tres sesiones (rondas 1–13), según consta en
`documentacion/01_arquitectura/ARCHITECTURE.md`.

El norte rector de la arquitectura, ante cualquier conflicto de prioridades, es:
(1) preservación de invariantes y aislamiento del *blast radius* (radio de daño),
(2) fiabilidad —nunca caer el turno, perder una respuesta ni degradar en
silencio— y (3) latencia dentro del presupuesto tier-Alexa.

## 6.1. Vista Lógica (paquetes del agente)

La Vista Lógica describe la descomposición funcional del sistema en paquetes y la
relación entre ellos. El sistema se organiza en torno a un **hot-path del turno**
(el camino de ejecución de un comando del usuario) y un conjunto de subsistemas de
soporte. Los paquetes principales son los siguientes:

**a) `agent_core` (orquestación del turno).** Es el núcleo coordinador,
materializado en `agent.py` (clase `Gemma4Agent`). Su método `run_content()` actúa
como orquestador delgado que delega en tres fases bien delimitadas:
`_decide_turn` (elección de modo y enrutamiento), `_execute_turn` (llamada al
modelo y despacho de herramientas) y `_finalize_turn` (saneamiento y entrega de la
respuesta). Esta separación fue producto de un refactor (Sprint R1) que redujo
`run_content` de 2 193 a 22 líneas de código (LOC), conteniendo así el radio de
daño de cualquier cambio futuro.

**b) `routing` (enrutamiento semántico de intención).** Implementado en
`routing/planner.py` y módulos asociados. Determina qué subconjunto de herramientas
ofrecer al modelo de lenguaje en cada turno. Combina varias señales: clasificación
por *embeddings* multilingües (`sentence-transformers` MiniLM), un **encoder
fine-tuneado** específico, una **cabeza de abstención** (*abstain head*) que decide
cuándo NO ofrecer ninguna herramienta, cabezas por herramienta y *clustering*. El
enrutador alcanza un *holdout* en español de **0.9964** de exactitud y aplica un
tope de 5 herramientas por turno (`MAX_SELECTED_TOOLS=5`) como cota anti-crash. El
criterio de diseño es la **precisión** (ofrecer solo lo necesario), no el *recall*,
que ya está saturado.

**c) `voice` (canal de audio "siempre activo").** Comprende la cadena
`audio_io → vad → wake → pipeline → tts`: captura de audio por PortAudio,
detección de actividad de voz (VAD), detección de palabra de activación
(*wake-word* LiveKit), transcripción (STT por Whisper/Parakeet en CPU) y síntesis
de voz (TTS por Piper, también en CPU). El invariante clave es que el *callback* de
audio nunca bloquea (solo copia y encola), y que el *wake* corre diezmado
(~8 Hz en lugar de ~31 Hz) para reducir el uso de CPU en reposo en un ~74 % medido,
sin perder *recall*.

**d) `computer_use` (control del sistema operativo).** Implementado en el
sub-paquete `computer_use_pkg/`, ejecuta acciones sobre cualquier aplicación
mediante una **cascada de tres niveles**: automatización por accesibilidad
(UI Automation / UIA), reconocimiento óptico de caracteres (OCR) y, como último
recurso, visión. Garantiza honestidad estructural mediante un verificador
tri-estado (`confirmed` ∈ {True, False, None}), donde `None` (no medible) ≠ `False`
(falló), evitando que el agente afirme haber realizado una acción que no ocurrió.

**e) `tools` (herramientas de dominio).** Conjunto de más de sesenta herramientas
de dominio (67 esquemas únicos expuestos al modelo tras retirar `smart_home`) organizadas
en el sub-paquete `domain_tools/` (creado en el Sprint R2, con 30+ áreas
funcionales: navegador, apps, WhatsApp, Steam, multimedia, sistema, etc.). Cada
herramienta se despacha envuelta en `try/except` y dentro de `_run_with_timeout`,
de modo que una herramienta que falla o se cuelga se convierte en
`{ok:False, error}` sin abortar el turno (*blast radius* contenido).

**f) Subsistemas transversales.** `safety_pkg` (clasificador de confirmación,
verificadores, políticas de honestidad); la **capa de memoria / "Jarvis"**
(observador de ambiente + perfil de gustos del usuario, inspirado en *Generative
Agents* de Park et al., 2023); `infra` (gestor del servidor llama —
`LlamaServerManager` —, cliente HTTP, *tracing*, telemetría); y la **UI web**
(React/Vite servida por pywebview).

```
┌──────────────────────────────────────────────────────────────┐
│  SUPERFICIES DE ENTRADA: launcher · server (FastAPI) · CLI · MCP │
└───────────────┬────────────────────────────┬──────────────────┘
                │                             │
        ┌───────▼────────┐          ┌─────────▼──────────┐
        │  voice          │ comando  │  agent_core         │
        │ audio→vad→wake  │ (texto)  │  run_content        │
        │ →pipeline (STT) │ ───────► │  → routing → LLM    │
        └───────┬─────────┘          │  → tools / computer │
            TTS │ (Piper)            │    _use → verifiers │
                ▼                    └──┬───────────┬───────┘
            parlantes                   │           │
                              ┌──────────▼──┐  ┌─────▼────────┐
                              │ PERSISTENCIA │  │ DIAGNÓSTICOS  │
                              │ state/exp/   │  │ tracing/      │
                              │ memoria-Jarvis│ │ telemetry     │
                              └──────────────┘  └──────────────┘
```
*Figura 6.1. Vista Lógica: paquetes del agente y flujo del hot-path. Fuente:
Elaboración propia (2026), a partir de `ARCHITECTURE.md`.*

## 6.2. Vista Física (mapeo a hardware)

La Vista Física describe cómo los componentes lógicos se asignan a los recursos de
hardware de **un único nodo: el computador del usuario**. No existe nodo remoto:
todo el cómputo es local, lo que constituye la propuesta de valor central del
producto (privacidad y operación sin conexión).

El hardware *target* es una laptop o PC modesta con **GPU de 4 GB de VRAM**
(GTX 1650 o RTX 3050 4 GB) o, en su defecto, sin GPU dedicada (fallback a CPU). El
reparto de recursos, **medido y registrado** en
`documentacion/datos_crudos/vram_real_medida.csv`, es el siguiente:

| Recurso | Componentes asignados | Consumo medido |
|---|---|---|
| **GPU (VRAM)** | LLM (Gemma 4 E2B-Q4_K_M) + encoder de visión (mmproj, residente) + caché KV | **3 371 MiB (delta)** para E2B-Q4_K_M; visión ≈ 1,2 GB |
| **CPU** | STT (Whisper/Parakeet int8), TTS (Piper), enrutador (MiniLM), render de la UI (`--disable-gpu`) | 0 VRAM; ~30 tok/s en CPU de escritorio |
| **RAM** | Modelo mapeado con `mmap` (páginas evictables) + caché KV residente | Piso recomendado: 8 GB; costo fijo KV ≈ 0,5–1 GB |
| **Periféricos** | Micrófono (captura), cámara (control por gestos, opcional), parlante (TTS) | — |

El dato de selección de modelo es determinante: según la medición, **E2B-Q4_K_M
(3 371 MiB) entra completo en 4 GB**, mientras que **E4B-Q4_K_M (5 087 MiB) NO
entra** ni en su cuantización más baja viable (E4B-UD-IQ2_M = 4 057 MiB ya roza el
límite). Los modelos 26B/31B requieren 12–15 GB y quedan descartados de plano. Esta
restricción física es la que fuerza el uso de E2B y, por consiguiente, todo el
trabajo de *fine-tuning* y optimización descrito en la Iteración 3.

```
┌─────────────────────────── PC DEL USUARIO (nodo único) ───────────────────────┐
│                                                                                │
│  ┌──────────── GPU 4 GB VRAM ────────────┐   ┌──────────── CPU ─────────────┐  │
│  │ Gemma 4 E2B-Q4 (3,36 GB)              │   │ STT int8 · TTS Piper         │  │
│  │ + visión mmproj (~1,2 GB, residente)  │   │ enrutador MiniLM · UI render │  │
│  │ + caché KV                            │   └──────────────────────────────┘  │
│  └───────────────────────────────────────┘                                     │
│  ┌──────────── RAM (≥8 GB) ──────────────┐   Periféricos: micrófono, cámara,   │
│  │ modelo mmap + KV residente            │   parlante                          │
│  └───────────────────────────────────────┘                                     │
└────────────────────────────────────────────────────────────────────────────────┘
                          (sin nodo en la nube — 0 transmisión de datos)
```
*Figura 6.2. Vista Física: reparto de recursos en el nodo único. Fuente:
Elaboración propia (2026), datos de `vram_real_medida.csv`.*

## 6.3. Vista de Despliegue (instalación y ejecución)

La Vista de Despliegue describe cómo se instala y arranca el sistema en la máquina
del usuario. El producto se despliega como un conjunto de **procesos locales
coordinados**, sin instalación de servicios remotos:

1. **Proceso `llama-server`** (motor de inferencia). Es un binario de `llama.cpp`
   que sirve el modelo GGUF por HTTP en el puerto local `:8080`. Se gestiona desde
   `LlamaServerManager`, que selecciona un **perfil de VRAM** según el hardware:
   `vram4` (perfil por defecto, asume CUDA, `-ngl` completo) o `cpu` (`-ngl 0`, 0
   VRAM, visión desactivada) como *fallback*. El gestor garantiza que el proceso se
   detiene en `atexit` (en Windows el hijo de `Popen` no muere con el padre y
   retendría los ~3,4 GB de VRAM).

2. **Proceso de la UI** (`pywebview` + WebView2 renderizando React/Vite). Renderiza
   por CPU (`--disable-gpu`) para no competir por la GPU con el juego o el LLM.

3. **Entornos virtuales (venvs) aislados**, por incompatibilidad de dependencias:
   - `.venv` (Python 3.10): runtime del agente.
   - `.venv_livekit` (Python 3.11): *training* del wake-word LiveKit.
   - venv Python 3.12: *fine-tuning* con Unsloth.

   En tiempo de ejecución NO se importa el paquete `livekit`; los modelos ONNX se
   sirven con `onnxruntime` puro (verificado idéntico al oficial al 4.º decimal).

**Configuración de parámetros de estabilidad** (perfil `vram4`, todos medidos):
`flash-attn` **OFF** por defecto (evita el crash CUDA #22527 en *prompts* >10 K
tokens), contexto **12 288** tokens (piso mínimo para que entre el system prompt),
y throttle de ONNX Runtime (`scripts/_ort_throttle.py`) antes de cargar modelos en
*loops* pesados para evitar el congelamiento de la PC por *busy-wait*.

**Despliegue sin GPU:** el sistema opera sin GPU dedicada (el LLM cae al
perfil `cpu` y STT/TTS siempre corrieron en CPU), con **auto-default al perfil
`cpu`** cuando no se detecta GPU NVIDIA y el binario `llama-server` CPU/Vulkan
empaquetado, de modo que el producto se ejecuta tanto en equipos con NVIDIA de
4 GB como en cualquier laptop razonable.

```
┌──────────────────── Máquina Windows del usuario ─────────────────────┐
│                                                                      │
│  «process» UI (pywebview/WebView2, --disable-gpu)                    │
│        │  HTTP / IPC                                                  │
│  «process» server.py (FastAPI/uvicorn)  ──►  Gemma4Agent (runtime)   │
│        │  HTTP :8080                                                  │
│  «process» llama-server  ──── perfil {vram4 | cpu}  ──► GGUF E2B-Q4  │
│                                                                      │
│  venvs: .venv (3.10 runtime) · .venv_livekit (3.11) · 3.12 (FT)     │
└──────────────────────────────────────────────────────────────────────┘
```
*Figura 6.3. Vista de Despliegue: procesos y entornos. Fuente: Elaboración propia
(2026).*

## 6.4. Vista de Escenarios (casos de uso clave)

La Vista de Escenarios concreta las anteriores mediante casos de uso reales,
**validados en vivo** contra el agente y el LLM (regla del proyecto: ningún cambio
de comportamiento se da por listo sin probarlo contra el modelo real). Se presentan
tres escenarios representativos:

**Escenario 1 — Comando de voz simple ("subí el volumen").**
El usuario pronuncia la palabra de activación; el *wake-word* dispara la captura; el
STT transcribe en CPU; `run_content` elige el modo, el enrutador ofrece la
herramienta de audio, el LLM emite la llamada, el despacho ejecuta y un verificador
re-lee el estado del SO (pycaw) para confirmar el cambio; finalmente Piper sintetiza
la respuesta. Latencia medida ~2,2 s por acción tras el fix de caché del
*summary-pass*. Ejemplo de honestidad: el verificador de brillo re-lee el valor
post-ajuste (tri-estado) en vez de afirmar a ciegas.

**Escenario 2 — Computer-use ("abrí Spotify y poné rock" / "instalá DOOM").**
Caso de cadena de acciones o misión con objetivo. El enrutador discrimina entre
encadenar herramientas (`abre X y pon Y`) y una misión-con-objetivo (`ve a X y luego
a Y`, donde X/Y son destinos → `computer_use(goal)`). La ejecución usa la cascada
UIA → OCR → visión. Casos reales validados: envío real a un grupo de WhatsApp,
descarga de Terraria al 99 % vía Steam, y detección honesta de "sin espacio en
disco" para DOOM (no miente sobre el resultado).

**Escenario 3 — Accesibilidad (control manos-libres por voz y gestos).**
Para usuarios con movilidad reducida, el módulo `vision_input` permite controlar el
cursor y el clic mediante gestos de la cámara (MediaPipe). El agente actúa como
**orquestador de meta-acciones**: el usuario dicta una instrucción en lenguaje
natural ("escribí X en la app Y") y el agente la traduce a una acción de
computer-use. El *grounding* multilingüe (no listas de palabras clave hardcodeadas)
y las guardas de honestidad estructural permiten que funcione para hablantes de
distintos idiomas y acentos.

```
Usuario ──(voz/gesto)──► [wake] ──► [STT] ──► run_content
                                                  │
                          ┌───────────────────────┼────────────────────┐
                          ▼                        ▼                     ▼
                  comando simple            cadena/misión          accesibilidad
                  (audio.set_volume)     (computer_use goal)      (vision_input→cu)
                          │                        │                     │
                          └────────► verificador (tri-estado) ◄──────────┘
                                                  │
                                          [TTS Piper] ──► respuesta hablada
```
*Figura 6.4. Vista de Escenarios: los tres casos de uso clave. Fuente: Elaboración
propia (2026), casos validados en `MEMORY.md`.*

---

# 7. EJECUCIÓN DEL PROYECTO (3 ITERACIONES)

El desarrollo se ejecutó de forma **iterativa e incremental**, organizado en
sprints sucesivos con un método de trabajo dirigido por mediciones (*measure-then-
ship*): cada funcionalidad se mide contra un criterio de éxito definido de antemano
(un *gate*), se implementa de forma gateada (con bandera por defecto desactivada),
se valida en vivo y solo entonces se activa. Esta sección consolida los numerosos
sprints reales del historial del repositorio en **tres iteraciones** coherentes con
el ciclo de vida del producto.

## 7.1. Iteración 1 — Núcleo del agente: LLM local, herramientas y enrutador

**Meta del sprint.** Establecer un agente conversacional capaz de ejecutar un
modelo de lenguaje local dentro del presupuesto de 4 GB de VRAM y de despachar
acciones mediante un sistema de herramientas, con un enrutador que seleccione la
acción correcta.

**Funcionalidades entregadas.**
- Selección y carga del modelo: medición exhaustiva de VRAM por modelo y
  cuantización (`vram_real_medida.csv`), concluyendo que **Gemma 4 E2B-Q4_K_M es el
  único Gemma 4 que entra completo en ≤4 GB** (3 371 MiB).
- Sistema de más de sesenta herramientas de dominio (67 esquemas únicos expuestos al modelo tras
  retirar `smart_home`) organizado y aislado (`domain_tools/`).
- Enrutador semántico (`routing/planner.py`): clasificación por *embeddings*
  multilingües + encoder + cabeza de abstención, con tope de 5 herramientas.
- Hot-path del turno con *blast radius* contenido: cada herramienta envuelta en
  `try/except` + timeout por herramienta; *timeout* por modo en la llamada al LLM.

**Historias de usuario.**
- *Como usuario, quiero hablarle a mi PC sin depender de internet, para mantener mi
  privacidad.*
- *Como usuario, quiero que el asistente ejecute la acción correcta cuando le pido
  algo, para no tener que repetir el comando.*

**Evidencia.**
- Gate de VRAM: ≤4 GB cumplido (3,36 GB medido, registrado en CSV).
- Gate de enrutamiento: *holdout* en español **0.9964** de exactitud.
- Suite de tests creciendo desde 80 (baseline) hacia cientos de casos verdes.
- Arquitectura verificada por auditoría de 3 sesiones (27 bugs reales corregidos,
  todos en los **bordes** —persistencia, recovery, teardown— nunca en el hot-path).

**Hallazgos al cierre de la iteración.**
- *Tool-calling* de E2B en una exactitud estimada de ~75 % (vs ~91 % de E4B): trade-off aceptado
  por entrar en 4 GB, abordado con *fine-tuning* y *forced-retry* en la Iteración 3,
  donde se eliminó la principal patología de honestidad (0 % de herramientas
  inventadas en producción).

## 7.2. Iteración 2 — Voz, percepción y robustez: STT/TTS, wake-word y visión

**Meta del sprint.** Convertir el agente de texto en un asistente de **voz**
operativo manos-libres y dotarlo de **percepción** (visión y cámara), todo en CPU
para no consumir la VRAM reservada al LLM.

**Funcionalidades entregadas.**
- Canal de audio "siempre activo": captura (PortAudio) → VAD → *wake-word*
  (LiveKit, ONNX) → STT (Whisper/Parakeet int8 en CPU) → TTS (Piper en CPU).
- *Wake-word* diezmado a ~8 Hz (−74 % CPU en reposo, medido) sin pérdida de recall;
  *silence gate* contra alucinaciones de Whisper sobre silencio.
- Computer-use con cascada UIA → OCR → visión y verificación tri-estado honesta.
- Módulo de visión / control por cámara y gestos (`vision_input`, MediaPipe) para
  accesibilidad; gestos llevados a 2D (la coordenada z de MediaPipe era ruidosa) y
  *pinch* exigente para reducir falsos clics de 14 a 0.

**Historias de usuario.**
- *Como usuario, quiero activar al asistente con una palabra y dictarle, para usarlo
  con las manos ocupadas.*
- *Como usuario con movilidad reducida, quiero controlar el cursor con gestos de la
  cámara, para operar el PC sin teclado ni ratón.*

**Evidencia.**
- Gate de *wake-word* **cumplido** (`recall ≥ 0.60 ∧ fp/hr ≤ 1.0`) sobre un *held-out*
  universal y diverso (25 voces, 13 idiomas, multi-acento), no sobre la voz del
  operador: la palabra de activación "Baxy" alcanzó un **recall de 0,872** (por encima
  del umbral de 0,60), con **1,00 en español, inglés, italiano, francés, polaco y ruso**.
  La voz extremo a extremo quedó implementada y operativa offline (captura → VAD →
  wake → STT → TTS), validada en uso real.
- Causa raíz documentada del falso "el modelo no sirve" (score ~0.002): *padding* al
  final en vez de al inicio del *window* de 2 s — corregido.
- Gestos: precisión de palma 0,928 → 0,992; falsos clics 14 → 0 (fixture
  reproducible Apache-2.0).
- Validación en vivo de casos de voz (email, wifi, brillo, clima, navegación,
  lectura/respuesta de mensajes, alarmas): 10/10 PASS contra el agente real.

**Hallazgos al cierre de la iteración.**
- VoxCPM segfaulta en GPUs RTX 40-series (Ada Lovelace) durante el *warm-up* →
  resuelto adoptando Piper VITS para TTS de *training*.
- Casos de envío a destinatario equivocado en WhatsApp (verificador que se
  auto-engañaba) — corregidos y validados con envío real.

## 7.3. Iteración 3 — Fine-tuning E2B, optimización 4 GB y accesibilidad

**Meta del sprint.** Cerrar la brecha de calidad del modelo pequeño mediante
*fine-tuning*, optimizar la latencia y la estabilidad dentro de 4 GB, y consolidar
los modos de accesibilidad y la capa de memoria.

**Funcionalidades entregadas.**
- **Fine-tuning de Gemma 4 E2B** (Unsloth, Q4): pivote deliberado E4B → E2B (E4B de
  5 GB no entra en 4 GB; E2B-FT Q4 ocupa 3,2 GB en disco y **2,07 GB de VRAM** según la
  medición del despliegue del modelo fine-tuneado v2 —`dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`—,
  sí entra). Auditoría del dataset al 100 % antes de entrenar tras un bug de
  serialización de 393 ejemplos.
- **Estabilidad CUDA #22527 eliminada** con 6 palancas medidas (flash-attn OFF,
  ctx-checkpoints 0, circuit breaker OFF correcto, core-rules 6, cap=5).
- **Optimización de latencia**: caché de prefijo en el *summary-pass*
  (`keep-tools`, −~1 s/acción), reglas *lean* del prompt (p50 7,56 vs 10,55 s),
  *streaming* TTS por defecto, *forced-retry* con tope de tokens. p50 global medido
  ~1,22 s (presupuesto 4–5 s).
- **Capa de memoria "Jarvis"**: observador de ambiente (metadata, no contenido) +
  perfil de gustos determinista + inferencia de gustos finos por el propio LLM
  local, con guardas de privacidad (inspect/forget por *embeddings*).
- **Fallback a CPU** gateado (perfil `cpu`, 0 VRAM, auto-switch cuando un juego
  satura la GPU) y **UI web "Baxy field"** que renderiza por CPU.

**Historias de usuario.**
- *Como usuario, quiero que el asistente sea rápido (respuesta en pocos segundos),
  para que la conversación se sienta natural.*
- *Como usuario, quiero que recuerde mis gustos sin enviar mis datos a la nube, para
  recibir un trato personalizado y privado.*
- *Como usuario sin GPU dedicada, quiero poder ejecutar el asistente en mi laptop,
  para no quedar excluido.*

**Evidencia.**
- Gate de *fine-tuning*: **0 % de herramientas inventadas en producción** (medido
  con el array de tools; sin él, el artefacto mide 47 % — NUNCA medir sin array).
- Gate de latencia: ≤5 s por turno cumplido (p50 ~1,22 s; cola de Spotify reducida
  de 12,6 a 5,6 s).
- Suite de tests: hasta **2 740 tests verdes, 0 fallos** en el cierre de la rama
  Dev (2026-06-02).
- VRAM del modelo fine-tuneado: 2,07 GB (medición del despliegue del FT v2,
  `dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`; el CSV de VRAM cubre los modelos
  base, no el FT), holgura confirmada dentro de 4 GB.

**Estado al cierre de la iteración.**
- Soporte sin GPU *out-of-the-box* operativo: el producto se ejecuta en el perfil
  `cpu` con el binario CPU/Vulkan empaquetado y auto-default sin NVIDIA.
- Soporte multilingüe implementado y operativo (enrutamiento por *embeddings*
  multilingües y *grounding* multilingüe, sin listas de palabras clave hardcodeadas).
- Cliente MCP construido, cableado e integrado en el sistema (gateado por bandera).

---

# 8. TRABAJOS FUTUROS

Sobre el producto terminado, las siguientes mejoras están identificadas,
priorizadas y documentadas en el `BACKLOG_MAESTRO.md` (fuente única de verdad,
verificada contra el código real). Se presentan como evolución hacia una versión
2.0, distinguiendo lo que amplía el mercado de lo que profundiza la calidad:

1. **Optimización del soporte sin GPU dedicada (build Vulkan).** El producto ya se
   ejecuta sin NVIDIA en el perfil `cpu` (`-ngl 0`) con binario empaquetado y
   auto-default. La evolución prevista es un build **Vulkan**, que aprovecha GPUs
   integradas Intel/AMD para acelerar la inferencia respecto al modo CPU puro.
   Caveats honestos del modo CPU: ~12–20 tok/s estimado en laptop y sin visión.

2. **Ampliación de la evaluación multilingüe.** El sistema es multi-idioma y opera
   con enrutamiento por *embeddings* multilingües; el corpus de evaluación
   cuantitativa es actualmente ~98 % español. La evolución prevista amplía esa
   medición: construir un corpus de evaluación multilingüe (B2) → reentrenar el
   encoder del enrutador (~12 h de GPU RTX 4060 Ti, B1) → calibración de la cabeza
   de abstención por idioma (B4, sobre la infraestructura `thresholds_by_lang` ya
   integrada). Es un frente de ampliación conocido, no deuda oculta.

3. **Activación del cliente MCP (Model Context Protocol).** El cliente está
   construido, cableado e integrado en el sistema (gateado, `GEMMA4_MCP=0`). La
   evolución prevista es habilitarlo por defecto tras declarar servidores en
   `~/.gemma4/mcp_servers.json` y **validar en vivo que un catálogo grande de
   herramientas no degrade el tool-calling del modelo de 2B**.

4. **Migración a E4B cuando haya más de 4 GB de VRAM.** Para usuarios con mejor
   hardware, conmutar a Gemma 4 E4B elevaría el *tool-calling* de un estimado ~75 % a ~91 %.

5. **Mejoras de calidad de menor ROI ya identificadas:** *anti-primacy ordering*,
   *few-shot* específico por turno, mmproj-Q8 (bloqueado upstream, llama.cpp#18881),
   y técnicas de decodificación más rápida (MTP) cuando lleguen a llama.cpp.

---

# 9. CONCLUSIONES

Conforme a la metodología del curso, se presenta una conclusión por cada objetivo
específico, contrastada contra su criterio de éxito, más una conclusión académica de
lecciones aprendidas.

**Conclusión 1 (operación en 4 GB de VRAM — CUMPLIDO).** El objetivo de ejecutar el
LLM, la visión y la voz dentro de 4 GB de VRAM se cumplió de forma medida. Conviene
distinguir dos cifras que corresponden a artefactos distintos: el **barrido de VRAM
sobre los modelos base** de la familia Gemma 4 midió que **Gemma 4 E2B-Q4_K_M base
ocupa 3.371 MiB (≈3,36 GB)** —dato que decidió objetivamente el pivote E4B → E2B—,
mientras que el **modelo fine-tuneado finalmente desplegado consume ≈2,07 GB de VRAM**,
con aún más holgura dentro de los 4 GB junto con la visión residente y la caché KV. La
decisión de modelo no fue intuitiva sino producto de medir 21 combinaciones de
modelo/cuantización, lo que descartó objetivamente E4B (5,09 GB) y los modelos
26B/31B.

**Conclusión 2 (tool-calling fiable en un modelo de 2B — CUMPLIDO con matiz
honesto).** El *fine-tuning* de E2B alcanzó el gate de **0 % de herramientas
inventadas en producción**, eliminando la principal patología de honestidad. La
exactitud de selección de acción se estima en ~75 % (vs ~91 % del modelo grande),
*trade-off* aceptado y compensado en producción por el mecanismo de *forced-retry*
y las guardas estructurales. Es una limitación declarada con transparencia, no
ocultada.

**Conclusión 3 (latencia tier-Alexa — CUMPLIDO).** Tras la optimización por caché de
prefijo, reglas *lean* y *streaming* de TTS, la latencia se ubicó holgadamente dentro
del presupuesto de 4–5 s: la **mediana global por turno (p50) ≈ 1,22 s** y las **acciones
con invocación de herramienta ≈ 2,2 s** (sobre el *replay* de 1.071 mensajes reales, el
p50 fue ≈ 2,5 s). Las colas problemáticas (Spotify) se mantuvieron por debajo del umbral
catastrófico de 8 s (reducidas de 12,6 s a 5,6 s).

**Conclusión 4 (operación local, privada y honesta — CUMPLIDO).** El sistema opera
100 % offline, sin transmitir audio ni datos a la nube, y garantiza honestidad
estructural mediante verificadores tri-estado que distinguen "no medible" de "falló"
y guardas que impiden afirmar efectos físicos no verificados. Esto satisface tanto
el requisito de privacidad como el principio de que el asistente nunca debe mentir
sobre lo que hizo.

**Conclusión académica (lecciones aprendidas).** La lección metodológica central es
el valor del principio **"medir, no celebrar"**: ningún resultado se dio por bueno
sin un número contra un gate definido de antemano, y los fallos se reportaron en
crudo. Este rigor permitió descubrir causas raíz contraintuitivas (el *padding* del
wake-word, el *busy-wait* de ONNX, la serialización corrupta del dataset) que un
enfoque de "parchear el síntoma" habría enmascarado. La arquitectura por capas con
*blast radius* contenido demostró que los bugs reales viven en los **bordes**
(recovery, teardown, persistencia), no en el camino feliz —un hallazgo que orienta
dónde invertir esfuerzo de testing en proyectos futuros.

---

> **Nota sobre fuentes.** Las referencias bibliográficas completas (cards de los
> modelos Gemma 4, Whisper, LiveKit, Piper, MediaPipe; *Generative Agents* de Park
> et al., 2023; el modelo 4+1 de Kruchten, 1995; benchmarks WindowsAgentArena/OSWorld;
> y la legislación chilena aplicable) se consolidan en la sección de Bibliografía
> (APA 7) del Informe Final. Los datos cuantitativos de este capítulo provienen de
> `documentacion/datos_crudos/vram_real_medida.csv`, `documentacion/01_arquitectura/`,
> `documentacion/_backlog/BACKLOG_MAESTRO.md` y `MEMORY.md`.
