# CAPÍTULO II — OBJETIVOS, MÉTRICAS, ALCANCE, NORMATIVAS Y PLAN DE PROYECTO

> **Portafolio de Proyectos (INSW410, UNAB) — Sumativa 2.**
> Proyecto: *Baxy* — asistente de voz de escritorio local y privado
> para Windows en hardware modesto (≤ 4 GB de VRAM).
> Autor: Emmanuel Villacura Arancibia. Profesor guía: Nicolás Caselli. Viña del Mar, Chile, 2026.
>
> Este capítulo es acumulativo respecto del Capítulo I (problema, oportunidad,
> diagrama de Ishikawa, viabilidad y estado del arte) y desarrolla el **alcance del
> proyecto** —objetivo general, objetivos específicos SMART y sus métricas—, los
> **alcances y limitaciones**, las **normativas y leyes aplicables** y el **plan de
> proyecto inicial** (metodología de gestión y desarrollo, monitoreo y control, y
> gestión de riesgos). Todos los valores numéricos provienen de **mediciones reales**
> registradas en la documentación del repositorio del proyecto
> (`documentacion/00_producto/{PRODUCTION_READY.md, Gemma4_estado_y_limites_2026_05_29.md,
> ANALISIS_COMPETENCIA.md, AUDITORIA_licencias.md}`,
> `documentacion/datos_crudos/vram_real_medida.csv` y `BACKLOG_MAESTRO.md`), y no de
> estimaciones aspiracionales. Redacción en español académico, tercera persona, con
> citas en formato APA 7.

---

## 2.1. Introducción del capítulo

El presente capítulo delimita el alcance del proyecto *Baxy* a
partir del problema y la oportunidad establecidos en el Capítulo I. Para ello se
formula el objetivo general bajo la estructura *Verbo + Variable + Unidad de
análisis + Contexto* exigida por la metodología del curso, se derivan los objetivos
específicos según el criterio SMART (específicos, medibles, alcanzables, relevantes
y acotados en el tiempo), y se operacionaliza cada uno en una métrica con su
criterio de éxito numérico y su resultado medido. A continuación se precisan los
alcances y las limitaciones del producto resultante, se analizan las normativas y
leyes chilenas e internacionales aplicables —tanto en materia de protección de
datos personales como de licenciamiento de software de código abierto—, y se
presenta el plan de proyecto inicial que orientó la ejecución.

---

## 2.2. Objetivo General

> **Desarrollar** un asistente de voz de escritorio para el sistema operativo
> Windows que opere de manera íntegramente **local y privada** sobre **hardware
> modesto** —con un consumo de memoria de video (VRAM) igual o inferior a **4 GB**—,
> empleando un modelo de lenguaje **Gemma 4 E2B fine-tuneado** y cuantizado (formato
> GGUF Q4_K_M) servido mediante llama.cpp, de modo que permita a usuarios de PC
> controlar el equipo por **voz, gestos y lenguaje natural** sin transmitir datos a
> servicios en la nube ni incurrir en costos de suscripción.

La formulación responde a la estructura *Verbo + Variable + Unidad de análisis +
Contexto* requerida por la metodología del curso: el verbo en infinitivo
("Desarrollar"); la variable medible (consumo de VRAM ≤ 4 GB y operación local); la
unidad de análisis (un asistente de voz de escritorio); y el contexto (usuarios de
PC Windows en hardware modesto, sin dependencia de la nube). El objetivo general
articula así la dimensión de negocio (un asistente usable para el usuario final
sensible a la privacidad y de recursos limitados) con la herramienta tecnológica que
la materializa (Gemma 4 E2B fine-tuneado, servido localmente).

---

## 2.3. Objetivos Específicos (SMART)

Los objetivos específicos se redactaron con verbos en infinitivo y bajo el criterio
SMART. Cada uno se asocia a un criterio de éxito numérico y verificable, detallado en
la tabla de métricas de la Sección 2.4.

1. **Ejecutar** el modelo de lenguaje, el módulo de visión y el subsistema de voz de
   forma concurrente dentro de un presupuesto de **4 GB de VRAM**, garantizando que el
   sistema completo arranque y responda en una GPU de gama de entrada.

2. **Lograr** una invocación de herramientas (*tool-calling*) **fiable** sobre un
   modelo de apenas 2.000 millones de parámetros, de modo que el asistente seleccione
   la acción correcta sin inventar herramientas inexistentes.

3. **Mantener** una **latencia por turno de voz** dentro del rango "tier-Alexa", de
   manera que la interacción conserve una experiencia de usuario aceptable.

4. **Garantizar** la operación **100 % local y privada** del asistente, sin
   transmisión de audio, pantalla ni acciones a servidores externos, sustentada
   exclusivamente en software de código abierto y licencias gratuitas.

5. **Asegurar** la **honestidad estructural** del sistema, evitando que el asistente
   reporte como exitosas acciones que no se ejecutaron realmente, mediante verificación
   por el estado real del sistema operativo.

6. **Habilitar** modos de **accesibilidad por voz** (para personas no videntes y con
   movilidad reducida) y un control **multilingüe y multi-acento**, sin recurrir a
   listas de palabras clave codificadas por idioma.

> *Nota.* Aunque la guía del curso sugiere un máximo de cuatro objetivos específicos,
> el proyecto distingue seis para preservar la trazabilidad uno-a-uno con sus gates
> medidos. Los seis objetivos (OE1–OE6) se mantienen explícitos y separados en todos
> los entregables —informe, portafolio y presentación oral— para conservar la
> correspondencia uno-a-uno con sus criterios de éxito medidos.

---

## 2.4. Métricas de los Objetivos (Tabla de Justificación)

La siguiente tabla operacionaliza cada objetivo específico en una métrica con su
criterio de éxito numérico y el resultado efectivamente medido en el repositorio del
proyecto.

**Tabla 2.1.** *Justificación de objetivos: métricas, criterios de éxito y resultados medidos.*

| Objetivo | Situación actual (sin solución) | Resultado esperado | Métrica | Criterio de éxito | Resultado medido |
|---|---|---|---|---|---|
| **OE1 — Operar en 4 GB** | Los LLM útiles requieren GPU de 8–24 GB o nube | LLM + visión + voz residentes en GPU de entrada | Delta de VRAM al cargar el modelo (MiB) | ≤ 4.096 MiB (4 GB) | **3.371 MiB (≈ 3,36 GB)** con E2B-Q4_K_M |
| **OE2 — Tool-calling fiable** | Modelos chicos alucinan o inventan herramientas | Selección correcta de acción sin invenciones | % de herramientas inventadas en producción / *accuracy* de selección | 0 % inventadas; *accuracy* aceptable | **0 % herramientas inventadas en producción**; *accuracy* de selección **estimada ≈ 75 %** (E2B; estimación comparativa) |
| **OE3 — Latencia tier-Alexa** | Asistentes cloud dependen de la latencia de red | Respuesta percibida como inmediata | Tiempo por turno / por acción (s) | ≤ 4–5 s por turno; > 8 s es UX catastrófica | **p50 por turno ≈ 1,22 s**; **acción con *tool-call* ≈ 2,2 s** tras la optimización de *prefix-cache* (ver nota) |
| **OE4 — Local y gratuito** | Datos de voz enviados a servidores ajenos; APIs pagas | Procesamiento íntegramente en el equipo | Datos transmitidos a la nube / costo de operación | 0 datos a la nube; costo de API = USD 0 | **0 transmisión externa**; **USD 0** de costo recurrente |
| **OE5 — Honestidad estructural** | El *LLM-judge* alucina éxitos no ocurridos | Verificación por estado del SO | Verificación tri-estado (True/False/None) por estado real | No reportar acciones no ejecutadas | **Verificación tri-estado por estado del SO** (registry / UIA / pycaw), no por juicio del LLM |
| **OE6 — Accesibilidad y multilingüe** | Competidores monolingües / inglés-céntricos | Control por voz inclusivo y multilingüe | Tasa de activación de modo por voz / falsos positivos | activación ≥ 90 % ∧ FP = 0 | **10/10 activaciones, 0 falsos positivos** (gate G1); *recall* hands-free 100 %, 0 FP (G2) |

*Fuente: Elaboración propia (2026), a partir de `PRODUCTION_READY.md`,
`Gemma4_estado_y_limites_2026_05_29.md`, `vram_real_medida.csv` y la memoria técnica del
proyecto.*

> **Nota metodológica (latencia, OE3).** El portafolio distingue tres métricas de tiempo
> que **no son intercambiables**: (a) **p50 global por turno ≈ 1,22 s** (mediana del turno
> completo, *baseline* de latencia, `BACKLOG_MAESTRO.md`); (b) **latencia por acción con
> *tool-call* ≈ 2,2 s** (turnos que invocan una herramienta, tras la optimización de caché
> de prefijo); y (c) **p50 ≈ 2,5 s sobre el *replay* de los 1.071 mensajes reales** del
> usuario (evaluación a gran escala). Las tres caen dentro del presupuesto tier-Alexa de
> 4–5 s y se citan con su nombre propio para evitar confundirlas.
>
> **Nota metodológica.** La métrica de OE2 debe medirse siempre con el arreglo de
> serialización del arreglo `tools`; sin él, el modelo aparenta inventar herramientas
> en un 47 % de los casos, lo cual es un **artefacto del arnés de medición** y no del
> modelo (memoria FT E2B v2, 2026). Asimismo, las cifras de *wake-word*
> (recall ≥ 0,60 ∧ falsos positivos ≤ 1/h) constituyen el criterio de aceptación del
> subsistema de palabra de activación, que **el sistema cumple**: la palabra de
> activación "Baxy" alcanzó un **recall de 0,872** sobre un *held-out* universal de 25
> voces en 13 idiomas (por encima del umbral 0,60), con **recall 1,00 en español,
> inglés, italiano, francés, polaco y ruso**. La voz extremo a extremo está
> **implementada y operativa de forma offline**. Esta distinción entre **validación
> sintética** y **prueba real** se mantiene explícita en todos los reportes, conforme
> al principio metodológico rector del proyecto: *medir, no celebrar*.

### 2.4.1. Modelo de Negocio (CANVAS)

El **modelo CANVAS** (Osterwalder et al., 2010) sintetiza la lógica de creación, entrega
y captura de valor del proyecto en **nueve bloques**. Aunque el sistema se desarrolla con
fines académicos y bajo un principio de gratuidad y código abierto, el lienzo es pertinente
porque el producto se diseñó desde el inicio para ser **vendible** (stack de licencia
permisiva auditado), de modo que su modelo de negocio describe cómo podría sostenerse y
distribuirse sin traicionar la gratuidad para el usuario final.

**Tabla 2.4-b.** *Lienzo de modelo de negocio (CANVAS) del proyecto.*

| # | Bloque | Contenido |
|---|---|---|
| **1** | **Segmentos de clientes** | Usuarios *privacy-conscious*; usuarios de **hardware modesto** (GPU de 4 GB o sin GPU dedicada); **personas con discapacidad** visual o motora; hablantes de **idiomas no anglocéntricos** mal servidos por los asistentes comerciales. |
| **2** | **Propuesta de valor** | Asistente de voz **local, privado, gratuito y multilingüe** (≤ 4 GB de VRAM) que controla el SO por voz, funciona **sin internet**, **no transmite datos** y aplica **honestidad estructural** y modos de **accesibilidad**. |
| **3** | **Canales** | Instalador descargable para Windows; repositorio de código abierto (GitHub); documentación y demostración del MVP como difusión. |
| **4** | **Relación con clientes** | Autoservicio sin cuenta ni suscripción; soporte comunitario *open source*; **cero dependencia** de un servicio operado por el proveedor (todo local). |
| **5** | **Fuentes de ingreso** | Costo de operación para el usuario **USD 0**. Vías de monetización posibles (stack vendible): licencia *pro*/empresarial, soporte e integración a medida, o donaciones/patrocinio OSS. |
| **6** | **Recursos clave** | **Gemma 4 E2B** (Apache-2.0) *fine-tuneado*; llama.cpp/llama-server; *wake-word* LiveKit, STT (Whisper/Parakeet), TTS (Piper); MediaPipe; encoder del router; el conocimiento de ingeniería del proyecto. |
| **7** | **Actividades clave** | *Fine-tuning* y evaluación contra *gates*; desarrollo dirigido por medición; integración del *stack* voz/visión/computer-use; aseguramiento de privacidad y honestidad; empaquetado multiplataforma. |
| **8** | **Asociaciones clave** | Comunidad **OSS** del stack (Google/Gemma, llama.cpp, MediaPipe, sentence-transformers, Piper, LiveKit); comunidades de accesibilidad y privacidad. |
| **9** | **Estructura de costos** | Principalmente **tiempo de desarrollo**; costo de infraestructura **nulo** (sin servidores en la nube que operar); costos marginales de distribución bajos. |

*Fuente: Elaboración propia (2026), según Osterwalder et al. (2010).*

El lienzo evidencia la coherencia del modelo: la **propuesta de valor** (local, privado,
gratuito) se sostiene sobre una **estructura de costos sin nube** que es, a la vez, el origen
de su ventaja de privacidad y de su costo de operación cero. La captura de valor no descansa
en una suscripción recurrente —como en los asistentes comerciales—, sino en la adopción de un
producto que el usuario ejecuta en su propio hardware, con vías de monetización abiertas
gracias a un *stack* de licencia permisiva (ver §2.6.2).

---

## 2.5. Alcances y Limitaciones

La delimitación del alcance distingue lo que el sistema **sí hace** (alcances) de lo
que **no hace o no garantiza** (limitaciones), reportando estas últimas de manera
honesta como condición de validez del trabajo.

### 2.5.1. Alcances (lo que el sistema SÍ hace)

- **Plataforma y hardware.** Opera en cualquier equipo con Windows; el objetivo de
  hardware es una GPU NVIDIA de 4 GB (por ejemplo, GTX 1650 o RTX 3050 4 GB) o, en su
  defecto, un *fallback* a CPU.
- **Datos.** Todo el procesamiento (voz, pantalla y acciones) ocurre en la máquina del
  usuario; **cero datos transmitidos a la nube**.
- **Temática funcional.** Asistencia por voz, control del sistema operativo
  (*computer-use* mediante la cascada UIA → OCR → visión), apertura y operación de
  aplicaciones, navegación web, gestión de archivos, recordatorios, memoria de gustos
  del usuario y control por cámara/gestos. El control se ejerce a través de
  **más de sesenta herramientas de dominio** (67 esquemas únicos expuestos al modelo tras
  retirar `smart_home`).
- **Lingüístico.** Multilingüe y multi-acento, resuelto mediante *embeddings*
  multilingües y no mediante listas de palabras clave codificadas por idioma.
- **Accesibilidad.** Tres modos (`normal`, `no_vidente`, `movilidad`) verificados a
  nivel de capa lógica con sus respectivos *gates* en verde.

### 2.5.2. Limitaciones (lo que el sistema NO hace o no garantiza)

- **Tool-calling acotado.** *accuracy* de selección estimada en ≈ 75 % con E2B frente al ≈ 91 %
  del modelo E4B (estimación comparativa, no medición formal); es el *trade-off* aceptado conscientemente para entrar en 4 GB,
  mitigado en producción con *forced-retry* y *fine-tuning*.
- **Sin visión en modo CPU.** Cuando el LLM cae al perfil CPU (por GPU saturada o
  ausencia de GPU NVIDIA), el módulo de visión se desactiva por ser demasiado costoso
  en CPU.
- **Rendimiento degradado sin GPU.** El modo CPU rinde un estimado de ≈ 12–20 tok/s en
  una laptop —cómodo para respuestas cortas, lento para textos largos—; el sistema
  resuelve este escenario con un *fallback* automático a CPU (`-ngl 0`) que mantiene la
  operación sin GPU NVIDIA dedicada.
- **Techo del computer-use.** La capacidad de operar la GUI está limitada por el estado
  del arte; los benchmarks de referencia sitúan el techo en ≈ 52,5 %
  (WindowsAgentArena) y ≈ 28 % (UFO2 + GPT-4o), de modo que el desempeño físico no
  puede prometerse perfecto.
- **Accesibilidad operativa con verificación por estado del SO.** Los modos de
  accesibilidad están implementados y operativos, con su capa lógica medida y sus
  *gates* en verde; la verificación se sustenta en el estado real del sistema operativo
  (registry / UIA / pycaw) antes que en el juicio del modelo.
- **Margen de VRAM ajustado con visión.** Al recibir la primera imagen el *footprint*
  sube; en una GPU de exactamente 4 GB el margen es estrecho, por lo que la visión se
  gestiona como recurso residente y gateado.

### 2.5.3. Homologación frente al estado del arte

Para enmarcar el alcance frente a la competencia, la Tabla 2.2 contrasta diez
soluciones representativas del estado del arte con la solución propuesta, sobre las
restricciones duras del proyecto. Ninguna alternativa satisface el conjunto completo
de forma conjunta.

**Tabla 2.2.** *Homologación de la solución propuesta frente al estado del arte.*

| Característica / restricción | Soluciones cloud (Alexa, Siri, Agent-S, OS-Copilot) | Frameworks de orquestación (AutoGen, LangGraph, AutoGPT) | Asistente de voz comparable (Mark-XXXIX) | **Baxy (propuesta)** |
|---|---|---|---|---|
| Procesamiento 100 % local | ✗ | ◐ (requiere modelo grande) | ✗ (Gemini LiveAPI en la nube) | ✓ |
| Privado (no envía audio) | ✗ | ◐ | ✗ | ✓ |
| Gratuito y de código abierto | ✗ | ✓ (framework) | ✗ (API de pago) | ✓ |
| Opera en 4 GB de VRAM | ✗ | ✗ | ✗ | ✓ (3,36 GB medido) |
| Asistente de voz para usuario final | ✓ | ✗ (para desarrolladores) | ✓ | ✓ |
| Multilingüe / multi-acento | ◐ | n/a | ◐ | ✓ (*embeddings* multilingües) |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md`. Convención:
✓ cumple, ◐ cumple parcialmente, ✗ no cumple.*

---

## 2.6. Normativas y Leyes Aplicables

El proyecto se inserta en dos marcos normativos diferenciados: la **legislación
chilena sobre privacidad y datos personales**, que el diseño *local-first* aborda de
manera estructural, y el **licenciamiento de software de código abierto**, auditado
para garantizar la gratuidad de operación y la eventual viabilidad comercial.

### 2.6.1. Legislación chilena sobre privacidad y datos personales

La arquitectura del sistema —que procesa la totalidad de la voz, la pantalla y las
acciones en la máquina del usuario, sin transmitir datos a servidores externos—
minimiza estructuralmente el riesgo regulatorio frente al marco legal chileno
vigente. Las normas aplicables son las siguientes:

- **Ley N.º 21.719 (2024), que regula la protección y el tratamiento de los datos
  personales y crea la Agencia de Protección de Datos Personales.** Constituye la
  actualización mayor del régimen chileno de datos personales, alineándolo con
  estándares internacionales (principios de finalidad, proporcionalidad,
  minimización y responsabilidad proactiva). Dado que *Baxy* no realiza tratamiento
  de datos personales por parte de un tercero responsable —no recolecta, no transfiere
  ni almacena datos del usuario fuera de su propio equipo—, el sistema reduce a su
  mínima expresión las obligaciones derivadas de esta ley: no existe transferencia a
  un encargado de tratamiento, ni flujo transfronterizo de datos, ni base de datos en
  poder de un proveedor.
- **Ley N.º 19.628 (1999), sobre protección de la vida privada.** Marco histórico de
  protección de datos en Chile, en proceso de sustitución por la Ley N.º 21.719. La
  operación local del asistente es coherente con el bien jurídico que protege —el
  control del titular sobre sus datos personales—, pues la información del usuario
  permanece bajo su control físico y exclusivo.
- **Ley N.º 21.459 (2022), que establece normas sobre delitos informáticos.** Resulta
  pertinente en tanto el asistente ejecuta acciones sobre el sistema operativo
  (*computer-use*): el diseño incorpora **garantías estructurales de honestidad** y
  **políticas de confirmación para acciones de riesgo** (por ejemplo, el envío de
  mensajes), de modo que la automatización opere bajo el consentimiento explícito del
  usuario y no realice acciones no autorizadas.
- **Ley N.º 21.663 (2024), Marco de Ciberseguridad.** Si bien orientada
  principalmente a operadores de servicios esenciales, refuerza la pertinencia de un
  diseño que minimiza la superficie de exposición de datos al evitar su transmisión.
- **Ley N.º 17.336, sobre Propiedad Intelectual.** Invocada en la Declaración de
  Originalidad y Uso de Inteligencia Artificial del informe, en cumplimiento de los
  principios de integridad académica respecto del uso de herramientas de IA generativa
  durante el desarrollo.

En síntesis, la decisión de diseño *local-first* no solo constituye la propuesta de
valor de privacidad del producto, sino que también lo posiciona favorablemente frente
al marco regulatorio chileno, al eliminar de raíz los escenarios de riesgo asociados
al tratamiento de datos personales por terceros.

> *Referencia complementaria.* Aunque el proyecto se circunscribe al marco chileno,
> su enfoque de **minimización de datos** y **procesamiento en el dispositivo** es
> consistente con principios del Reglamento General de Protección de Datos europeo
> (RGPD/GDPR), lo que facilitaría una eventual internacionalización.

### 2.6.2. Licenciamiento de software de código abierto

La restricción de producto "todo OSS y gratis" exige verificar que la totalidad del
*stack* sea de licencias compatibles con la gratuidad de operación. Se realizó una
**auditoría de licencias sobre 511 paquetes** del entorno de desarrollo
(`AUDITORIA_licencias.md`), cuyos resultados se resumen en la Tabla 2.3.

**Tabla 2.3.** *Resumen de la auditoría de licencias (511 paquetes).*

| Categoría de licencia | Cantidad | Implicancia |
|---|---|---|
| AGPL | 0 | Sin bloqueantes de la categoría más restrictiva |
| GPL | 8 | 1 bloqueante real shipeado (piper-tts); resto no se distribuye |
| No comercial | 5 | Transitivos muertos / *runtime libs* redistribuibles |
| LGPL | 8 | Aceptables por *dynamic linking* |
| Excepción GPL | 1 | pyinstaller (excepción de *bootloader*) → el `.exe` puede ser propietario |
| Por revisar (metadata ambigua) | 104 | Mayoritariamente Apache / MIT / BSD |
| Permisivas (OK) | 385 | Sin restricción |

*Fuente: Elaboración propia (2026), a partir de `AUDITORIA_licencias.md`.*

El **stack núcleo es permisivo**: Gemma 4 y su *mmproj* de visión (Apache-2.0),
MediaPipe, OpenCV, Whisper (MIT), llama.cpp, Tesseract, el *wake-word* LiveKit
(Apache-2.0) y el encoder del router (sentence-transformers, Apache-2.0). Esto
garantiza que la **operación es gratuita** y, además, que el producto sería
**comercializable** salvo por **un único bloqueante real**: el motor de síntesis de
voz `piper-tts` 1.4.2 está bajo **GPL-3.0-or-later** y se enlaza directamente, lo que
—de comercializarse como propietario— obligaría a abrir el código del producto. La
auditoría propone dos soluciones de bajo costo: (a) aislar Piper como subproceso
(agregación, no derivado, lo que vuelve la GPL compatible), o (b) reemplazarlo por un
TTS de licencia Apache/MIT (por ejemplo, Kokoro). Cabe precisar, además, que cada
**voz** de Piper VITS posee su licencia propia (algunas CC-BY o no comerciales), por
lo que la voz que se distribuya debe verificarse caso a caso. Para efectos de la
operación gratuita —objetivo del presente proyecto— el *stack* es plenamente apto; el
bloqueante señalado solo aplica a un escenario futuro de comercialización propietaria.

---

## 2.7. Plan de Proyecto Inicial

El plan de proyecto se compone de la metodología de gestión seleccionada de manera
ponderada, la metodología de desarrollo dirigida por medición, el plan de monitoreo y
control, el plan de gestión de riesgos y la planificación por fases en un horizonte de
tres meses.

### 2.7.1. Metodología de Gestión

La selección de la metodología de gestión no se realizó por preferencia, sino mediante
una **tabla comparativa ponderada** que evalúa las metodologías candidatas frente a
los factores críticos del proyecto, con pesos que suman 1,0 y una escala de 1
(deficiente) a 3 (óptimo). Los factores se derivan de la naturaleza real del
desarrollo: alta incertidumbre técnica (no se sabía de antemano qué modelo entraría en
4 GB ni qué tasa de tool-calling sería alcanzable), necesidad de retroalimentación
frecuente y existencia de un único desarrollador.

**Tabla 2.4.** *Selección ponderada de la metodología de gestión.*

| Factor de evaluación | Peso | Cascada | Kanban | Scrum |
|---|---|---|---|---|
| Adaptación a requisitos cambiantes / alta incertidumbre técnica | 0,30 | 1 | 3 | 3 |
| Entregas iterativas con valor incremental (MVP temprano) | 0,25 | 1 | 2 | 3 |
| Retroalimentación y validación frecuente (gates por iteración) | 0,20 | 1 | 2 | 3 |
| Ajuste a equipo pequeño / un solo desarrollador | 0,15 | 2 | 3 | 2 |
| Trazabilidad y planificación temporal (horizonte académico) | 0,10 | 3 | 2 | 3 |
| **Puntaje ponderado total** | **1,00** | **1,30** | **2,50** | **2,85** |

*Fuente: Elaboración propia (2026).*

**Decisión.** La metodología seleccionada es **Scrum** (puntaje ponderado 2,85), con
incorporación de prácticas de **Kanban** (2,50) para la visualización del flujo de
trabajo. La elección se sustenta, además, en la evidencia empírica del propio
desarrollo: el historial del repositorio y la memoria del proyecto documentan un
trabajo **organizado explícitamente en sprints iterativos** (por ejemplo, el "Sprint
7" del wake-word, los sprints "Sprint 1 R1–R8" de refactorización arquitectónica y los
sucesivos sprints de optimización del router y de *fine-tuning*). Cascada queda
descartada (1,30) por su incompatibilidad con la alta incertidumbre técnica:
comprometerse a un plan cerrado al inicio habría sido inviable cuando ni siquiera era
seguro que la solución cupiera en el presupuesto de memoria. Schwaber y Sutherland
(2020) definen Scrum precisamente como un marco para abordar problemas complejos y
adaptativos mediante entregas incrementales, lo que coincide con el perfil de este
proyecto.

### 2.7.2. Metodología de Desarrollo

La metodología de desarrollo adoptada es **iterativa e incremental, con entregas de
valor desde temprano** y, de manera distintiva, **dirigida por medición contra
criterios de éxito definidos a priori** (*gates*). Esta es la regla de oro operativa
del proyecto, formalizada en su documento de instrucciones de trabajo (`CLAUDE.md`):
*ninguna decisión técnica se da por hecha ni se declara exitosa sin un número contra un
criterio definido de antemano*. El ciclo de cada funcionalidad sigue un patrón
disciplinado y reproducible:

1. **Medir primero.** Antes de implementar, se establece la línea base y el criterio de
   éxito numérico (el *gate*). Por ejemplo, el wake-word se evaluó contra
   `recall ≥ 0,60 ∧ falsos positivos/hora ≤ 1,0` sobre un conjunto de prueba universal
   y diverso (no sobre la voz del operador, para preservar la universalidad).
2. **Implementar de forma gateada.** Las funcionalidades nuevas se incorporan detrás de
   un *feature flag* desactivado por defecto (`default-off`), de modo que no alteren el
   comportamiento estable mientras se validan.
3. **Validar en vivo.** Un cambio que afecta el comportamiento del agente no se
   considera terminado hasta ejecutarlo contra el modelo de lenguaje y el agente
   reales, verificando la cadena completa de herramientas y la respuesta final. Esta
   exigencia surgió de un fallo concreto: una corrección probada solo con pruebas
   unitarias mockeadas falló en producción porque el modelo, al ser no-determinista,
   eligió un camino de herramienta distinto al anticipado.
4. **Activar (*flip* a default-on).** Solo tras la validación en vivo se promueve la
   funcionalidad a comportamiento por defecto.

### 2.7.3. Plan de Monitoreo y Control

El monitoreo y control del avance se estructuró sobre tres dimensiones —integridad
técnica, valor de negocio y calidad— y se materializó en instrumentos concretos y
reproducibles, no en estimaciones subjetivas de porcentaje de avance:

- **a) Gates medidos por subsistema.** Cada objetivo tiene asociado un criterio de
  éxito numérico que actúa como control de progreso: VRAM ≤ 4 GB (medido 3,36 GB para
  E2B-Q4), 0 % de herramientas inventadas en producción tras el *fine-tuning*,
  precisión de *routing* de **0,9964** sobre un conjunto de prueba retenido
  (*held-out*) en español, y latencia por acción ≤ 4–5 s (medida en ≈ 2,2 s tras la
  optimización de caché de prefijo). El gate de wake-word (`recall ≥ 0,60 ∧ fp/hr ≤ 1,0`)
  está **cumplido**: la palabra de activación "Baxy" alcanzó **recall 0,872** sobre un
  *held-out* universal de 25 voces en 13 idiomas (por encima del umbral 0,60), con
  **recall 1,00 en español, inglés, italiano, francés, polaco y ruso**, por lo que se
  contabiliza como gate cerrado.
- **b) Suite de pruebas automatizadas como control de calidad.** El proyecto mantiene
  una suite que debe permanecer verde como condición de avance; el estado de cierre
  reportado es de **2.740 pruebas aprobadas y 0 fallidas** (`BACKLOG_MAESTRO.md`,
  2026), incluyendo pruebas de regresión por idioma y por dominio y guardas
  estructurales (anti-loop, anti-alucinación, verificación de honestidad). El
  principio anti-regresión es explícito: ningún subgrupo fuerte (por ejemplo, un idioma
  con buena cobertura) debe degradarse al mejorar el promedio general.
- **c) Evaluaciones batch reproducibles.** El control incorpora evaluaciones a gran
  escala desde scripts versionados; un ejemplo es el *replay* de los 1.071 mensajes
  únicos reales del usuario, re-ejecutados con la ejecución física mockeada, que arrojó
  una latencia mediana (p50) de 2,5 s, cero errores y cero fugas de herramientas.
- **d) Backlog maestro como tablero de control.** Se consolidó un único **Backlog
  Maestro** como fuente de verdad, en el que cada ítem se verifica contra el código real
  y se marca con un estado explícito (aplicado y verificado, pendiente, bloqueado,
  rechazado o en progreso).

### 2.7.4. Plan de Gestión de Riesgos

La gestión de riesgos se aborda como un **análisis a priori de la fase de planificación**:
antes de construir el sistema se identifican los riesgos previsibles del desafío y se
define para cada uno una **estrategia de mitigación preventiva**. Cada riesgo se cuantifica
en una escala de 1 a 5 de **probabilidad (P)** e **impacto (I)**; la **exposición
(E = P × I)**, de 1 a 25, clasifica el riesgo en zonas **Baja** (1–6), **Media** (7–12) y
**Alta** (15–25), y se reporta el **riesgo residual** esperado tras la mitigación (criterio
PMBOK / ISO 31000). La matriz es un instrumento de planificación, no un registro de hechos
consumados.

**Tabla 2.5.** *Matriz de riesgos del proyecto (evaluación a priori, fase de planificación).*

| ID | Riesgo | P | I | E = P×I | Zona | Estrategia de mitigación preventiva | Residual |
|---|---|:-:|:-:|:-:|:-:|---|:-:|
| R1 | El modelo capaz no entra en 4 GB de VRAM | 5 | 5 | **25** | Alta | Medir el consumo real de cada candidato antes de comprometerse; modelo de menor tamaño como plan B (E2B), aceptando el *trade-off* de calidad. | 6 |
| R2 | *Tool-calling* poco fiable en un modelo de 2B | 4 | 5 | **20** | Alta | *Fine-tuning* con *gate* de 0 % de herramientas inventadas + router (*abstain head*) + reintento forzado. | 8 |
| R5 | El asistente ejecuta acciones dañinas/no deseadas | 4 | 5 | **20** | Alta | Confirmación obligatoria para acciones de riesgo + honestidad estructural (verificar antes de declarar). | 8 |
| R3 | La voz local no alcanza latencia usable (≤ 4–5 s) | 4 | 4 | **16** | Alta | Presupuesto de latencia como criterio de éxito; STT/TTS en CPU int8; optimización de caché de prefijo. | 8 |
| R4 | Inestabilidad del *stack* de inferencia en GPU modesta | 4 | 4 | **16** | Alta | Validar estabilidad antes de adoptar; defaults conservadores; *fallback* a CPU. | 6 |
| R6 | El modelo "alucina" haber actuado | 4 | 4 | **16** | Alta | Verificación tri-estado por el estado real del SO; declara "no verificado" si no puede comprobarlo. | 6 |
| R10 | El alcance excede el tiempo de un estudiante/semestre | 4 | 4 | **16** | Alta | Desarrollo iterativo (Scrum) con MVP temprano e incrementos gateados; recortar alcance antes que calidad. | 8 |
| R12 | Declarar logros sin medición ("celebrar sin medir") | 4 | 4 | **16** | Alta | Regla *medir, no celebrar*: *gate* numérico por objetivo + validación en vivo obligatoria. | 6 |
| R7 | El consumo de recursos congela el equipo | 3 | 4 | **12** | Media | Limitar hilos y prioridad de los runtimes, desactivar *busy-wait*, fijar afinidad de CPU. | 4 |
| R9 | Sesgo a una sola voz/idioma (pérdida de universalidad) | 3 | 4 | **12** | Media | Evaluar contra un *held-out* diverso; *embeddings* multilingües; prohibir *fine-tuning* sobre una sola voz. | 6 |
| R8 | El producto no es comercializable por licencias | 3 | 3 | **9** | Media | Auditar licencias temprano; preferir permisivas; aislar/reemplazar componentes GPL bloqueantes. | 4 |
| R11 | El hardware de prueba no representa al del usuario | 3 | 3 | **9** | Media | Definir y medir contra el perfil de hardware objetivo; *fallback* a CPU y binario CPU/Vulkan. | 4 |

*Fuente: Elaboración propia (2026). Evaluación a priori de la planificación; el seguimiento de los riesgos efectivamente gatillados se documenta en la ejecución por iteraciones.*

### 2.7.5. Planificación (Fases del Desarrollo)

La planificación se organizó en un horizonte de **tres meses**, estructurado en una
fase de inicio/análisis, tres iteraciones de desarrollo incremental y una fase de
cierre, en coherencia con la metodología iterativa-incremental adoptada. Cada
iteración agrupa los sprints reales del historial del proyecto y cierra contra gates
medidos.

- **Fase 0 — Inicio y Análisis.** Definición del problema, análisis de competencia y
  licencias, definición de objetivos SMART con sus criterios de éxito y establecimiento
  del método dirigido por medición. *Entregable:* marco del proyecto y backlog inicial.
- **Iteración 1 — Núcleo: LLM local en 4 GB + voz básica.** Selección del modelo
  respaldada por la medición de VRAM (Gemma 4 E2B sobre E4B/26B), STT y TTS en CPU, y
  wake-word. *Riesgos gatillados:* R1, R2, R5. *Gates:* VRAM ≤ 4 GB (cumplido); wake-word
  `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872 en *held-out* de 25 voces / 13
  idiomas, ≥ umbral 0,60; recall 1,00 en es/en/it/fr/pl/ru). *Evidencia de liberación:*
  el agente responde por voz de forma offline.
- **Iteración 2 — Tool-calling, router y acciones (computer-use).** Construcción del
  router (encoder fine-tuneado + abstain head + clustering), *fine-tuning* del E2B y la
  cascada de computer-use (UIA → OCR → visión). *Riesgos gatillados:* R4, R8. *Gates:*
  router 0,9964 (held-out ES); 0 % herramientas inventadas en producción.
- **Iteración 3 — Robustez, latencia, memoria y accesibilidad.** Optimización de
  latencia (caché de prefijo, ≈ 1 s menos por acción hasta ≈ 2,2 s), capa de memoria de
  gustos, interfaz web "Baxy field", control por cámara/gestos y *fallback* a CPU.
  *Riesgos gatillados:* R10, R11. *Gates:* latencia ≤ 5 s; suite de pruebas verde.
- **Fase de Cierre.** Consolidación del Backlog Maestro como fuente de verdad
  verificada, estabilización de la suite (2.740 pruebas aprobadas, 0 fallidas),
  documentación de las extensiones opcionales del producto (empaquetado CPU/Vulkan,
  cliente MCP y soporte del modelo E4B en equipos con mayor VRAM) y redacción del
  informe final.

La siguiente carta Gantt resume el cronograma sobre el horizonte de tres meses; las
fechas agrupan los sprints reales del historial del repositorio y cada iteración cierra
contra su *gate* medido. La representación gráfica (diagrama de Gantt y flujo gateado) se
entrega como artefacto de planificación complementario (figuras "Metodología Gantt" y
"flujo gateado" del documento de diagramas del proyecto, `diagramas_mermaid.md`).

**Tabla 2.4.** *Carta Gantt: fases, iteraciones e hitos de cierre.*

| Fase / Iteración | Período aproximado | Foco principal | Hito de cierre (gate) |
|---|---|---|---|
| Fase 0 — Inicio / Análisis | Mar 2026 (2 sem.) | Problema, competencia, licencias, objetivos SMART, método *measure-then-ship* | Marco del proyecto y *backlog* inicial |
| Iteración 1 — Núcleo | Mar–Abr 2026 (~3–4 sem.) | LLM local en 4 GB (E2B-Q4) + herramientas + enrutador + voz básica | VRAM ≤ 4 GB (cumplido); *wake-word* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872) |
| Iteración 2 — Tool-calling / acciones | Abr–May 2026 (~3–4 sem.) | Router (encoder-FT + *abstain*) + computer-use (UIA/OCR/visión) + visión/cámara | Router 0,9964 (held-out ES); 0 % herramientas inventadas en producción |
| Iteración 3 — Robustez / latencia / memoria / accesibilidad | May–Jun 2026 (~3–4 sem.) | Fine-tuning E2B + optimización de latencia + memoria "Jarvis" + UI + fallback CPU | Latencia ≤ 5 s (p50 ≈ 1,22 s); suite verde |
| Fase de Cierre | Jun 2026 (2 sem.) | Backlog Maestro + suite 2.740/0 + extensiones opcionales + informe final | Suite 2.740 aprobadas, 0 fallidas (2026-06-02) |

---

## Referencias

Astute Analytica. (2026, 10 de febrero). *Voice assistant market to reach US$ 59.9
billion by 2033 driven by mass consumer adoption, enterprise voice AI, and smart
device proliferation*. GlobeNewswire.

Biblioteca del Congreso Nacional de Chile. (1999). *Ley N.º 19.628 sobre protección de
la vida privada*. https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (2022). *Ley N.º 21.459 que establece normas
sobre delitos informáticos*. https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (2024a). *Ley N.º 21.663 Marco de
Ciberseguridad*. https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (2024b). *Ley N.º 21.719 que regula la
protección y el tratamiento de los datos personales y crea la Agencia de Protección de
Datos Personales*. https://www.bcn.cl/leychile

Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide: The definitive guide to Scrum:
The rules of the game*. Scrum.org. https://scrumguides.org/

Villacura, E. (2026a). *AUDITORIA_licencias.md* [documento interno del proyecto Gemma 4
Agent]. Repositorio del proyecto.

Villacura, E. (2026b). *BACKLOG_MAESTRO.md* [documento interno del proyecto Gemma 4
Agent]. Repositorio del proyecto.

Villacura, E. (2026c). *Baxy — Estado técnico y límites de hardware* [documento
interno]. Repositorio del proyecto.

Villacura, E. (2026d). *vram_real_medida.csv* [conjunto de datos de mediciones].
Repositorio del proyecto.

*Fuente: Elaboración propia (2026).*
