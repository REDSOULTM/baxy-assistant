# Artefactos sueltos — Tesis "Baxy" (UNAB INSW410)

> Documento de **artefactos independientes** solicitados clase a clase por el profesor.
> Cada sección es autocontenida y está lista para anexarse al Portafolio o al Informe
> Final. Los datos provienen de mediciones reales del repositorio del proyecto
> (`documentacion/00_producto/{ANALISIS_COMPETENCIA, AUDITORIA_licencias}.md`,
> `vram_real_medida.csv`, Backlog Maestro y memoria persistente); no se inventan cifras.
> Redacción en español académico formal, tercera persona, normas APA 7.
>
> Estudiante: Emmanuel Villacura Arancibia. Profesor guía: Nicolás Caselli. Santiago de Chile, 2026.

**Contenido**

1. [Tabla comparativa de competencia](#1-tabla-comparativa-de-competencia)
2. [Análisis legal](#2-análisis-legal-privacidad-y-licencias)
3. [Matriz de riesgos y mitigaciones](#3-matriz-de-riesgos-y-mitigaciones)
4. [Técnicas de análisis del problema (Ishikawa + Árbol de problemas / Pareto)](#4-técnicas-de-análisis-del-problema)
5. [Modelo de negocio CANVAS](#5-modelo-de-negocio-canvas)

---

## 1. Tabla comparativa de competencia

El análisis de competencia examinó **diez soluciones del estado del arte** (Agent-S,
OS-Copilot, Open Interpreter, OpenHands, Goose, Mark-XXXIX, openclaw, AutoGen, LangGraph
y AutoGPT), además de los asistentes comerciales dominantes (Amazon Alexa, Apple Siri y
Google Assistant), contrastándolas con la solución propuesta —Baxy—
sobre los criterios que constituyen las restricciones duras del proyecto. La conclusión
medida es categórica: **ninguna solución del estado del arte satisface de forma conjunta
el conjunto de restricciones** (local, privado, gratuito, voz, multilingüe, accesible,
operable en 4 GB de VRAM con capacidad de *computer-use*). Las soluciones comerciales son
de nube y de pago; las soluciones de agentes son de nube, dependen de modelos grandes o
son frameworks de orquestación que no corren en hardware modesto.

### 1.1. Tabla de homologación frente al estado del arte

La siguiente tabla de homologación compara las categorías de soluciones según los
criterios diferenciadores del proyecto. La notación es: ✓ cumple, ✗ no cumple, ◐ cumple
parcialmente, n/a no aplica.

**Tabla 1.1. Homologación de la solución propuesta frente a la competencia.**

| Criterio | Asistentes comerciales (Alexa / Siri / Google) | Agentes *computer-use* en la nube (Agent-S, OS-Copilot, OpenHands) | Asistente de voz local comparable (Mark-XXXIX) | Agentes locales / frameworks (Goose, AutoGPT, AutoGen, LangGraph, Open Interpreter) | **Baxy (propuesta)** |
|---|---|---|---|---|---|
| **Local / privado** (procesa todo en el equipo) | ✗ Audio y datos enviados a servidores | ✗ Nube (GPT-4V, UI-TARS) | ✗ Depende de Gemini LiveAPI en la nube | ◐ Algunos soportan modelos locales, pero orientados a desarrollo | ✓ 100 % local, cero datos a la nube |
| **Costo de operación** | ✗ Suscripción y/o APIs de pago | ✗ APIs de pago (GPT-4V) | ✗ Gemini LiveAPI de pago | ◐ Gratis solo si se usa modelo local; muchos asumen API de pago | ✓ USD 0 (modelo y stack OSS) |
| **Cabe en 4 GB de VRAM** | n/a (servidores remotos) | ✗ Requiere GPU grande o nube | ✗ Cómputo en la nube | ✗ Asumen modelos grandes o GPU ≥ 6–8 GB | ✓ 3,36 GB medido (E2B-Q4_K_M) |
| **Interacción por voz** (wake-word + STT + TTS) | ✓ Voz nativa | ✗ Texto / agentes de GUI | ✓ Voz (Gemini, latencia ~100 ms) | ✗ Texto / agentes de desarrollo | ✓ Voz local (wake-word LiveKit + STT CPU + Piper TTS) |
| **Multilingüe / multi-acento** | ◐ Multilingüe pero anglocéntrico | ✗ Algunos con *hardcodeo* de idioma (OS-Copilot: chino) | ◐ Según el modelo cloud | ✗ Mayoritariamente inglés-céntricos | ✓ Por *embeddings* multilingües, no listas por idioma |
| **Accesibilidad** (modos por voz/gestos) | ◐ Comandos de voz básicos | ✗ No es su foco | ✗ No es su foco | ✗ No es su foco | ✓ Modos `normal` / `no_vidente` / `movilidad` + control por cámara/gestos |
| **Computer-use** (operar el SO / la GUI) | ◐ Limitado a integraciones | ✓ Capacidad principal (OSWorld 72 %) | ◐ Vía agente de desarrollo | ◐ Orientado a software/código | ✓ Cascada UIA → OCR → visión con verificación por estado del SO |
| **Honestidad estructural** (no declara acciones no ejecutadas) | ✗ No verificable por el usuario | ✗ *LLM-judge* propenso a alucinar | ✗ *LLM-judge* | ✗ No es su foco | ✓ Verificación tristado por estado real del SO (registry/UIA/pycaw) |
| **Funciona sin internet** | ✗ Requiere conexión | ✗ Requiere conexión | ✗ Requiere conexión | ◐ Solo con modelo local | ✓ Operación *offline* completa |

*Fuente: Elaboración propia (2026), a partir de `documentacion/00_producto/ANALISIS_COMPETENCIA.md`
(análisis de 10 competidores mediante lectura de su código) y `vram_real_medida.csv`.*

### 1.2. Panorama de competidores analizados

A modo de contexto, la siguiente tabla resume qué es cada competidor y qué modelo o
recursos asume, evidenciando por qué ninguno es un rival directo en el nicho del proyecto.

**Tabla 1.2. Caracterización de los competidores del estado del arte.**

| Competidor | Qué es | Modelo / recursos | ¿Rival directo? |
|---|---|---|---|
| Agent-S | *Computer-use* / GUI (OSWorld 72 %) | GPT-4V / UI-TARS, **nube** | No (nube, modelo grande) |
| OS-Copilot | Automatización del SO + auto-aprendizaje | GPT, **nube**, *hardcodeo* en chino | No (nube, monolingüe) |
| Open Interpreter | Ejecuta código en lenguaje natural | Claude / LiteLLM (soporta local) | No (orientado a código) |
| OpenHands | Agente de software empresarial | Anthropic / OpenAI, **nube/Docker** | No (nube, enterprise) |
| Goose | Agente local en Rust + MCP | 50+ proveedores, llama-cpp-2 nativo | No (agente de desarrollo) |
| **Mark-XXXIX** | Asistente de voz tipo JARVIS (el más parecido) | **Gemini 2.5 LiveAPI, nube / de pago** | No (nube de pago) |
| openclaw | Gateway multicanal (WhatsApp, etc.) | TypeScript, agnóstico, **infraestructura de producción** | No (gateway, no asistente) |
| AutoGen | Multiagente conversacional | Framework, N modelos grandes | No (framework, modelos grandes) |
| LangGraph | Máquina de estados / DAG de agentes | Framework | No (framework) |
| AutoGPT | Bucle autónomo + memoria episódica | Framework | No (framework) |

*Fuente: Elaboración propia (2026), `documentacion/00_producto/ANALISIS_COMPETENCIA.md`.*

### 1.3. Interpretación

El cruce de criterios confirma que el nicho de la solución propuesta —**local, 4 GB de
VRAM, voz, privado, gratuito, multilingüe y honesto**— está vacío en el estado del arte.
La única solución comparable orientada a voz personal (Mark-XXXIX) depende de la API en la
nube Gemini LiveAPI, de pago, lo que viola simultáneamente las restricciones de privacidad,
costo y operación *offline*. En consecuencia, la estrategia competitiva del proyecto no
consiste en superar a los modelos grandes de la nube en capacidad bruta, sino en ser el
**mejor asistente de voz local, privado y gratuito que opera en la máquina del usuario**,
posición en la que no enfrenta competencia directa.

---

## 2. Análisis legal (privacidad y licencias)

El análisis legal del proyecto aborda tres dimensiones: (a) la **legislación chilena de
privacidad y ciberseguridad** aplicable y por qué la arquitectura del sistema minimiza
estructuralmente el riesgo regulatorio; (b) la **auditoría de licencias de software libre**
del stack tecnológico y su implicancia para la comercialización; y (c) la conclusión de
**vendibilidad** del producto.

### 2.1. Marco legal chileno aplicable a la privacidad

El sistema procesa datos potencialmente sensibles —audio de voz (dato biométrico),
contenido de pantalla y acciones del usuario—. La diferencia fundamental respecto de los
asistentes comerciales es que **Baxy no transmite estos datos fuera del equipo del
usuario**: todo el procesamiento ocurre localmente. Esta decisión de diseño tiene un efecto
jurídico directo de minimización del riesgo frente a la normativa nacional vigente.

**Tabla 2.1. Legislación chilena aplicable y cumplimiento estructural del proyecto.**

| Ley | Materia | Relevancia para el proyecto | Cumplimiento estructural |
|---|---|---|---|
| **Ley N.º 21.719** | Protección de Datos Personales (nueva ley general, crea la Agencia de Protección de Datos Personales) | El audio de voz constituye dato personal y, en cuanto rasgo biométrico, dato sensible | Al no transmitir ni almacenar datos en servidores de terceros, se evita el grueso de las obligaciones de tratamiento, transferencia y consentimiento aplicables al modelo en la nube |
| **Ley N.º 19.628** | Protección de la Vida Privada (tratamiento de datos de carácter personal) | Regula el almacenamiento y la cesión de datos personales | El procesamiento local sin cesión a terceros reduce la superficie regulatoria a su mínima expresión |
| **Ley N.º 21.459** | Delitos Informáticos (acceso ilícito, interceptación, daño a sistemas) | Un asistente que opera el SO debe evitar acciones no autorizadas o dañinas | La honestidad estructural y la política de confirmación para acciones de riesgo (p. ej., envío de mensajes) acotan el riesgo de operaciones indebidas |
| **Ley N.º 21.663** | Marco de Ciberseguridad e Infraestructura Crítica de la Información | Estándar de seguridad para software que maneja información | La ausencia de exfiltración de datos a la nube y el procesamiento *offline* reducen la exposición a vectores de ataque sobre datos en tránsito |

*Fuente: Elaboración propia (2026), a partir de la legislación chilena vigente y de la
arquitectura del sistema documentada en `documentacion/00_producto/`.*

**Conclusión de privacidad.** El enfoque *privacy-by-design* del proyecto —procesamiento
íntegramente local, cero transmisión de audio o pantalla— hace que el sistema sea
**favorable frente a las cuatro leyes** citadas: al no recolectar, transferir ni almacenar
datos personales en infraestructura ajena, el sistema minimiza estructuralmente el riesgo
regulatorio, en contraste directo con el modelo de los asistentes comerciales que envían el
audio del usuario a servidores remotos.

### 2.2. Auditoría de licencias del stack

La auditoría de licencias se ejecutó de forma reproducible sobre la totalidad del entorno
de desarrollo (`scripts/_diag/_license_audit.py`), abarcando **511 paquetes**. Para evaluar
la viabilidad comercial, lo relevante no es el entorno de desarrollo completo (que incluye
dependencias de experimentos), sino la **clausura efectivamente distribuida**, es decir, lo
que el código de producción (`gemma4_agent/`) realmente importa.

**Tabla 2.2. Resumen del inventario de licencias (511 paquetes auditados).**

| Categoría de licencia | Cantidad | Implicancia comercial |
|---|---|---|
| ✅ Permisiva (Apache / MIT / BSD) | 385 | Sin restricción; vendible |
| ❓ A revisar (metadata ambigua) | 104 | Mayoritariamente permisivas conocidas (pydantic=MIT, scikit-learn=BSD, etc.) |
| ⚠️ LGPL | 8 | Aceptable por enlace dinámico, sin modificar la biblioteca |
| 🚫 GPL | 8 | Bloquean si se enlazan directamente |
| 🚫 No comercial | 5 | Bloquean si se distribuyen (libs de Intel/NVIDIA; redistribuibles con atribución) |
| ✅ GPL con excepción | 1 | `pyinstaller` (excepción de bootloader: el `.exe` puede ser propietario) |
| 🚫 AGPL | 0 | Ninguna (la peor categoría está ausente) |

*Fuente: Elaboración propia (2026), `documentacion/00_producto/AUDITORIA_licencias.md`,
generado por `scripts/_diag/_license_audit.py`.*

#### 2.2.1. Único bloqueante real

El cruce de las licencias con el código importado en producción identificó **un solo
bloqueante real** para una comercialización propietaria:

- **`piper-tts` 1.4.2 (motor de síntesis de voz, TTS):** se importa directamente en
  `voice/tts.py` (`from piper import PiperVoice`), por lo que queda enlazado. Su metadata
  declara **GPL-3.0-or-later** (el *rewrite* `piper1-gpl`; documentación antigua que lo
  indicaba como MIT está desactualizada, pues correspondía al Piper de rhasspy). Al estar
  enlazado, una licencia GPL obligaría a abrir el código del producto vendido.
  - **Soluciones de bajo costo:** (a) aislar Piper como **subproceso** (ejecutar el binario
    `piper` por CLI = agregación, no obra derivada, GPL compatible con producto propietario);
    (b) **reemplazar** por un TTS de licencia permisiva (Kokoro = Apache; Piper antiguo =
    MIT); o (c) liberar el producto como código abierto. Se recomiendan (a) o (b).

#### 2.2.2. Falsos bloqueantes (GPL/no comercial que no aplican)

Varias dependencias GPL o no comerciales aparecen en el inventario pero **no se distribuyen**
porque no son importadas por el código de producción (son transitivas muertas de
experimentos previos, p. ej., de coqui-TTS): `encodec` (CC-BY-NC), `pedalboard` (GPL),
`Unidecode`, `pysrt`, `num2words`, y las dependencias de PyAutoGUI (`MouseInfo`, `PyMsgBox`,
GPL), dado que producción usa `SendInput`/win32 y no PyAutoGUI. Las bibliotecas de Intel
(`mkl`, `tbb`, `intel-openmp`) y NVIDIA (`cuda-nvrtc`) son runtime redistribuibles con
atribución. Al construir el producto con la **clausura mínima** (no el entorno de desarrollo
de 511 paquetes), estos componentes no se incluyen.

#### 2.2.3. Licencias de los modelos (fuera del alcance de pip)

| Modelo | Función | Licencia |
|---|---|---|
| **Gemma 4** (E2B) | LLM principal | Apache-2.0 ✅ |
| **mmproj** | Visión (parte de Gemma) | Apache-2.0 ✅ |
| **Whisper** | Reconocimiento de voz (STT) | MIT ✅ (verificar el checkpoint exacto) |
| **Piper VITS** | Síntesis de voz (TTS) | Motor MIT; **cada voz tiene su licencia** (algunas CC-BY / no comercial): verificar la voz distribuida |
| **Wake-word (LiveKit)** | Detección de palabra de activación | Apache-2.0 ✅ |
| **Encoder del router** (sentence-transformers FT) | Enrutamiento de herramientas | Apache-2.0 (base) ✅ |

*Fuente: Elaboración propia (2026), `documentacion/00_producto/AUDITORIA_licencias.md`.*

### 2.3. Conclusión: por qué es vendible

El **stack núcleo es de licencia permisiva y, por tanto, comercializable**: Gemma 4
(Apache-2.0), MediaPipe, OpenCV, Whisper (MIT), llama.cpp, Tesseract y sentence-transformers
son todos permisivos. La única salvedad —`piper-tts` GPL— tiene una solución conocida y de
costo bajo (aislamiento por subproceso o reemplazo por un TTS Apache/MIT). En cuanto a
privacidad, el procesamiento local hace al producto **favorable** frente a las leyes
chilenas N.º 21.719, 19.628, 21.459 y 21.663. La ruta de comercialización es, por tanto,
clara: (1) resolver `piper-tts`; (2) construir con la clausura mínima de licencias; y (3)
acompañar con `pip-licenses` sobre el build y un archivo `THIRD_PARTY_LICENSES.md` con las
atribuciones.

---

## 3. Matriz de riesgos y mitigaciones

La gestión de riesgos se realiza como **análisis a priori en la fase de planificación del
proyecto**: antes de construir el sistema, se identifican los riesgos previsibles del
desafío técnico —ejecutar un asistente de voz con un modelo de lenguaje, visión y voz dentro
de 4 GB de VRAM, de forma local y desarrollado por un solo estudiante en un semestre— y se
define para cada uno una **estrategia de mitigación preventiva**. La matriz no es un registro
de hechos consumados, sino un **instrumento de planificación** que orienta las decisiones de
diseño desde el inicio.

### 3.1. Metodología de evaluación (escala cuantitativa)

Cada riesgo se cuantifica con dos variables en una escala de 1 a 5, siguiendo la práctica
estándar de gestión de riesgos (PMBOK / ISO 31000):

- **Probabilidad (P):** 1 = muy improbable (≈10 %), 2 = improbable (≈30 %), 3 = posible
  (≈50 %), 4 = probable (≈70 %), 5 = casi seguro (≈90 %).
- **Impacto (I):** 1 = insignificante, 2 = menor, 3 = moderado, 4 = mayor, 5 = crítico
  (compromete la viabilidad del proyecto).
- **Exposición (E = P × I):** valor de 1 a 25, que clasifica el riesgo en tres zonas:
  **Baja** (1–6, verde), **Media** (7–12, amarillo) y **Alta** (15–25, roja).

Para cada riesgo se indica además el **riesgo residual** (la exposición esperada *después* de
aplicar la mitigación planificada), que es el objetivo de la gestión: llevar los riesgos de
zona alta a zona media o baja.

**Tabla 3.1. Matriz de riesgos del proyecto (evaluación a priori, fase de planificación).**

| ID | Riesgo | Descripción (anticipada al planificar) | P | I | E = P×I | Zona | Estrategia de mitigación preventiva | Residual (E) |
|---|---|---|:-:|:-:|:-:|:-:|---|:-:|
| R1 | **El modelo capaz no entra en 4 GB de VRAM** | El LLM de mejor calidad podría exceder la VRAM disponible y ser imposible de cargar, bloqueando el proyecto entero. | 5 | 5 | **25** | Alta | Medir el consumo real de cada candidato (barrido de VRAM reproducible) antes de comprometerse; tener un modelo de menor tamaño como plan B (familia E2B) y aceptar el *trade-off* de calidad si el grande no cabe. | 6 (Baja) |
| R2 | **Tool-calling poco fiable en un modelo pequeño** | Un modelo de ~2 000 M de parámetros podría seleccionar mal la herramienta, inventarla o no llamarla, degradando la utilidad del asistente. | 4 | 5 | **20** | Alta | Reservar tiempo para *fine-tuning* específico de *tool-calling* con un *gate* numérico (0 % de herramientas inventadas); diseñar un router previo (encoder + *abstain head*) y un mecanismo de reintento forzado de herramienta. | 8 (Media) |
| R3 | **La voz local no alcanza latencia usable** | La cadena voz→STT→LLM→TTS, corriendo STT/TTS en CPU para no robar VRAM, podría superar el presupuesto de 4–5 s por turno y volver la experiencia inviable. | 4 | 4 | **16** | Alta | Fijar un presupuesto de latencia explícito como criterio de éxito; ejecutar STT/TTS en CPU con cuantización int8; planificar optimización de caché de prefijo y medir cada turno contra el presupuesto. | 8 (Media) |
| R4 | **Inestabilidad del *stack* de inferencia en GPU modesta** | Los motores de inferencia y de TTS sobre GPU de gama baja/serie reciente pueden presentar *crashes* o *segfaults* (p. ej., por *flash-attention* o SDPA), tumbando el servicio. | 4 | 4 | **16** | Alta | Validar la estabilidad de cada componente antes de adoptarlo; preferir configuraciones conservadoras por defecto (desactivar funciones inestables, usar un TTS estable en CPU); tener *fallback* a CPU. | 6 (Baja) |
| R5 | **El asistente ejecuta acciones dañinas o no deseadas** | Un agente que opera el SO (borrar archivos, enviar mensajes, apagar) podría realizar acciones irreversibles por error de interpretación o por una orden ambigua. | 4 | 5 | **20** | Alta | Diseñar desde el inicio una política de **confirmación obligatoria** para acciones de riesgo y **honestidad estructural** (verificar el efecto real contra el estado del SO antes de declararlo hecho); resolución de destinatarios sin búsqueda ciega. | 8 (Media) |
| R6 | **El modelo "alucina" haber actuado** | El LLM podría afirmar que realizó una acción sin haberla ejecutado, erosionando la confianza del usuario en un asistente que debe ser fiable. | 4 | 4 | **16** | Alta | Verificación tri-estado (`True`/`False`/`None`) por el estado real del SO (registry, UIA, pycaw), nunca por el juicio del propio modelo; el sistema declara "no verificado" cuando no puede comprobarlo. | 6 (Baja) |
| R7 | **El consumo de recursos congela el equipo** | Las cargas de inferencia/evaluación en CPU/GPU podrían saturar la máquina del usuario (uso del 100 % de CPU, *busy-wait*) y dejarla inutilizable. | 3 | 4 | **12** | Media | Limitar hilos y prioridad de los runtimes, desactivar *busy-wait* y fijar afinidad de CPU; presupuestar recursos por componente desde el diseño. | 4 (Baja) |
| R8 | **El producto no es comercializable por licencias** | Alguna dependencia con licencia restrictiva (GPL/AGPL) podría impedir vender el producto, comprometiendo el objetivo de viabilidad comercial. | 3 | 3 | **9** | Media | Auditar las licencias de todo el *stack* de forma reproducible y temprana; preferir dependencias permisivas (Apache/MIT/BSD); aislar como subproceso o reemplazar cualquier componente GPL bloqueante. | 4 (Baja) |
| R9 | **Sesgo a una sola voz/idioma (pérdida de universalidad)** | Optimizar el sistema sobre la voz o el idioma del desarrollador mejoraría su caso particular pero degradaría a los demás usuarios, violando el requisito de uso universal. | 3 | 4 | **12** | Media | Evaluar siempre contra un conjunto diverso *held-out* (múltiples voces e idiomas); clasificar por *embeddings* multilingües en lugar de listas de palabras por idioma; prohibir el *fine-tuning* sobre una sola voz. | 6 (Baja) |
| R10 | **El alcance excede el tiempo de un estudiante en un semestre** | El proyecto integra voz, visión, *computer-use*, memoria y accesibilidad; intentar todo a la vez arriesga no terminar nada con calidad dentro del plazo. | 4 | 4 | **16** | Alta | Adoptar desarrollo iterativo (Scrum) con tres iteraciones priorizadas; entregar un MVP funcional temprano y crecer por incrementos gateados; recortar alcance antes que comprometer calidad. | 8 (Media) |
| R11 | **El hardware de prueba no representa al del usuario** | Desarrollar en una GPU potente podría ocultar problemas que solo aparecen en el hardware modesto objetivo (4 GB o sin GPU dedicada). | 3 | 3 | **9** | Media | Definir el perfil de hardware objetivo desde el inicio y medir contra él (perfil de 4 GB); proveer un *fallback* a CPU y un binario CPU/Vulkan para equipos sin GPU NVIDIA. | 4 (Baja) |
| R12 | **Declarar logros sin medición ("celebrar sin medir")** | El riesgo metodológico de dar por resuelto un problema sin un número contra un criterio definido, llevando a un producto que parece funcionar pero no. | 4 | 4 | **16** | Alta | Regla transversal *medir, no celebrar*: cada objetivo tiene un *gate* numérico definido de antemano; validación en vivo obligatoria con variantes de fraseo antes de cerrar cualquier avance. | 6 (Baja) |

*Fuente: Elaboración propia (2026). Evaluación a priori de la fase de planificación; las
estrategias de mitigación orientan las decisiones de diseño del proyecto.*

### 3.2. Lectura de la matriz

La evaluación a priori identifica **siete riesgos en zona alta** (E ≥ 15): R1, R2, R3, R4,
R5, R10 y R12. Su concentración es reveladora: los de mayor exposición provienen de las
**dos restricciones nucleares del proyecto** —la barrera física de 4 GB de VRAM (R1) y la
debilidad de *tool-calling* del modelo pequeño que esa barrera obliga a usar (R2)—, junto con
los riesgos propios de un **agente que actúa sobre el sistema** (R5, R6) y los de **gestión**
del propio proyecto (R10 alcance, R12 método). Esta lectura justifica directamente las
decisiones de diseño centrales: medir la VRAM antes de elegir el modelo, *fine-tunear* el
*tool-calling* con un *gate*, construir honestidad estructural y confirmación de acciones, y
adoptar un desarrollo iterativo y dirigido por medición. La estrategia de mitigación apunta a
llevar todos los riesgos a zona **media o baja** (columna de residual), que es el objetivo de
la planificación: ningún riesgo de viabilidad queda sin un plan de contención definido antes
de empezar a construir.

> **Nota sobre el seguimiento.** Esta matriz a priori se complementa, durante la ejecución,
> con el seguimiento de los riesgos efectivamente gatillados en cada iteración (documentado en
> la sección de ejecución del proyecto). Varios de estos riesgos previstos se materializaron y
> fueron contenidos con la estrategia planificada —evidencia de que la identificación a priori
> fue acertada—, mientras que las mitigaciones preventivas evitaron que otros llegaran a
> ocurrir.

---

## 4. Técnicas de análisis del problema

El proyecto aplica **dos técnicas complementarias** de análisis de la problemática: el
**Diagrama de Ishikawa** (causa-efecto, técnica exigida por el curso) y el **Árbol de
Problemas** complementado con un **análisis de Pareto** (priorización de causas según su
peso). Ambas convergen en el mismo problema central y se anclan en datos reales medidos.

**Problema central:** *"No existe un asistente de voz local, privado y gratuito usable en
hardware modesto (≤ 4 GB de VRAM)."*

### 4.1. Técnica 1 — Diagrama de Ishikawa (espina de pescado)

El diagrama de Ishikawa organiza las causas del problema en las categorías de las "6M"
adaptadas al caso (Tecnología/Máquina, Costo, Privacidad, Conectividad, Idioma/Usuario y
Método). La representación esquemática es la siguiente:

```
   TECNOLOGÍA / MÁQUINA          COSTO
   • LLMs grandes no caben       • APIs en la nube de pago
     en 4 GB de VRAM             • GPUs de alta gama caras
   • Visión (mmproj) ~1,2 GB     • Suscripciones recurrentes
   • Hardware de usuario modesto         \
            \                              \
             \                              ▼
   PROBLEMA: ──► "No existe un asistente de voz local, privado y
                  gratuito usable en hardware modesto (≤ 4 GB VRAM)"
             /                              ▲
            /                              /
   • Audio enviado a la nube     • Tool-calling difícil en
   • Datos en servidores ajenos    modelos chicos (~75 % vs ~91 %, estimado)
   • Pérdida de control          • El modelo puede "alucinar" acciones
   PRIVACIDAD                    • Sin medición/gates, se celebra sin verificar
                                 MÉTODO
   • Requiere internet permanente   • Competidores monolingües/anglocéntricos
   • Latencia de red por turno      • Hardcodeo de idioma (p. ej., chino)
   • No opera offline               • Excluye otros idiomas/acentos
   CONECTIVIDAD                     IDIOMA / USUARIO
```

**Tabla 4.1. Ishikawa: categorías y causas de la problemática.**

| Categoría (6M) | Causas identificadas |
|---|---|
| **Tecnología / Máquina** | Los LLM de gran tamaño no entran en 4 GB de VRAM; el encoder de visión (mmproj) consume ~1,2 GB; el hardware del usuario objetivo es modesto (GPU de 4 GB o sin GPU dedicada). |
| **Costo** | Las API en la nube son de pago; las GPU de alta gama son costosas; las versiones avanzadas de los asistentes exigen suscripción recurrente. |
| **Privacidad** | El audio del usuario se envía a la nube; los datos de voz se almacenan en servidores ajenos; el usuario pierde el control sobre su información. |
| **Conectividad** | El servicio requiere conexión permanente a internet; la latencia de red se suma a cada turno; no hay operación offline. |
| **Idioma / Usuario** | Los competidores son monolingües o anglocéntricos; existe *hardcodeo* de idioma; se excluye a hablantes de otros idiomas y acentos. |
| **Método** | El *tool-calling* es difícil en modelos pequeños (estimado ~75 % de acierto frente a ~91 % en grandes); riesgo de "alucinar" acciones; la ausencia de medición contra criterios de éxito conduce a declarar logros no verificados. |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md`,
`Gemma4_estado_y_limites_2026_05_29.md` y `vram_real_medida.csv`.*

#### Descripción de causas (en qué consiste / qué efecto / cómo incide)

- **LLM grandes no caben en 4 GB (Tecnología, causa principal, factor interno).** *En qué
  consiste:* los modelos más capaces requieren varios GB de VRAM aun en cuantizaciones
  agresivas. *Qué efecto produce:* obliga a renunciar al modelo más capaz. *Cómo incide:* es
  la restricción raíz; la medición lo confirma (E2B-Q4 = 3.371 MiB entra; E4B-Q4 = 5.087 MiB
  no entra; 26B > 12.000 MiB), y motivó el pivote E4B → E2B.
- **El encoder de visión pesa ~1,2 GB (Tecnología).** *En qué consiste:* habilitar visión
  exige mantener residente el componente mmproj. *Qué efecto produce:* reduce el margen para
  el resto del modelo y el contexto. *Cómo incide:* en 4 GB el margen tras la visión es
  estrecho, obligando a gestionarla como recurso gateado y a desactivarla en modo CPU.
- **API en la nube de pago y suscripciones (Costo, principal, externo).** *En qué consiste:*
  las capacidades avanzadas se ofrecen bajo pago. *Qué efecto produce:* barrera económica de
  entrada y gasto continuo. *Cómo incide:* es incompatible con la restricción de ser
  íntegramente gratuito y de código abierto.
- **Audio a la nube y datos en servidores ajenos (Privacidad, principal).** *En qué
  consiste:* el modelo dominante transmite el audio a terceros. *Qué efecto produce:* el
  usuario pierde el control sobre información biométrica y sensible. *Cómo incide:* da
  identidad al proyecto; la evidencia de mercado muestra que ~60 % de los usuarios está
  preocupado por su privacidad y 77 % preferiría una alternativa con mayores garantías
  (Secure Data Recovery Services, 2024).
- **Requiere internet permanente y añade latencia (Conectividad, secundaria, externo).** *En
  qué consiste:* el procesamiento en la nube exige conexión continua y un viaje de datos por
  turno. *Qué efecto produce:* inhabilita el uso offline y degrada la experiencia en redes
  lentas. *Cómo incide:* limita fiabilidad y disponibilidad; el procesamiento local lo
  resuelve.
- **Competidores monolingües y hardcodeo de idioma (Idioma/Usuario, secundaria).** *En qué
  consiste:* varias soluciones están optimizadas para un único idioma o codifican reglas
  rígidas por idioma (OS-Copilot: chino). *Qué efecto produce:* excluye a hablantes de otros
  idiomas y acentos. *Cómo incide:* contradice el requisito de uso universal; se cubre con
  clasificación por *embeddings* multilingües.
- **Tool-calling difícil en modelos pequeños (Método, principal, interno).** *En qué
  consiste:* la selección correcta de la acción es más exigente para un modelo de 2.000
  millones de parámetros. *Qué efecto produce:* menor tasa de acierto (estimada ~75 % E2B vs ~91 %
  E4B) y riesgo de acción equivocada. *Cómo incide:* es el compromiso técnico aceptado al
  elegir el modelo que entra en 4 GB; motiva el reintento forzado de herramienta.
- **Riesgo de "alucinar" acciones (Método).** *En qué consiste:* el modelo puede inventar
  una acción o afirmar haberla ejecutado sin que sea cierto. *Qué efecto produce:* erosiona
  la confianza del usuario. *Cómo incide:* obliga a incorporar honestidad estructural
  (verificación por el estado real del SO, no por el juicio del modelo).
- **Ausencia de medición frente a criterios de éxito (Método).** *En qué consiste:* declarar
  logros sin un número contra un criterio definido de antemano. *Qué efecto produce:* lleva a
  dar por resueltos problemas que persisten. *Cómo incide:* se neutraliza con desarrollo
  dirigido por mediciones y *gates*.

### 4.2. Técnica 2 — Árbol de problemas y priorización de Pareto

El **Árbol de Problemas** complementa al Ishikawa al explicitar la relación vertical entre
**causas** (raíces), **problema central** (tronco) y **efectos** (copa), permitiendo además
una lectura como **árbol de oportunidad** al invertir cada nodo. La representación es la
siguiente:

```
                        EFECTOS (copa)
   • Exclusión de usuarios sin GPU/conexión/presupuesto
   • Pérdida de privacidad (audio biométrico a la nube)
   • Dependencia de proveedores y costo recurrente
   • Asistentes inaccesibles para personas con discapacidad
                              ▲
   ───────────────────────────────────────────────────────
   PROBLEMA CENTRAL (tronco): "No existe un asistente de voz
   local, privado y gratuito usable en hardware modesto (≤4 GB)"
   ───────────────────────────────────────────────────────
                              ▲
                        CAUSAS (raíces)
   • Restricción de VRAM: LLM grandes no entran en 4 GB
   • Tool-calling débil en modelos pequeños (~75 %, estimado)
   • Modelos comerciales atan voz a la nube de pago
   • Soluciones del estado del arte monolingües/de nube
```

**Tabla 4.2. Árbol de problemas: causas, problema central y efectos.**

| Nivel | Elementos |
|---|---|
| **Causas (raíces)** | Restricción física de 4 GB de VRAM (los LLM capaces no entran); *tool-calling* débil en modelos pequeños; el modelo comercial ata la voz a la nube de pago; las soluciones locales del estado del arte son de nube, monolingües o frameworks de desarrollo. |
| **Problema central (tronco)** | No existe un asistente de voz local, privado y gratuito usable en hardware modesto (≤ 4 GB de VRAM). |
| **Efectos (copa)** | Exclusión de usuarios sin GPU potente, sin conexión o sin presupuesto; pérdida de privacidad por envío de audio biométrico a la nube; dependencia de proveedores y costo recurrente; barreras de accesibilidad para personas con discapacidad visual o motora. |

*Fuente: Elaboración propia (2026).*

#### Priorización de Pareto (regla 80/20)

No todas las causas pesan igual. El análisis de Pareto ordena las causas por su contribución
al problema, distinguiendo las **"causas vitales"** (las pocas que explican la mayor parte
del problema) de las **"causas triviales"**. Las causas se ponderan con datos medidos del
proyecto.

**Tabla 4.3. Priorización de causas (análisis de Pareto).**

| # | Causa | Peso relativo | Evidencia medida | Clasificación |
|---|---|---|---|---|
| 1 | Restricción de VRAM (4 GB) | Muy alto | E2B-Q4 = 3.371 MiB entra; E4B-Q4 = 5.087 MiB no; 26B > 12.000 MiB (`vram_real_medida.csv`) | **Vital** |
| 2 | Tool-calling débil del modelo pequeño | Alto | estimado ~75 % E2B vs ~91 % E4B; gate de 0 % herramientas inventadas tras fine-tuning | **Vital** |
| 3 | Costo / dependencia de la nube de pago | Alto | Competidores de pago (Mark-XXXIX = Gemini LiveAPI); costo de operación de la propuesta = USD 0 | **Vital** |
| 4 | Privacidad (audio a la nube) | Medio-alto | ~60 % de usuarios preocupados; 77 % preferiría más garantías (Secure Data Recovery Services, 2024) | Significativa |
| 5 | Falta de soporte multilingüe real | Medio | Competidores monolingües/anglocéntricos (OS-Copilot: chino) | Significativa |
| 6 | Conectividad / latencia de red | Bajo-medio | Procesamiento local elimina el viaje de red por turno | Trivial (derivada) |

*Fuente: Elaboración propia (2026), a partir de `vram_real_medida.csv`,
`ANALISIS_COMPETENCIA.md` y Secure Data Recovery Services (2024).*

**Interpretación de Pareto.** Las **tres causas vitales** —la restricción de VRAM, la
debilidad de *tool-calling* y el costo/dependencia de la nube— concentran la mayor parte de
la dificultad del problema y, en consecuencia, son las que reciben el grueso del esfuerzo de
diseño (pivote E4B → E2B medido, fine-tuning con gate de 0 % de herramientas inventadas y
stack íntegramente OSS). Atacar estas tres causas resuelve estructuralmente el problema
central; las causas restantes se resuelven como subproducto del enfoque local (privacidad,
conectividad) o mediante decisiones de diseño específicas (embeddings multilingües para el
idioma).

### 4.3. Convergencia de ambas técnicas

Las dos técnicas son consistentes y complementarias: el **Ishikawa** descompone las causas
por naturaleza (las 6M) y es exhaustivo, mientras que el **Árbol de Problemas con Pareto**
las ordena verticalmente (causa → problema → efecto) y las prioriza por peso. Ambas
convergen en que las causas de mayor impacto son técnicas y económicas (VRAM, *tool-calling*
y costo de la nube), lo que justifica directamente las decisiones nucleares del proyecto: un
modelo pequeño que entra en 4 GB, fine-tuneado para *tool-calling* fiable, sobre un stack de
código abierto y de procesamiento local.

---

## 5. Modelo de negocio CANVAS

El **modelo CANVAS** (Osterwalder et al., 2010) describe la lógica de creación, entrega y
captura de valor del proyecto a través de **nueve bloques**. Aunque el sistema se desarrolla
con fines académicos y bajo un principio de gratuidad y código abierto, el análisis CANVAS es
pertinente porque el proyecto se diseñó desde el inicio como un producto **vendible** (stack
de licencia permisiva, auditado), por lo que su modelo de negocio describe cómo podría
sostenerse y distribuirse.

**Tabla 5.1. Lienzo de modelo de negocio (CANVAS) del proyecto.**

| # | Bloque | Contenido |
|---|---|---|
| **1** | **Segmentos de clientes** | (a) Usuarios *privacy-conscious* que rechazan enviar su voz a la nube; (b) usuarios de **hardware modesto** (laptops con GPU de 4 GB o sin GPU dedicada) excluidos por los asistentes que exigen GPU grande o nube de pago; (c) **personas con discapacidad** visual o motora que requieren control por voz/gestos; (d) hablantes de **idiomas no anglocéntricos** mal servidos por los asistentes comerciales. |
| **2** | **Propuesta de valor** | Un asistente de voz **local, privado, gratuito y multilingüe** que opera íntegramente en la máquina del usuario (≤ 4 GB de VRAM), controla el sistema operativo por voz (*computer-use*), funciona **sin internet** y **no transmite datos** a terceros, con **honestidad estructural** (no declara acciones que no puede verificar) y modos de **accesibilidad**. |
| **3** | **Canales** | Distribución como software descargable (instalador para Windows); repositorio de código abierto (GitHub) para la comunidad técnica; documentación y demostración del MVP como canal de difusión. |
| **4** | **Relación con clientes** | Autoservicio (instalación local sin cuenta ni suscripción); soporte comunitario *open source*; el carácter local implica **cero dependencia** de un servicio operado por el proveedor. |
| **5** | **Fuentes de ingreso** | El **costo de operación para el usuario es USD 0** (sin suscripción ni APIs de pago). Vías de monetización posibles, dado el stack vendible: licencia de versión *pro*/empresarial, soporte e integración a medida, o donaciones/patrocinio del proyecto OSS. El modelo base es de adopción gratuita. |
| **6** | **Recursos clave** | Modelo **Gemma 4 E2B** (Apache-2.0) *fine-tuneado*; motor de inferencia llama.cpp/llama-server; *wake-word* LiveKit, STT (Whisper/Parakeet) y TTS (Piper); MediaPipe (visión/gestos); encoder del router (*sentence-transformers*); el conocimiento de ingeniería del proyecto (medición, gates, honestidad estructural). |
| **7** | **Actividades clave** | *Fine-tuning* y evaluación del modelo contra *gates*; desarrollo dirigido por medición; integración del *stack* de voz/visión/computer-use; aseguramiento de privacidad y honestidad; mantenimiento y empaquetado multiplataforma (CUDA/CPU/Vulkan). |
| **8** | **Asociaciones clave** | Comunidad y proyectos **OSS** de los que depende el stack: Google/Gemma, llama.cpp, MediaPipe, sentence-transformers, Piper, LiveKit; comunidades de accesibilidad y privacidad como aliados de difusión. |
| **9** | **Estructura de costos** | El costo es principalmente **tiempo de desarrollo** (un estudiante); el costo de infraestructura es **nulo** (no hay servidores en la nube que operar, al correr todo en el equipo del usuario); costos marginales de distribución (*hosting* del instalador) bajos. |

*Fuente: Elaboración propia (2026), según el modelo CANVAS (Osterwalder et al., 2010),
con datos de `documentacion/00_producto/{ANALISIS_COMPETENCIA, AUDITORIA_licencias}.md`.*

### 5.1. Lectura del CANVAS

El lienzo evidencia la coherencia del modelo: la **propuesta de valor** (local, privado,
gratuito) se sostiene sobre una **estructura de costos sin nube** (bloque 9) que, a su vez,
es el origen de su ventaja de privacidad y de su costo de operación cero. La captura de valor
no descansa en una suscripción recurrente —como en los asistentes comerciales—, sino en la
adopción de un producto que el usuario ejecuta en su propio hardware, con vías de
monetización abiertas gracias a un *stack* de licencia permisiva (ver Análisis legal, §2). El
modelo es, por tanto, **sostenible y comercializable** sin traicionar el principio de
gratuidad para el usuario final.

---

## Referencias (APA 7)

Astute Analytica. (2026, 10 de febrero). *Voice assistant market to reach US$ 59.9 billion
by 2033 driven by mass consumer adoption, enterprise voice AI, and smart device
proliferation*. GlobeNewswire.

Secure Data Recovery Services. (2024, 5 de agosto). *Listening in: Privacy concerns of voice
assistants*. https://www.securedatarecovery.com/blog/smart-device-privacy-concerns

Villacura, E. (2026). *ANALISIS_COMPETENCIA.md*, *AUDITORIA_licencias.md*,
*Gemma4_estado_y_limites_2026_05_29.md* y *BACKLOG_MAESTRO.md* [documentos internos del
proyecto Baxy]. Repositorio del proyecto.

Biblioteca del Congreso Nacional de Chile. (2024). *Ley N.º 21.719: Protección de datos
personales*; *Ley N.º 19.628: Protección de la vida privada*; *Ley N.º 21.459: Delitos
informáticos*; *Ley N.º 21.663: Marco de ciberseguridad*. Viña del Mar, Chile.

Osterwalder, A., Pigneur, Y., & Clark, T. (2010). *Business Model Generation: A Handbook
for Visionaries, Game Changers, and Challengers*. John Wiley & Sons.

Project Management Institute. (2021). *A Guide to the Project Management Body of Knowledge
(PMBOK Guide)* (7.ª ed.). Project Management Institute.

---

*Fuente general del documento: Elaboración propia (2026), sobre datos medidos del
repositorio del proyecto Baxy.*
