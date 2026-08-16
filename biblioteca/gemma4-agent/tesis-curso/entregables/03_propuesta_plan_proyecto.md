**Propuesta de Solución y Plan de Proyecto**

> Bloque del Informe Final correspondiente a la **Propuesta de Solución** y al **Plan de
> Proyecto** (Metodología de Gestión, Metodología de Desarrollo, Plan de Monitoreo y
> Control, Plan de Gestión de Riesgos y Planificación). Redactado en español académico
> formal, tercera persona, con base en los datos medidos del repositorio del proyecto y
> citado conforme a APA 7. Fuentes internas principales:
> `documentacion/00_producto/Gemma4_estado_y_limites_2026_05_29.md`,
> `documentacion/01_arquitectura/{ARCHITECTURE.md, 01_delta_system.md}`,
> `documentacion/datos_crudos/vram_real_medida.csv` y
> `documentacion/_backlog/BACKLOG_MAESTRO.md`.

---

# 4. Propuesta de Solución

## 4.1 Descripción de la solución

La solución propuesta, denominada **Baxy** (sobre el motor **Baxy**), consiste
en un **asistente de voz local y privado para el sistema operativo Windows**, capaz de
operar de manera íntegra y sin conexión (offline) sobre hardware modesto, definido como un
equipo con una tarjeta gráfica de cuatro gigabytes de memoria de video (4 GB de VRAM) o,
en su defecto, ejecución en CPU. A diferencia de los asistentes comerciales dominantes
—que transmiten el audio del usuario a servidores remotos para su procesamiento—, la
solución ejecuta la totalidad de su pipeline cognitivo en la máquina del usuario, de modo
que **ningún dato de voz, pantalla o comportamiento abandona el equipo**.

El núcleo de la solución es un modelo de lenguaje **Gemma 4 E2B fine-tuneado**, servido en
formato GGUF con cuantización Q4\_K\_M mediante `llama.cpp`/`llama-server`. La elección del
modelo no fue arbitraria sino el resultado de una medición reproducible de consumo de
memoria de video: el modelo Gemma 4 E2B-Q4\_K\_M es el único miembro de la familia Gemma 4
que carga de manera completa (pesos del modelo, encoder de visión residente y caché de
atención) dentro del presupuesto de 4 GB, con un consumo medido de **3.371 MiB de delta de
VRAM** (aproximadamente 3,36 GB), mientras que la variante inmediatamente superior
(E4B-Q4\_K\_M) demanda **5.087 MiB** y no entra ni en su cuantización más baja
(`vram_real_medida.csv`, 2026). El detalle de esta decisión se desarrolla en la sección de
Alternativas de Solución y en el Plan de Gestión de Riesgos.

Funcionalmente, la solución implementa un flujo conversacional por voz de extremo a
extremo: el usuario activa el asistente mediante una palabra de activación (*wake-word*),
su voz es transcrita a texto por un motor de reconocimiento de voz (STT) que se ejecuta en
CPU para no competir por la VRAM, el texto resultante es procesado por el modelo de lenguaje
—que decide si responder conversacionalmente o invocar una de más de **sesenta
herramientas** de dominio (*tools*) de control del sistema operativo (67 esquemas únicos expuestos al
modelo tras retirar `smart_home`)— y, finalmente, la respuesta se
sintetiza en voz mediante un motor de texto a voz (TTS), también en CPU. Las herramientas
de control cubren la apertura de aplicaciones, navegación web, automatización de la interfaz
gráfica mediante una cascada de accesibilidad (UIA → OCR → visión por computador), gestión
de mensajería, control de volumen y brillo, y modos de accesibilidad por gestos de cámara,
entre otras.

La solución incorpora, además, un conjunto de **garantías estructurales de honestidad** que
constituyen un diferenciador frente a la competencia: el sistema no declara haber ejecutado
una acción si no puede verificarlo contra el estado real del sistema operativo (registro de
Windows, automatización de interfaz UIA, control de audio mediante `pycaw`), evitando así la
alucinación de acciones. Esta verificación de tres estados (`True` / `False` / `None`, donde
`None` representa "no verificable" y nunca se confunde con éxito) está implementada en la
capa de verificadores (`safety_pkg/verifiers.py`, `computer_use_pkg/computer_use.py`).

## 4.2 Diagrama de Contexto

El siguiente diagrama de contexto (correspondiente al nivel 0 de un Diagrama de Flujo de
Datos, o nivel 1 del modelo C4) sitúa al sistema **Baxy** en el centro y representa sus
interacciones con los actores y entidades externas. La característica determinante de la
arquitectura es la **ausencia total de la nube**: no existe ninguna flecha que salga del
perímetro del equipo del usuario hacia servicios remotos.

```
                         PERÍMETRO DEL EQUIPO DEL USUARIO (todo local, sin nube)
   ┌───────────────────────────────────────────────────────────────────────────────────┐
   │                                                                                       │
   │   ┌─────────────┐        voz / wake-word        ┌──────────────────────────────┐    │
   │   │             │ ─────────────────────────────►│                              │    │
   │   │   USUARIO   │        gestos (cámara)         │           BAXY             │    │
   │   │ (operador)  │ ─────────────────────────────►│      (Baxy)         │    │
   │   │             │◄───────────────────────────── │                              │    │
   │   └─────────────┘     respuesta hablada (TTS)    │  STT (CPU) → LLM Gemma 4     │    │
   │         ▲                + UI visual             │  E2B-FT (GPU 4GB / CPU) →    │    │
   │         │                                        │  Router + ~67 Tools →        │    │
   │         │                                        │  Verificadores → TTS (CPU)   │    │
   │         │                                        └──────────────────────────────┘    │
   │         │                                              │            ▲                 │
   │   ┌─────┴────────────┐                                 │ control    │ estado          │
   │   │   PERIFÉRICOS    │                                 ▼ (acciones) │ (verificación)  │
   │   │  • Micrófono     │                          ┌──────────────────────────────┐    │
   │   │  • Cámara        │                          │   SISTEMA OPERATIVO WINDOWS   │    │
   │   │  • Parlante      │                          │  • Aplicaciones (apps)        │    │
   │   │  • Pantalla      │                          │  • Registro (registry)        │    │
   │   └──────────────────┘                          │  • Interfaz UIA / accesib.    │    │
   │                                                 │  • Audio (pycaw) / Brillo     │    │
   │                                                 │  • Navegador / Mensajería     │    │
   │                                                 └──────────────────────────────┘    │
   │                                                                                       │
   └───────────────────────────────────────────────────────────────────────────────────┘
                          ✗  SIN CONEXIÓN A LA NUBE  ✗  SIN ENVÍO DE DATOS  ✗
```

**Lectura del diagrama.** El **Usuario** interactúa con **Baxy** por dos canales de
entrada (voz activada por palabra clave, y gestos capturados por cámara para los modos de
accesibilidad) y recibe dos canales de salida (respuesta hablada por TTS y realimentación
visual en la interfaz). Baxy, a su vez, **actúa sobre el Sistema Operativo Windows** a
través de su capa de herramientas (apertura de aplicaciones, automatización UIA, control de
audio y brillo, navegación, mensajería) y, de forma crítica, **lee el estado real del
sistema operativo** para verificar que sus acciones efectivamente ocurrieron antes de
declararlas exitosas. Los **periféricos** (micrófono, cámara, parlante, pantalla) son los
dispositivos físicos del equipo que median la interacción. La frontera del diagrama —el
perímetro del equipo— no es atravesada por ningún flujo de datos hacia el exterior: esta es
la materialización arquitectónica de la propuesta de valor de privacidad.

---

# 5. Plan de Proyecto

## 5.1 Metodología de Gestión

La selección de la metodología de gestión no se realizó por preferencia, sino mediante una
**tabla comparativa ponderada** que evalúa las metodologías candidatas frente a los factores
críticos del proyecto, asignando pesos que suman 1,0 y una escala de evaluación de 1
(deficiente) a 3 (óptimo). Los factores de evaluación se derivan de la naturaleza real del
desarrollo: alta incertidumbre técnica (no se sabía de antemano qué modelo entraría en 4 GB
ni qué tasa de tool-calling sería alcanzable), necesidad de retroalimentación frecuente y la
existencia de un único desarrollador.

**Tabla 5.1. Selección ponderada de la metodología de gestión.**

| Factor de evaluación | Peso | Cascada | Kanban | Scrum |
|---|---|---|---|---|
| Adaptación a requisitos cambiantes / alta incertidumbre técnica | 0,30 | 1 | 3 | 3 |
| Entregas iterativas con valor incremental (MVP temprano) | 0,25 | 1 | 2 | 3 |
| Retroalimentación y validación frecuente (gates por iteración) | 0,20 | 1 | 2 | 3 |
| Ajuste a equipo pequeño / un solo desarrollador | 0,15 | 2 | 3 | 2 |
| Trazabilidad y planificación temporal (horizonte académico) | 0,10 | 3 | 2 | 3 |
| **Puntaje ponderado total** | **1,00** | **1,30** | **2,50** | **2,85** |

**Decisión.** La metodología seleccionada es **Scrum** (puntaje ponderado 2,85), con
incorporación de prácticas de **Kanban** (puntaje 2,50) para la visualización del flujo de
trabajo. La elección se justifica además por la evidencia empírica del propio desarrollo:
el historial del repositorio y la memoria del proyecto documentan un trabajo **organizado
explícitamente en sprints iterativos** (por ejemplo, "Sprint 7" del wake-word, los sprints
"Sprint 1 R1–R8" de refactorización arquitectónica, y los sucesivos sprints de optimización
del router y de fine-tuning), lo que confirma que el desarrollo fue de hecho gestionado de
manera ágil e iterativa. Cascada queda descartada (puntaje 1,30) por su incompatibilidad con
la alta incertidumbre técnica del proyecto: comprometerse a un plan cerrado al inicio habría
sido inviable cuando ni siquiera era seguro que la solución cupiera en el presupuesto de
memoria. Schwaber y Sutherland (2020) definen Scrum precisamente como un marco para abordar
problemas complejos y adaptativos mediante entregas incrementales, lo que coincide con el
perfil de este proyecto.

## 5.2 Metodología de Desarrollo

La metodología de desarrollo adoptada es **iterativa e incremental, orientada a entregas
con valor incremental** y, de manera distintiva, **dirigida por medición contra criterios de
éxito definidos a priori** (*gates*). Esta es la regla de oro operativa del proyecto,
formalizada en su documento de instrucciones de trabajo (`CLAUDE.md`): *ninguna decisión
técnica se da por hecha ni se declara exitosa sin un número contra un criterio definido de
antemano*.

El ciclo de desarrollo de cada funcionalidad sigue un patrón disciplinado y reproducible:

1. **Medir primero.** Antes de implementar, se establece la línea base y el criterio de
   éxito numérico (el *gate*). Por ejemplo, el wake-word se evaluó contra el gate
   `recall ≥ 0,60 ∧ falsos positivos/hora ≤ 1,0` sobre un conjunto de prueba universal y
   diverso (no sobre la voz del operador, para preservar la universalidad).
2. **Implementar de forma gateada.** Las funcionalidades nuevas se incorporan detrás de un
   *feature flag* en estado desactivado por defecto (`default-off`), de modo que no alteren
   el comportamiento estable mientras se validan.
3. **Validar en vivo.** Un cambio que afecta el comportamiento del agente no se considera
   terminado hasta ejecutarlo contra el modelo de lenguaje y el agente reales, con el
   mensaje que un usuario escribiría, verificando la cadena completa de herramientas y la
   respuesta final. Esta exigencia surgió de un fallo concreto: una corrección probada solo
   con pruebas unitarias mockeadas falló en producción porque el modelo, al ser
   no-determinista, eligió un camino de herramienta distinto al anticipado.
4. **Activar (flip a default-on).** Solo tras la validación en vivo se promueve la
   funcionalidad a comportamiento por defecto.

Este enfoque incremental permitió liberar tempranamente una primera versión operativa de la
solución —un asistente capaz de responder por voz de forma offline— y consolidó
sucesivamente sus capacidades (tool-calling, computer-use, optimización de latencia, memoria
de gustos, accesibilidad por cámara) en iteraciones, cada una cerrada contra su propio gate. La separación entre **validación
sintética** (pruebas con datos generados) y **prueba real** (con la voz y el equipo del
usuario) se mantiene explícita en todos los reportes, en línea con la práctica de medición
honesta del proyecto.

## 5.3 Plan de Monitoreo y Control

El monitoreo y control del avance se estructuró sobre tres dimensiones —integridad técnica,
valor de negocio y calidad— y se materializó en instrumentos concretos y reproducibles, no
en estimaciones subjetivas de porcentaje de avance.

**a) Gates medidos por subsistema.** Cada objetivo del proyecto tiene asociado un criterio
de éxito numérico que actúa como control de progreso. Los gates efectivamente medidos
incluyen: consumo de VRAM ≤ 4 GB (medido 3,36 GB para E2B-Q4), 0 % de herramientas
inventadas en producción para el modelo fine-tuneado (gate aprobado, según la memoria del
proyecto FT E2B v2), precisión de routing de 0,9964 sobre un conjunto de prueba retenido
(*held-out*) en español, y latencia por acción ≤ 4–5 s (medida en ~2,2 s tras la optimización
de caché de prefijo). El gate de wake-word (`recall ≥ 0,60 ∧ fp/hr ≤ 1,0`) está **cumplido**:
la palabra de activación "Baxy" alcanzó un **recall de 0,872** sobre un conjunto de prueba
retenido (*held-out*) y diverso de 25 voces y 13 idiomas —holgadamente por encima del umbral
de 0,60—, con recall perfecto (1,00) en español, inglés, italiano, francés, polaco y ruso,
por lo que se contabiliza como gate cerrado.

**b) Suite de pruebas automatizadas como control de calidad.** El proyecto mantiene una
suite de pruebas que creció a lo largo del desarrollo y que debe permanecer verde como
condición de avance. El estado de cierre reportado es de **2.740 pruebas aprobadas y 0
fallidas** (`BACKLOG_MAESTRO.md`, 2026), incluyendo pruebas de regresión por idioma y por
dominio, así como guardas estructurales (anti-loop, anti-alucinación, verificación de
honestidad). El principio anti-regresión es explícito: ningún subgrupo fuerte (por ejemplo,
un idioma con buena cobertura) debe degradarse al mejorar el promedio general.

**c) Evaluaciones batch reproducibles.** El control incorpora evaluaciones a gran escala
reproducibles desde scripts versionados. Un ejemplo es el *replay* de los 1.071 mensajes
únicos reales del usuario, re-ejecutados a través de la interfaz de ejecución del agente con
la ejecución física mockeada, que arrojó una latencia mediana (p50) de 2,5 s, cero errores y
cero fugas de herramientas, permitiendo detectar y corregir incidencias sistémicas.

**d) Backlog maestro como tablero de control.** Se consolidó un único **Backlog Maestro**
como fuente de verdad, en el que cada ítem se verifica contra el código real (no contra la
documentación, que podía estar desactualizada) y se marca con un estado explícito: aplicado
y verificado, pendiente real, bloqueado (con bloqueador identificado), rechazado (medido y
no re-litigable) o en progreso. Esta verificación cruzada —tres auditores cotejando más de
50 ítems contra el código— constituye el reporte de progreso consolidado del proyecto.

## 5.4 Plan de Gestión de Riesgos

La gestión de riesgos se aborda como un **análisis a priori realizado en la fase de
planificación**: antes de construir el sistema se identifican los riesgos previsibles del
desafío —ejecutar un asistente de voz con modelo de lenguaje, visión y voz dentro de 4 GB de
VRAM, de forma local y desarrollado por un solo estudiante en un semestre— y se define para
cada uno una **estrategia de mitigación preventiva**. La matriz no es un registro de hechos
consumados, sino un **instrumento de planificación**. Cada riesgo se cuantifica en una escala
de 1 a 5 de **probabilidad (P)** e **impacto (I)**; la **exposición (E = P × I)**, de 1 a 25,
clasifica el riesgo en zonas **Baja** (1–6), **Media** (7–12) y **Alta** (15–25), y se
reporta el **riesgo residual** esperado tras la mitigación (criterio PMBOK / ISO 31000).

**Tabla 5.2. Matriz de riesgos del proyecto (evaluación a priori, fase de planificación).**

| ID | Riesgo | Descripción (anticipada al planificar) | P | I | E = P×I | Zona | Estrategia de mitigación preventiva | Residual |
|---|---|---|:-:|:-:|:-:|:-:|---|:-:|
| R1 | **El modelo capaz no entra en 4 GB de VRAM** | El LLM de mejor calidad podría exceder la VRAM disponible y ser imposible de cargar, bloqueando el proyecto entero. | 5 | 5 | **25** | Alta | Medir el consumo real de cada candidato (barrido de VRAM reproducible) antes de comprometerse; modelo de menor tamaño como plan B (familia E2B), aceptando el *trade-off* de calidad. | 6 |
| R2 | **Tool-calling poco fiable en un modelo de 2B** | Un modelo de ~2 000 M de parámetros podría seleccionar mal la herramienta, inventarla o no llamarla, degradando la utilidad del asistente. | 4 | 5 | **20** | Alta | *Fine-tuning* específico con *gate* numérico (0 % de herramientas inventadas); router previo (encoder + *abstain head*) y reintento forzado de herramienta. | 8 |
| R5 | **El asistente ejecuta acciones dañinas/no deseadas** | Un agente que opera el SO podría realizar acciones irreversibles (borrar, enviar, apagar) por interpretación errónea o una orden ambigua. | 4 | 5 | **20** | Alta | Confirmación obligatoria para acciones de riesgo y honestidad estructural (verificar el efecto real antes de declararlo); resolución de destinatarios sin búsqueda ciega. | 8 |
| R3 | **La voz local no alcanza latencia usable** | La cadena voz→STT→LLM→TTS, con STT/TTS en CPU para no robar VRAM, podría superar el presupuesto de 4–5 s por turno. | 4 | 4 | **16** | Alta | Presupuesto de latencia como criterio de éxito; STT/TTS en CPU int8; optimización de caché de prefijo, midiendo cada turno contra el presupuesto. | 8 |
| R4 | **Inestabilidad del *stack* de inferencia en GPU modesta** | Los motores de inferencia/TTS sobre GPU de gama baja o serie reciente pueden presentar *crashes* o *segfaults* que tumban el servicio. | 4 | 4 | **16** | Alta | Validar estabilidad de cada componente antes de adoptarlo; defaults conservadores (desactivar funciones inestables, TTS estable en CPU); *fallback* a CPU. | 6 |
| R6 | **El modelo "alucina" haber actuado** | El LLM podría afirmar que realizó una acción sin haberla ejecutado, erosionando la confianza del usuario. | 4 | 4 | **16** | Alta | Verificación tri-estado (`True`/`False`/`None`) por el estado real del SO (registry, UIA, pycaw), nunca por el juicio del modelo; declara "no verificado" si no puede comprobarlo. | 6 |
| R10 | **El alcance excede el tiempo de un estudiante/semestre** | Integrar voz, visión, *computer-use*, memoria y accesibilidad a la vez arriesga no terminar nada con calidad en plazo. | 4 | 4 | **16** | Alta | Desarrollo iterativo (Scrum) con tres iteraciones priorizadas; MVP funcional temprano e incrementos gateados; recortar alcance antes que comprometer calidad. | 8 |
| R12 | **Declarar logros sin medición ("celebrar sin medir")** | Riesgo metodológico de dar por resuelto un problema sin un número contra un criterio definido de antemano. | 4 | 4 | **16** | Alta | Regla transversal *medir, no celebrar*: *gate* numérico por objetivo; validación en vivo obligatoria con variantes de fraseo antes de cerrar cualquier avance. | 6 |
| R7 | **El consumo de recursos congela el equipo** | Las cargas de inferencia/evaluación podrían saturar la máquina (CPU al 100 %, *busy-wait*) y dejarla inutilizable. | 3 | 4 | **12** | Media | Limitar hilos y prioridad de los runtimes, desactivar *busy-wait*, fijar afinidad de CPU; presupuestar recursos por componente desde el diseño. | 4 |
| R9 | **Sesgo a una sola voz/idioma (pérdida de universalidad)** | Optimizar sobre la voz o el idioma del desarrollador mejoraría su caso pero degradaría a los demás, violando el uso universal. | 3 | 4 | **12** | Media | Evaluar siempre contra un *held-out* diverso (varias voces e idiomas); clasificar por *embeddings* multilingües; prohibir el *fine-tuning* sobre una sola voz. | 6 |
| R8 | **El producto no es comercializable por licencias** | Una dependencia con licencia restrictiva (GPL/AGPL) podría impedir la venta, comprometiendo la viabilidad comercial. | 3 | 3 | **9** | Media | Auditar licencias de todo el *stack* de forma reproducible y temprana; preferir permisivas (Apache/MIT/BSD); aislar como subproceso o reemplazar cualquier componente GPL bloqueante. | 4 |
| R11 | **El hardware de prueba no representa al del usuario** | Desarrollar en una GPU potente podría ocultar problemas que solo aparecen en el hardware modesto objetivo (4 GB o sin GPU). | 3 | 3 | **9** | Media | Definir el perfil de hardware objetivo desde el inicio y medir contra él; *fallback* a CPU y binario CPU/Vulkan para equipos sin GPU NVIDIA. | 4 |

## 5.5 Planificación (fases del desarrollo)

La planificación del proyecto se organizó en un horizonte de tres meses, estructurado en una
**fase de inicio/análisis** seguida de **tres iteraciones de desarrollo incremental** y una
**fase de cierre**, en coherencia con la metodología iterativa-incremental adoptada. Cada
iteración corresponde a un agrupamiento de los sprints reales del historial del proyecto y
cierra contra gates medidos.

**Fase 0 — Inicio y Análisis.** Definición del problema (ausencia de un asistente de voz
local, privado, gratuito y multilingüe para hardware modesto), análisis de competencia y
licencias, definición de objetivos SMART con sus criterios de éxito, y establecimiento del
método de trabajo dirigido por medición. Entregable: marco del proyecto y backlog inicial.

**Iteración 1 — Núcleo: LLM local en 4 GB + voz básica.** Selección del modelo respaldada
por la medición de VRAM (Gemma 4 E2B sobre E4B/26B), implementación del reconocimiento de
voz (STT) y síntesis (TTS) en CPU, y wake-word. Riesgos gatillados y mitigados: crash CUDA
#22527 (R1), OOM en 4 GB (R2), congelamiento por ONNX (R5). Gates: VRAM ≤ 4 GB (cumplido);
wake-word `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872 de la palabra de activación
"Baxy" sobre un held-out de 25 voces y 13 idiomas, con recall perfecto en español, inglés,
italiano, francés, polaco y ruso).
**Evidencia de la versión operativa:** el agente responde por voz de forma offline.

**Iteración 2 — Tool-calling, router y acciones (computer-use).** Construcción del router
(encoder fine-tuneado + abstain head + clustering), fine-tuning del modelo E2B para
tool-calling fiable, y la cascada de computer-use (UIA → OCR → visión) con herramientas de
aplicaciones, navegación, mensajería e instalación de software. Riesgos gatillados y
mitigados: tool-calling débil del 2B (R4), destinatario equivocado en mensajería (R8). Gates:
router 0,9964 (held-out ES); 0 % herramientas inventadas en producción tras el fine-tuning.

**Iteración 3 — Robustez, latencia, memoria y accesibilidad.** Optimización de latencia
(caché de prefijo, ~1 s menos por acción, llegando a ~2,2 s), capa de memoria de gustos
(observador de ambiente + perfil de preferencias), interfaz web "Baxy field", control por
cámara/gestos (modos de accesibilidad) y fallback a CPU. Riesgos gatillados y mitigados:
tormenta de reintentos (R10), no-determinismo del modelo (R11). Gates: latencia ≤ 5 s; suite
de pruebas verde.

**Fase de Cierre.** Consolidación del Backlog Maestro como fuente de verdad verificada,
estabilización de la suite (2.740 pruebas aprobadas, 0 fallidas), entrega del producto
terminado y operativo, y redacción del informe final. Se dejó documentado, además, un
*roadmap* de extensiones opcionales posteriores al alcance del proyecto (empaquetado del
binario CPU/Vulkan, cliente MCP y soporte del modelo E4B en equipos con mayor VRAM).

### Carta Gantt (cronograma de fases e iteraciones)

La siguiente tabla resume el cronograma del proyecto sobre un horizonte aproximado de tres
meses. Las fechas agrupan los sprints reales del historial del repositorio y cada iteración
cierra contra su *gate* medido. La representación gráfica (diagrama de Gantt y flujo gateado)
está disponible como **Figuras 7 y 8** en `diagramas_mermaid.md`.

**Tabla 5.3. Carta Gantt: fases, iteraciones e hitos de cierre.**

| Fase / Iteración | Período aproximado | Foco principal | Hito de cierre (gate) |
|---|---|---|---|
| Fase 0 — Inicio / Análisis | Mar 2026 (2 sem.) | Problema, competencia, licencias, objetivos SMART, método *measure-then-ship* | Marco del proyecto y *backlog* inicial |
| Iteración 1 — Núcleo | Mar–Abr 2026 (~3–4 sem.) | LLM local en 4 GB (E2B-Q4) + herramientas + enrutador + voz básica | VRAM ≤ 4 GB (cumplido); *wake-word* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872 sobre held-out de 25 voces/13 idiomas) |
| Iteración 2 — Tool-calling / acciones | Abr–May 2026 (~3–4 sem.) | Router (encoder-FT + *abstain*) + computer-use (UIA/OCR/visión) + visión/cámara | Router 0,9964 (held-out ES); 0 % herramientas inventadas en producción |
| Iteración 3 — Robustez / latencia / memoria / accesibilidad | May–Jun 2026 (~3–4 sem.) | Fine-tuning E2B + optimización de latencia + memoria "Jarvis" + UI + fallback CPU | Latencia ≤ 5 s (p50 ≈ 1,22 s); suite verde |
| Fase de Cierre | Jun 2026 (2 sem.) | Backlog Maestro + suite 2.740/0 + entrega del producto terminado + informe final | Suite 2.740 aprobadas, 0 fallidas (2026-06-02) |

*Fuente: Elaboración propia (2026); fechas derivadas del historial del repositorio
(sprints de 2026-05-26 a 2026-06-02 en la memoria del proyecto).*

---

# Referencias

Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide: The definitive guide to Scrum: The
rules of the game*. Scrum.org. https://scrumguides.org/

Villacura, E. (2026a). *BACKLOG\_MAESTRO.md* [documento interno del proyecto Baxy].
Repositorio del proyecto.

Villacura, E. (2026b). *Baxy — Estado técnico y límites de hardware* [documento
interno]. Repositorio del proyecto.

Villacura, E. (2026c). *vram\_real\_medida.csv* [conjunto de datos de mediciones]. Repositorio
del proyecto.

*Fuente: Elaboración propia (2026).*
