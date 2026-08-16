<div align="center">

# UNIVERSIDAD ANDRÉS BELLO

## FACULTAD DE INGENIERÍA

### INGENIERÍA CIVIL INFORMÁTICA

<br><br><br>

# ASISTENTE DE VOZ DE ESCRITORIO LOCAL Y PRIVADO PARA WINDOWS EN HARDWARE MODESTO (≤ 4 GB DE VRAM) MEDIANTE UN MODELO GEMMA 4 E2B FINE-TUNEADO

<br><br>

*Portafolio de Proyectos — Proyecto de Título para optar al título de Ingeniero Civil en Informática*

<br><br><br><br>

**Autor:** Emmanuel Villacura Arancibia

**Profesor guía:** Nicolás Caselli

**Asignatura:** INSW410 — Portafolio de Proyectos

<br><br><br><br>

Viña del Mar, Chile

2026

</div>

---

> *Nota de formato (para la edición final del documento).* Las páginas preliminares se
> numeran en romanos minúsculos (i, ii, iii, …) y el cuerpo en arábigos (1, 2, 3, …), en
> la esquina inferior derecha. Cada capítulo inicia en página nueva. Tamaño carta, fuente
> Arial o Times New Roman 12, interlineado 1,5, márgenes de 2,5 cm (inferior 3 cm) y texto
> justificado. Este documento corresponde al Portafolio completo: reúne las páginas
> preliminares y los tres capítulos acumulativos (Capítulos I, II y III) en un único
> archivo con índice y numeración coherente.

## Declaración de Originalidad y Uso de Inteligencia Artificial

El autor declara que el presente Proyecto de Título es de su autoría y que el
asistente de voz local *Baxy* fue **concebido, diseñado, arquitecturado,
implementado y validado por él**. La arquitectura del sistema, las decisiones
técnicas y la metodología de medición que sustenta cada resultado son trabajo
propio.

En cumplimiento de los principios de integridad académica y de la **Ley N.º 17.336
sobre Propiedad Intelectual** de la República de Chile, se declara que durante el
desarrollo se utilizaron herramientas de inteligencia artificial generativa
(Claude, de Anthropic, entre otras) como **apoyo puntual** —autocompletado y
borradores de código, y revisión de redacción de este documento—, siempre bajo la
dirección, revisión y validación empírica del autor. Su uso fue equivalente al de
cualquier herramienta de productividad de desarrollo; en ningún caso reemplazó el
criterio técnico ni la verificación de resultados, que el autor ejecutó y supervisó
conforme al principio rector del proyecto: *medir, no celebrar*. El autor asume la
responsabilidad íntegra del contenido, las decisiones de diseño y los resultados
reportados, todos verificables en el repositorio del proyecto.

<br>

_______________________________

Emmanuel Villacura Arancibia

Viña del Mar, 2026

---

## Resumen

Los asistentes de voz se han consolidado como una de las interfaces humano-computador
de mayor crecimiento; sin embargo, las soluciones dominantes del mercado operan bajo
un paradigma dependiente de la nube que transmite el audio del usuario a servidores
remotos, exige conectividad permanente, introduce latencia de red y, con frecuencia,
conlleva costos de suscripción. A partir de un análisis comparativo del estado del
arte, se identificó un vacío concreto: la inexistencia de un asistente de voz que
fuese, de manera simultánea, local, privado, gratuito, de código abierto, multilingüe
y capaz de operar en hardware modesto, específicamente en una tarjeta gráfica de
apenas cuatro gigabytes de memoria de video. El presente proyecto abordó dicho vacío
mediante el diseño y desarrollo de *Baxy*, un asistente de voz para
el sistema operativo Windows que opera íntegramente de forma local, empleando un
modelo de lenguaje Gemma 4 E2B ajustado mediante *fine-tuning*, cuantizado en formato
GGUF Q4_K_M y servido con la biblioteca de código abierto llama.cpp. El asistente
controla el sistema operativo a través de más de sesenta herramientas de dominio,
abarcando control por voz, visión por cámara, automatización de la interfaz mediante
una cascada de accesibilidad y reconocimiento óptico de caracteres, y modos de
accesibilidad para personas con movilidad reducida. Para la identificación de la
problemática se aplicó la técnica del diagrama de Ishikawa, y la gestión del trabajo
adoptó un enfoque iterativo-incremental dirigido por la medición sistemática contra
criterios de éxito definidos a priori, en el cual ninguna decisión técnica se dio por
válida sin evidencia cuantitativa. El diseño de alto nivel se documentó mediante el
modelo de vistas arquitectónicas 4+1, y la ejecución se organizó en tres iteraciones
sucesivas que abordaron, respectivamente, el núcleo del agente, la voz y la
percepción, y la optimización junto con la accesibilidad.

**Palabras clave:** asistente de voz local; modelos de lenguaje en el borde (*edge LLM*); Gemma 4; invocación de herramientas (*tool-calling*); *computer-use*; privacidad; accesibilidad.

---

## Abstract

Voice assistants have become one of the fastest-growing human-computer interfaces;
however, the market's dominant solutions operate under a cloud-dependent paradigm
that transmits the user's audio to remote servers, requires permanent connectivity,
introduces network latency, and frequently entails subscription costs. Based on a
comparative analysis of the state of the art, a concrete gap was identified: the
absence of a voice assistant that is, simultaneously, local, private, free,
open-source, multilingual, and capable of running on modest hardware—specifically on
a graphics card with only four gigabytes of video memory. This project addressed that
gap through the design and development of *Baxy*, a voice assistant
for the Windows operating system that operates entirely locally, using a Gemma 4 E2B
language model fine-tuned, quantized in GGUF Q4_K_M format, and served with the
open-source library llama.cpp. The assistant controls the operating system through
more than sixty domain tools, covering voice control, camera-based vision, interface
automation via an accessibility-and-optical-character-recognition cascade, and
accessibility modes for people with reduced mobility. The Ishikawa diagram technique
was applied to identify the problem, and project management adopted an
iterative-incremental approach driven by systematic measurement against
pre-defined success criteria, in which no technical decision was deemed valid without
quantitative evidence. The high-level design was documented using the 4+1
architectural view model, and execution was organized into three successive
iterations addressing, respectively, the agent core, voice and perception, and
optimization together with accessibility.

**Key words:** local voice assistant; edge large language models (edge LLM); Gemma 4; tool-calling; computer-use; privacy; accessibility.

---

## Tabla de Contenidos

> *Nota.* En la versión `.docx`, esta tabla se genera automáticamente desde los estilos
> de título (Word → Referencias → Insertar Tabla de Contenidos). Se reproduce aquí la
> estructura del portafolio con su numeración UNAB.

**Páginas preliminares**
- Portada
- Declaración de Originalidad y Uso de Inteligencia Artificial
- Resumen y Palabras Clave
- Abstract y Key Words
- Tabla de Contenidos
- Índice de Tablas e Índice de Figuras

**CAPÍTULO I — Problema / Oportunidad y Planteamiento**
- 1.1. Descripción del Problema / Oportunidad
- 1.2. Situación Actual
- 1.3. Identificación de la Problemática (Diagrama de Ishikawa)
- 1.4. Descripción de Causas (en qué consiste / qué efecto / cómo incide)
- 1.5. Fundamentación del Problema
- 1.6. Viabilidad del Proyecto (cinco dimensiones)
- 1.7. Estado del Arte
- Referencias del Capítulo I

**CAPÍTULO II — Objetivos, Métricas, Alcance, Normativas y Plan de Proyecto**
- 2.1. Introducción del capítulo
- 2.2. Objetivo General
- 2.3. Objetivos Específicos (SMART)
- 2.4. Métricas de los Objetivos (Tabla de Justificación)
  - 2.4.1. Modelo de Negocio (CANVAS)
- 2.5. Alcances y Limitaciones
- 2.6. Normativas y Leyes Aplicables
- 2.7. Plan de Proyecto Inicial
- Referencias del Capítulo II

**CAPÍTULO III — Metodología, Alternativas y Factibilidades, Plan, Arquitectura, Gestión, Monitoreo y Producto Funcional**
- 3.1. Introducción del Capítulo
- 3.2. Metodología de Gestión del Proyecto
- 3.3. Metodología de Desarrollo
- 3.4. Propuesta de Alternativas de Solución y sus Factibilidades
- 3.5. Plan de Proyecto
- 3.6. Arquitectura y Diseño de Alto Nivel (Modelo 4+1 de Kruchten)
- 3.7. Herramientas de Apoyo a la Gestión
- 3.8. Plan de Monitoreo y Control
- 3.9. Evidencia del Producto Funcional
- 3.10. Síntesis del Capítulo
- Referencias del Capítulo III

**Referencias (general, APA 7)** — sección única consolidada al cierre del portafolio

---

## Índice de Tablas e Índice de Figuras

> *Nota.* Numeración correlativa por capítulo (capítulo.número). En la versión `.docx`
> los índices se autogeneran desde las leyendas de tablas y figuras.

**Índice de Tablas**
- Tabla A. Declaración de uso de herramientas de inteligencia artificial (preliminares).
- Tabla 1. Diagrama de Ishikawa: categorías y causas de la problemática (Cap. I).
- Tabla 2. Costo operativo comparado (Cap. I).
- Tabla 3. Homologación del estado del arte frente a las restricciones del proyecto (Cap. I).
- Tabla 2.1. Justificación de objetivos: métricas, criterios de éxito y resultados medidos (Cap. II).
- Tabla 2.2. Homologación de la solución propuesta frente al estado del arte (Cap. II).
- Tabla 2.3. Resumen de la auditoría de licencias (511 paquetes) (Cap. II).
- Tabla 2.4. Selección ponderada de la metodología de gestión (Cap. II).
- Tabla 2.5. Matriz de riesgos del proyecto (Cap. II).
- Tabla 3.1. Selección ponderada de la metodología de gestión (Cap. III).
- Tabla 3.2. Alternativas de arquitectura general (Cap. III).
- Tabla 3.3. Consumo de VRAM medido por variante de modelo y cuantización (Cap. III).
- Tabla 3.4. Costo operativo comparado (Cap. III).
- Tabla 3.5. Requisitos funcionales y no funcionales con su criterio de verificación (Cap. III).
- Tabla 3.6. Reparto de recursos físicos del nodo único (Cap. III).
- Tabla 3.7. Herramientas de apoyo a la gestión y su función (Cap. III).
- Tabla 3.8. Matriz de riesgos del proyecto (extracto de los riesgos críticos y altos) (Cap. III).
- Tabla 3.9. Evidencia de liberación del sistema por iteración, contra su gate (Cap. III).

**Índice de Figuras**
- Figura 3.1. Diagrama de contexto del sistema Baxy (Cap. III).
- Figura 3.2. Vista Lógica: paquetes del agente y flujo del hot-path (Cap. III).
- Figura 3.3. Vista de Despliegue: procesos y entornos (Cap. III).
- Figura 3.4. Vista de Escenarios: los tres casos de uso clave (Cap. III).

> *Diagramas reproducibles.* Las versiones en Mermaid (Ishikawa, diagrama de contexto
> y las cuatro vistas 4+1) se entregan en el artefacto `diagramas_mermaid.md` para
> renderizarlas en alta resolución y reemplazar el arte ASCII en la entrega final.

---
---

# CAPÍTULO I — PROBLEMA / OPORTUNIDAD Y PLANTEAMIENTO

> *Nota de procedencia de datos.* Todas las cifras técnicas de este capítulo provienen de
> mediciones reales registradas en el repositorio del proyecto
> (`documentacion/datos_crudos/vram_real_medida.csv`,
> `documentacion/00_producto/{ANALISIS_COMPETENCIA.md, Gemma4_estado_y_limites_2026_05_29.md,
> AUDITORIA_licencias.md, PRODUCTION_READY.md}`) y no de estimaciones referenciales. Las
> fuentes externas se citan en formato APA 7 y se listan al cierre del capítulo.

---

## 1.1. Descripción del Problema / Oportunidad

Durante la última década, los asistentes de voz se consolidaron como una de las interfaces
de interacción humano-computador de mayor crecimiento. La conversación en lenguaje natural
—dictar un mensaje, consultar el clima, abrir una aplicación o controlar el equipo mediante
la palabra— dejó de ser una promesa de la ciencia ficción para convertirse en una práctica
cotidiana de cientos de millones de personas. El mercado global de asistentes de voz se
valoró en aproximadamente USD 9.163 millones en 2025 y se proyecta que alcance USD 59.900
millones hacia 2033, con una tasa de crecimiento anual compuesta (CAGR) cercana al 26,8 %,
sostenida por más de 8.400 millones de dispositivos habilitados para voz operando a nivel
mundial (Astute Analytica, 2026). El uso es, además, frecuente: en Estados Unidos la
penetración mensual del asistente de voz entre propietarios de teléfonos inteligentes
alcanzó el 88,1 % y el uso semanal subió del 45 % en 2024 al 60 % (Astute Analytica, 2026).
Estas cifras evidencian que el control por voz se ha vuelto un canal de acceso masivo a la
tecnología.

Sin embargo, este crecimiento descansa sobre una arquitectura tecnológica con limitaciones
estructurales que afectan directamente al usuario final. Los asistentes dominantes del
mercado —tales como Amazon Alexa, Apple Siri y Google Assistant— operan mayoritariamente
bajo un **paradigma dependiente de la nube**: el audio capturado por el micrófono se
transmite a servidores remotos propiedad de las empresas proveedoras, donde se realiza el
reconocimiento de voz, la comprensión del lenguaje y la generación de la respuesta. Este
modelo, si bien permite desplegar modelos de gran capacidad, traslada los datos de voz del
usuario —información biométrica y conductual sensible— fuera de su control, exige
conectividad permanente a internet, introduce latencia de red y, en sus versiones más
avanzadas, conlleva costos de suscripción o el uso de interfaces de programación de
aplicaciones (API) de pago.

El problema, en su formulación precisa, se enuncia como sigue: **no existe en el mercado un
asistente de voz que sea, simultáneamente, local (que procese todo en el equipo del
usuario), privado (que no envíe audio ni datos a la nube), gratuito y de código abierto,
multilingüe, y que opere en hardware modesto —específicamente en una tarjeta gráfica de
apenas 4 GB de memoria de video (VRAM)—.** El análisis comparativo de diez soluciones
contemporáneas del estado del arte (véase la Sección 1.7) confirma que ninguna cumple este
conjunto de restricciones de forma conjunta: todas son soluciones en la nube, dependen de
modelos de gran tamaño o constituyen marcos de orquestación para desarrolladores, no
productos de voz para el usuario final en hardware limitado (documentación interna del
proyecto, `ANALISIS_COMPETENCIA.md`, 2026).

Este vacío admite una doble lectura. Por un lado constituye un **problema** —el usuario que
valora su privacidad, carece de hardware potente o no desea pagar suscripciones queda hoy
sin una opción que satisfaga sus necesidades—; por el otro, configura una **oportunidad**
clara, sustentada en tres tendencias convergentes y medibles:

1. **Demanda insatisfecha de privacidad.** Una encuesta aplicada a más de mil personas en
   Estados Unidos reveló que el 77 % de los usuarios se mostraría más propenso a utilizar
   asistentes de voz si estos ofrecieran mayores garantías de privacidad y transparencia
   (Secure Data Recovery Services, 2024). Existe, por tanto, una demanda explícita por
   soluciones que prioricen la privacidad y que el modelo dominante no resuelve.
2. **Madurez de la inteligencia artificial en el borde (*edge AI*).** La literatura técnica
   reciente documenta que el despliegue de modelos de lenguaje directamente en el
   dispositivo elimina la necesidad de transmitir datos en bruto del usuario hacia
   servidores externos, mitigando los riesgos de privacidad, costo, latencia y fiabilidad
   de red propios del enfoque en la nube (Zheng et al., 2024; Cai et al., 2026).
3. **Disponibilidad de modelos abiertos y eficientes.** La aparición de modelos de lenguaje
   abiertos y cuantizables (como la familia Gemma 4) y de motores de inferencia maduros para
   hardware de consumo (como `llama.cpp`) hace técnicamente factible, por primera vez,
   ejecutar un asistente capaz en una GPU de gama de entrada.

El presente proyecto, denominado **Baxy**, aborda este vacío mediante el
diseño y desarrollo de un asistente de voz para el sistema operativo Windows que opera
íntegramente de manera local y privada, sin transmitir información a servidores externos,
empleando un modelo de lenguaje Gemma 4 E2B ajustado mediante *fine-tuning* y servido con la
biblioteca de código abierto `llama.cpp`. El asistente controla el sistema operativo a través
de más de **sesenta herramientas** de dominio (67 esquemas únicos expuestos al modelo tras retirar
`smart_home`): control por voz, visión por cámara, automatización de
la interfaz —*computer-use*— mediante accesibilidad UIA y reconocimiento óptico de
caracteres, gestión de aplicaciones y navegación web; e incorpora modos de accesibilidad
orientados a personas con movilidad reducida y no videntes.

La importancia de esta iniciativa radica en que aborda, de manera simultánea, tres
dimensiones críticas habitualmente tratadas por separado: la **privacidad** (los datos nunca
abandonan el equipo), la **accesibilidad económica** (operación sin costo recurrente, sobre
hardware que el usuario ya posee) y la **inclusión tecnológica** (funcionamiento en máquinas
modestas y soporte multilingüe).

---

## 1.2. Situación Actual

El proceso actual mediante el cual un usuario interactúa con un asistente de voz comercial
puede describirse, de forma simplificada, en la siguiente secuencia de siete pasos: (1) el
usuario emite un comando de voz; (2) el micrófono del dispositivo captura el audio; (3) el
dispositivo transmite ese audio a través de internet hacia los servidores del proveedor; (4)
en la nube se realiza el reconocimiento del habla y la interpretación de la intención; (5)
los servidores generan la respuesta; (6) la respuesta se devuelve por la red al dispositivo;
y (7) el dispositivo reproduce el resultado. Este flujo, aparentemente fluido para el
usuario, presenta una serie de problemas estructurales que **destruyen valor**:

- **Dependencia de la nube y de la conectividad.** El asistente no funciona sin conexión a
  internet. Una caída de red, una zona sin cobertura o una interrupción del servicio del
  proveedor inhabilitan por completo la herramienta. El usuario no posee control real sobre
  la disponibilidad de un servicio del que depende para tareas cotidianas.
- **Pérdida de privacidad.** El audio del usuario —que constituye información biométrica y
  puede contener datos sensibles— se transmite y, en muchos casos, se almacena en servidores
  de terceros. La evidencia indica que cerca del 60 % de los usuarios está, al menos
  ocasionalmente, preocupado por su privacidad y que el 49 % desconoce que estos dispositivos
  escuchan de forma continua a la espera de la palabra de activación (Secure Data Recovery
  Services, 2024).
- **Latencia de red.** Cada turno de interacción requiere, como mínimo, un viaje de ida y
  vuelta de los datos a través de internet. Esta latencia se suma al tiempo de procesamiento
  y depende de condiciones de red ajenas al control del usuario.
- **Costo.** Las versiones más avanzadas de estos servicios, o el acceso programático a sus
  capacidades mediante API, conllevan costos de suscripción o por uso. El usuario que desea
  funcionalidades plenas debe asumir un gasto recurrente.
- **Datos de voz en servidores ajenos (asimetría de control).** El problema de fondo es que
  la voz del usuario, una vez transmitida, deja de ser suya en términos prácticos. La
  transparencia sobre qué se almacena, por cuánto tiempo y con qué fines de perfilamiento es
  limitada, y la posibilidad efectiva de borrar o controlar esos datos resulta restringida.

Frente a este panorama, las alternativas locales existentes en el estado del arte tampoco
resuelven el problema para el usuario objetivo. El análisis de competencia interno examinó
diez soluciones representativas y concluyó que **ninguna corre de manera local en 4 GB de
VRAM con capacidad de voz**: las orientadas a *computer-use* (como Agent-S u OS-Copilot)
dependen de modelos en la nube (GPT-4V, Gemini); los marcos de orquestación (AutoGen,
LangGraph, AutoGPT) requieren modelos grandes; y la única solución de asistencia personal por
voz comparable (Mark-XXXIX) depende de la API de Gemini en la nube, que es de pago
(`ANALISIS_COMPETENCIA.md`, 2026). En consecuencia, el usuario que valora la privacidad,
carece de hardware potente o no desea pagar suscripciones queda hoy sin una opción que
satisfaga sus necesidades.

---

## 1.3. Identificación de la Problemática (Diagrama de Ishikawa)

Para identificar y organizar de manera sistemática las causas que originan la problemática,
se aplicó la técnica del **diagrama de Ishikawa** (también denominado diagrama de
causa-efecto o de espina de pescado). Esta técnica permite descomponer un problema central
en categorías de causas, facilitando el análisis de su origen antes de proponer una
solución. El problema central (la "cabeza del pescado") se define como:

> **"No existe un asistente de voz local, privado y gratuito que sea usable en hardware
> modesto (≤ 4 GB de VRAM)."**

Las causas se organizaron en seis categorías, mediante una adaptación de las clásicas "6M"
del método de Ishikawa (Método, Máquina, Mano de obra, Medio ambiente, Materiales y
Medición) al dominio tecnológico del proyecto. La siguiente representación textual sintetiza
la espina de pescado, y la Tabla 1 consolida las categorías y sus causas.

```
                                                          ┌─ TECNOLOGÍA / MÁQUINA
                                                          │   • LLMs grandes no caben en 4 GB de VRAM
                                                          │   • El encoder de visión (mmproj) pesa ~1,2 GB
                                                          │   • Hardware de usuario modesto (GPU 4 GB o sin GPU)
                                                          │
                          ┌─ COSTO ────────────────────┐ │
                          │   • APIs cloud de pago      │ │
                          │   • GPUs de alta gama caras │ │
                          │   • Suscripciones recurrentes│ │
                          │                              ▼ ▼
   PROBLEMA: "No existe un asistente de voz local, privado y gratuito
   ◄──────────────────────  usable en hardware modesto (≤ 4 GB VRAM)"
                              ▲ ▲
                          │   │ │   ┌─ MÉTODO
   ┌─ PRIVACIDAD ─────────┘   │ └───┤   • Tool-calling difícil en modelos chicos (~75 % vs ~91 %, estimado)
   │   • Audio enviado a la nube│    │   • Modelos pueden "alucinar" acciones
   │   • Datos en servidores    │    │   • Sin medición/gates, se celebra sin verificar
   │     ajenos                  │    │
   │                            │    └─ IDIOMA / MANO DE OBRA-USUARIO
   └─ CONECTIVIDAD ─────────────┘        • Competidores monolingües o anglocéntricos
       • Requiere internet permanente    • Hardcodeo de idioma (p. ej. a chino)
       • Latencia de red por turno       • Excluye a usuarios de otros idiomas/acentos
```

**Tabla 1.** *Diagrama de Ishikawa: categorías y causas de la problemática.*

| Categoría (6M) | Causas identificadas |
|---|---|
| **Tecnología / Máquina** | LLMs de gran tamaño no entran en 4 GB de VRAM; el encoder de visión (mmproj) consume ~1,2 GB; el hardware del usuario objetivo es modesto (GPU de 4 GB o sin GPU dedicada). |
| **Costo** | Las API en la nube son de pago; las GPU de alta gama son costosas; las versiones avanzadas de los asistentes exigen suscripción recurrente. |
| **Privacidad** | El audio del usuario se envía a la nube; los datos de voz se almacenan en servidores ajenos; el usuario pierde el control sobre su información. |
| **Conectividad** | El servicio requiere conexión permanente a internet; la latencia de red se suma a cada turno de interacción; no hay operación offline. |
| **Idioma / Usuario** | Los competidores son monolingües o anglocéntricos; existe *hardcodeo* de idioma; se excluye a hablantes de otros idiomas y acentos. |
| **Método** | El *tool-calling* (selección de la acción correcta) es difícil en modelos pequeños (estimado ~75 % de acierto frente a ~91 % en modelos grandes); riesgo de que el modelo "alucine" acciones; la ausencia de medición contra criterios de éxito conduce a declarar logros no verificados. |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md`,
`Gemma4_estado_y_limites_2026_05_29.md` y `vram_real_medida.csv`.*

---

## 1.4. Descripción de Causas (en qué consiste / qué efecto / cómo incide)

A continuación se describe cada causa identificada en el diagrama de Ishikawa,
especificando **en qué consiste**, **qué efecto produce** y **cómo incide** en la
problemática central. Se distingue, además, entre causas **principales** (las de mayor peso
en el origen del problema) y **secundarias**, y entre **factores internos** (propios de la
tecnología) y **externos** (propios del entorno de mercado y del usuario).

### 1.4.1. Categoría Tecnología / Máquina (causa principal)

- **Los modelos de lenguaje grandes no caben en 4 GB de VRAM.**
  *En qué consiste:* los modelos de lenguaje de mayor capacidad requieren varios gigabytes
  de memoria de video incluso en sus cuantizaciones más agresivas.
  *Qué efecto produce:* obliga a renunciar al modelo más capaz y a buscar uno que entre en
  el presupuesto de memoria.
  *Cómo incide:* es la restricción de hardware fundamental que define la viabilidad del
  proyecto. La medición real lo confirma: el modelo Gemma 4 E2B en cuantización Q4_K_M ocupa
  3.371 MiB de VRAM (delta), por lo que entra en una tarjeta de 4 GB; en cambio, el modelo
  E4B en la misma cuantización ocupa 5.087 MiB y **no entra**, y las variantes de 26B/31B
  superan los 12.000 MiB (`vram_real_medida.csv`, 2026). Esta es la causa raíz que motivó el
  pivote de E4B a E2B en el diseño.

- **El encoder de visión pesa ~1,2 GB.**
  *En qué consiste:* habilitar la capacidad de visión (procesar imágenes de la pantalla)
  requiere mantener residente un componente adicional (mmproj) que consume aproximadamente
  1,2 GB.
  *Qué efecto produce:* reduce el margen disponible para el resto del modelo y del contexto.
  *Cómo incide:* en una GPU de exactamente 4 GB, el margen tras cargar la visión es estrecho,
  lo que obliga a gestionar la visión como un recurso residente y gateado, y a desactivarla
  en el modo de respaldo por CPU (`Gemma4_estado_y_limites_2026_05_29.md`, 2026).

- **Hardware de usuario modesto.**
  *En qué consiste:* el usuario objetivo posee una laptop o PC típica, con GPU de 4 GB o sin
  GPU dedicada.
  *Qué efecto produce:* impide asumir hardware de gama alta como base de diseño.
  *Cómo incide:* es un factor externo que fija el techo de recursos y, junto con la causa
  anterior, define el carácter "modesto" del *target*.

### 1.4.2. Categoría Costo (causa principal, factor externo)

- **API en la nube de pago y suscripciones recurrentes.**
  *En qué consiste:* las capacidades avanzadas de los asistentes comerciales y el acceso
  programático a modelos de gran escala se ofrecen bajo modelos de pago.
  *Qué efecto produce:* impone una barrera económica de entrada y un gasto continuo.
  *Cómo incide:* excluye a usuarios y desarrolladores que no pueden o no desean pagar, y es
  incompatible con la restricción de producto de ser íntegramente gratuito y de código
  abierto.

- **GPU de alta gama costosas.**
  *En qué consiste:* ejecutar localmente modelos grandes exigiría tarjetas gráficas de mucha
  memoria, de precio elevado.
  *Qué efecto produce:* traslada el costo del servicio en la nube a un costo de hardware.
  *Cómo incide:* refuerza, desde el lado económico, la necesidad de operar en hardware
  modesto.

### 1.4.3. Categoría Privacidad (causa principal)

- **Audio enviado a la nube y datos en servidores ajenos.**
  *En qué consiste:* el modelo dominante transmite el audio del usuario a servidores de
  terceros para su procesamiento.
  *Qué efecto produce:* el usuario pierde el control sobre información biométrica y sensible,
  y queda expuesto a riesgos de almacenamiento, perfilamiento y filtración.
  *Cómo incide:* es la causa que da identidad al proyecto; la evidencia de mercado muestra
  que cerca del 60 % de los usuarios está preocupado por su privacidad y que el 77 %
  preferiría una alternativa con mayores garantías (Secure Data Recovery Services, 2024), lo
  que confirma que esta causa representa también una **oportunidad de demanda insatisfecha**.

### 1.4.4. Categoría Conectividad (causa secundaria, factor externo)

- **Requiere internet permanente y añade latencia de red.**
  *En qué consiste:* el procesamiento en la nube exige conexión continua y un viaje de datos
  por turno.
  *Qué efecto produce:* inhabilita el uso offline y degrada la experiencia bajo redes lentas.
  *Cómo incide:* limita la fiabilidad y la disponibilidad del asistente; su resolución es un
  beneficio directo del procesamiento local.

### 1.4.5. Categoría Idioma / Usuario (causa secundaria)

- **Competidores monolingües o anglocéntricos y *hardcodeo* de idioma.**
  *En qué consiste:* varias soluciones del estado del arte están optimizadas para un único
  idioma o codifican rígidamente reglas dependientes del idioma (por ejemplo, OS-Copilot
  presenta *hardcodeo* en chino).
  *Qué efecto produce:* excluye a hablantes de otros idiomas y a usuarios con acentos
  diversos.
  *Cómo incide:* contradice el requisito de uso universal y multilingüe, y evidencia un vacío
  que el proyecto cubre mediante clasificación por *embeddings* multilingües en lugar de
  listas de palabras clave por idioma (`ANALISIS_COMPETENCIA.md`, 2026).

### 1.4.6. Categoría Método (causa principal, factor interno)

- **El *tool-calling* es difícil en modelos pequeños.**
  *En qué consiste:* la selección correcta de la acción a ejecutar es más exigente para un
  modelo de 2.000 millones de parámetros que para uno grande.
  *Qué efecto produce:* una tasa de acierto menor (estimada ~75 % en E2B frente a ~91 % en E4B), con
  riesgo de elegir una acción equivocada.
  *Cómo incide:* es el principal compromiso técnico aceptado al elegir el modelo que entra en
  4 GB, y motiva mecanismos correctivos como el reintento forzado de herramienta
  (`Gemma4_estado_y_limites_2026_05_29.md`, 2026).

- **Riesgo de "alucinar" acciones.**
  *En qué consiste:* un modelo puede inventar una acción o afirmar haberla ejecutado sin que
  sea cierto.
  *Qué efecto produce:* erosiona la confianza del usuario y puede provocar comportamientos
  incorrectos.
  *Cómo incide:* obliga a incorporar honestidad estructural —verificación del resultado por
  el estado real del sistema operativo, no por el juicio del propio modelo— como requisito de
  diseño.

- **Ausencia de medición frente a criterios de éxito.**
  *En qué consiste:* declarar logros sin un número contra un criterio definido de antemano.
  *Qué efecto produce:* lleva a dar por resueltos problemas que persisten.
  *Cómo incide:* es una causa metodológica que el proyecto neutraliza mediante un enfoque de
  desarrollo dirigido por mediciones y *gates*.

### 1.4.7. Síntesis causal

En síntesis, las **causas principales** de la problemática son las restricciones de hardware
(Tecnología/Máquina), el costo, la privacidad y la dificultad de *tool-calling* en modelos
pequeños (Método); mientras que la dependencia de la conectividad y las limitaciones de
idioma operan como **causas secundarias** que agravan el problema y estrechan aún más la
oferta disponible. La convergencia de todas ellas explica por qué, al día de hoy, no existe
una solución que satisfaga de forma conjunta los requisitos de ser local, privada, gratuita,
multilingüe y operable en hardware modesto —el vacío que el presente proyecto se propone
llenar.

---

## 1.5. Fundamentación del Problema

La industria de los asistentes de voz constituye uno de los segmentos de mayor dinamismo
dentro del ecosistema de la inteligencia artificial aplicada al consumidor. Como se señaló,
el mercado se valoró en aproximadamente USD 9.163 millones en 2025 y se proyecta una
expansión hasta los USD 59.900 millones en 2033 (CAGR ≈ 26,8 %), impulsada por una base
instalada que ya supera los 8.400 millones de dispositivos habilitados para voz (Astute
Analytica, 2026). El uso es masivo: en Estados Unidos la penetración mensual del asistente de
voz entre propietarios de teléfonos inteligentes alcanzó el 88,1 %, y el uso semanal por voz
creció del 45 % en 2024 al 60 % (Astute Analytica, 2026). Este segmento está dominado por un
reducido grupo de actores —Amazon (Alexa), Apple (Siri) y Google (Google Assistant)— cuyo
modelo operativo descansa, de forma predominante, en el procesamiento en la nube.

De manera paralela, se observa una tendencia tecnológica creciente hacia la **inteligencia
artificial en el borde (*edge AI*)**, es decir, hacia la ejecución de modelos directamente en
el dispositivo del usuario en lugar de en servidores remotos. La literatura técnica reciente
identifica que el despliegue de modelos de lenguaje de gran escala (LLM) en el dispositivo
elimina la necesidad de transmitir datos en bruto del usuario hacia servidores externos,
manteniendo toda la información personal de forma localizada, lo que mitiga los riesgos de
privacidad, costo, latencia y fiabilidad de red propios del enfoque en la nube; no obstante,
este despliegue se ve severamente restringido por las limitaciones de recursos del hardware
de borde (Zheng et al., 2024; Cai et al., 2026). Fabricantes como Google (con Gemini Nano
integrado en Android) ya han comenzado a ejecutar modelos
localmente en sus dispositivos, lo que confirma la direccionalidad de la industria hacia el
procesamiento en el borde (Android Developers, s. f.).

En tercer lugar, existe una preocupación documentada y creciente respecto de la
**privacidad** en el uso de asistentes de voz. La encuesta de Secure Data Recovery Services
(2024), aplicada a más de mil personas en Estados Unidos, reveló que cerca del 60 % de los
usuarios manifestaba estar, al menos ocasionalmente, preocupado por su privacidad al utilizar
asistentes de voz; que el 49 % desconocía que estos dispositivos escuchan de forma continua a
la espera de la palabra de activación; que el 68 % nunca había adoptado medida alguna para
mejorar su privacidad; y, de manera reveladora, que el 77 % se mostraría más propenso a usar
asistentes de voz si estos ofrecieran mayores garantías de privacidad y transparencia. Esta
última cifra es particularmente significativa para el presente proyecto, pues señala una
demanda insatisfecha por soluciones que prioricen la privacidad.

En síntesis, el contexto configura una tensión clara: una industria masiva y en expansión,
una tendencia tecnológica que habilita el procesamiento local, y una preocupación ciudadana
por la privacidad que el modelo dominante no resuelve. El cruce de estos tres vectores
delimita la oportunidad que este proyecto persigue, y fundamenta que el problema no es un
caso aislado, sino un vacío sistémico de la oferta actual frente a una demanda real y
documentada.

---

## 1.6. Viabilidad del Proyecto (cinco dimensiones)

La viabilidad del proyecto se analiza en cinco dimensiones —legal, medioambiental, económica,
tecnológica y cultural—. En todos los casos, el análisis se sustenta en datos medidos del
repositorio del proyecto y no en proyecciones, conforme al principio metodológico rector
("medir, no celebrar").

### 1.6.1. Viabilidad Legal

El carácter íntegramente local del sistema lo posiciona de manera favorable frente al marco
normativo chileno sobre datos y privacidad. Dado que **ningún dato de voz, pantalla o
comportamiento abandona el equipo del usuario**, el sistema minimiza estructuralmente el
riesgo regulatorio asociado a:

- la **Ley N.º 21.719** sobre protección de datos personales;
- la **Ley N.º 19.628** sobre protección de la vida privada;
- la **Ley N.º 21.459** sobre delitos informáticos; y
- la **Ley N.º 21.663** marco de ciberseguridad.

A diferencia del modelo en la nube —en el que la transmisión y el almacenamiento de
información biométrica (la voz) en servidores de terceros activan obligaciones de tratamiento,
consentimiento y resguardo—, el procesamiento local desplaza el dato fuera del alcance de
estas obligaciones por diseño. En el plano de la propiedad intelectual, el proyecto se ajusta
a la **Ley N.º 17.336** sobre Propiedad Intelectual: la declaración de uso de herramientas de
inteligencia artificial en el desarrollo es transparente, y el *stack* de software empleado
es de licencias de código abierto. La auditoría de licencias del repositorio
(`AUDITORIA_licencias.md`) sobre 511 paquetes concluyó que el núcleo es permisivo —Gemma 4
(Apache-2.0), MediaPipe, OpenCV, Whisper (MIT), `llama.cpp`, Tesseract y
*sentence-transformers*—, con un **único bloqueante** para una eventual comercialización
propietaria: `piper-tts` 1.4.2, bajo GPL-3.0-or-later, cuyo aislamiento por subproceso o
sustitución por un TTS Apache/MIT resuelve el obstáculo a bajo costo. La viabilidad legal es,
por tanto, alta.

### 1.6.2. Viabilidad Medioambiental

El modelo de **cómputo local sobre hardware ya existente** presenta una huella ambiental
favorable frente al paradigma de la nube. El procesamiento en centros de datos remotos implica
consumo energético de servidores, refrigeración e infraestructura de red asociada a cada
turno de interacción; en cambio, la solución propuesta ejecuta la totalidad de su pipeline en
el equipo que el usuario ya posee, sin requerir aprovisionamiento adicional de hardware ni la
operación continua de un *datacenter*. Adicionalmente, el diseño está explícitamente
optimizado para hardware modesto (consumo medido de 3.371 MiB de VRAM, equivalente a una GPU
de gama de entrada), lo que evita inducir la compra de equipos de alta gama de mayor consumo.
La viabilidad medioambiental es favorable, en tanto el proyecto sustituye cómputo remoto
continuo por cómputo local puntual sobre recursos preexistentes.

### 1.6.3. Viabilidad Económica

El proyecto presenta un **costo de operación de USD 0**, lo que constituye su principal
ventaja económica frente a la competencia. El usuario reutiliza el hardware que ya posee y no
incurre en suscripciones, costos de API por uso ni infraestructura de servidor. La Tabla 2
contrasta el costo operativo de la solución frente al de un asistente en la nube.

**Tabla 2.** *Costo operativo comparado.*

| Concepto | Asistente cloud (Alexa+/APIs) | Baxy |
|---|---|---|
| APIs de IA por uso | Costo recurrente por token/consulta | USD 0 (modelo local) |
| Suscripción mensual | Sí (tiers de pago) | USD 0 |
| Infraestructura de servidor | A cargo del proveedor (incluida en el precio) | Ninguna (corre en el equipo del usuario) |
| Hardware | GPU cara o nube | GPU 4 GB que el usuario ya posee / CPU |

*Fuente: Elaboración propia (2026), a partir de `AUDITORIA_licencias.md` y
`PRODUCTION_READY.md`.*

Desde la perspectiva de costos de desarrollo, el proyecto se construyó íntegramente sobre
software libre y gratuito, por lo que la inversión principal corresponde a tiempo de
desarrollo y no a licencias. La viabilidad económica es, por tanto, alta: costo de operación
nulo, hardware preexistente y un único obstáculo legal de resolución conocida y barata.

### 1.6.4. Viabilidad Tecnológica

La factibilidad técnica está **probada, no proyectada**: el sistema corre hoy en 4 GB de
VRAM. El barrido de VRAM sobre los modelos **base** midió que E2B-Q4_K_M base consume
3.371 MiB (≈3,36 GB) —dato que decidió el pivote E4B → E2B—, mientras que el modelo
*fine-tuneado* finalmente desplegado consume ≈2,07 GB de VRAM (medición del despliegue del
FT v2, `dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`), con aún más margen para la visión
(≈1,2 GB) y el contexto (12.288 tokens de piso). La inferencia se sirve
con `llama.cpp`/`llama-server`, una solución madura y ampliamente adoptada en la comunidad
para correr LLM cuantizados en hardware de consumo. Los componentes del *stack* también están
verificados: el LLM (Gemma 4 E2B *fine-tuneado*, GGUF Q4_K_M) con *fallback* a CPU; la
transcripción (Parakeet/Whisper int8) y la síntesis de voz (Piper) corren en CPU para no
competir por la VRAM; y se resolvieron los riesgos de estabilidad conocidos (el *crash* del
render WebView2 al competir por la GPU y el *crash* CUDA #22527, mitigado al desactivar
*flash-attention* por defecto, estable en 37/37 mediciones). El hardware objetivo es una GPU
NVIDIA de 4 GB (GTX 1650 / RTX 3050 4 GB) con un piso de 8 GB de RAM, y existe una ruta clara
(*build* CPU/Vulkan) para extender el alcance a laptops sin gráfica dedicada. La viabilidad
tecnológica está, por tanto, demostrada empíricamente.

### 1.6.5. Viabilidad Cultural

La dimensión cultural y social del proyecto se articula en tres ejes que constituyen su
propuesta de valor diferencial. **Privacidad:** al no transmitir datos fuera del equipo, el
sistema responde a una preocupación ciudadana documentada (el 77 % de los usuarios preferiría
un asistente con mayores garantías de privacidad; Secure Data Recovery Services, 2024).
**Accesibilidad e inclusión:** el asistente incorpora tres modos de accesibilidad
—respaldados por las pautas WCAG 2.1/2.2— que atienden necesidades distintas: el modo
`no_vidente` narra cada acción y lee la pantalla a pedido (todo por audio), y el modo
`movilidad` ofrece control 100 % por voz para personas que no pueden usar teclado o mouse; el
control por cámara y gestos (MediaPipe) complementa el acceso para movilidad reducida.
**Democratización y universalidad:** al correr en hardware modesto, el sistema democratiza el
acceso a un asistente inteligente sin exigir GPU cara ni suscripción, y su carácter
multilingüe y multi-acento —resuelto por *embeddings* y no por listas de palabras codificadas
por idioma— lo hace utilizable por hablantes de cualquier lengua, a diferencia de
competidores monolingües o inglés-céntricos. La viabilidad cultural es alta, en tanto el
proyecto se alinea con valores de privacidad, inclusión y acceso universal.

---

## 1.7. Estado del Arte

El estado del arte se analizó comparando **diez soluciones contemporáneas** representativas
de las tres familias de productos vecinos al proyecto: (a) los asistentes de voz comerciales
dominantes, (b) los asistentes y agentes locales de código abierto, y (c) los agentes de
*computer-use* y los marcos de orquestación de agentes. El análisis combinó la lectura del
código de cada competidor con la del propio sistema, filtrado por las restricciones duras del
proyecto (4 GB de VRAM, código abierto, operación local, voz y multilingüismo)
(`ANALISIS_COMPETENCIA.md`, 2026).

### 1.7.1. Asistentes de voz comerciales (Alexa, Siri, Google Assistant)

Los asistentes comerciales dominantes —Amazon Alexa, Apple Siri y Google Assistant—
constituyen el referente de uso masivo y de calidad conversacional. Su capacidad descansa en
modelos de gran escala ejecutados en la nube, lo que les confiere un *tool-calling* y una
comprensión del lenguaje de alta calidad. No obstante, este mismo diseño es la fuente de las
limitaciones estructurales descritas en la Sección 1.2: transmiten el audio del usuario a
servidores remotos (pérdida de privacidad), requieren conectividad permanente (no operan
offline), introducen latencia de red y, en sus versiones avanzadas o de acceso programático,
conllevan costos. Como tendencia reciente, fabricantes como Google (Gemini Nano)
han comenzado a ejecutar parte del procesamiento localmente en sus
dispositivos móviles (Android Developers, s. f.), lo que confirma la direccionalidad hacia el
borde,
pero no resuelve el caso del usuario de PC de escritorio en hardware modesto y bajo control
total de sus datos.

### 1.7.2. Asistentes y agentes locales de código abierto

Existen soluciones que sí permiten ejecución local, pero ninguna satisface el conjunto
completo de restricciones del proyecto. **Open Interpreter** ejecuta código en lenguaje
natural y soporta modelos locales, pero está orientado a desarrolladores y no constituye un
asistente de voz para el usuario final. **Goose** es un agente local escrito en Rust con
soporte de más de 50 proveedores de modelos y enlace nativo a `llama-cpp`, pero es igualmente
un agente de software, no un producto de voz, y reescribir en Rust no se ajusta al *stack* del
proyecto. La solución más cercana en concepto, **Mark-XXXIX**, es un asistente personal de voz
del tipo "JARVIS"; sin embargo, **depende de la API de Gemini 2.5 LiveAPI en la nube, que es
de pago**, lo que lo descalifica frente a las restricciones de privacidad, costo y operación
local.

### 1.7.3. Agentes de *computer-use* y marcos de orquestación

En el ámbito de la automatización del sistema operativo (*computer-use*), **Agent-S** alcanza
un alto desempeño en el benchmark OSWorld (≈72 %) pero opera sobre modelos en la nube (GPT-4V
/ UI-TARS); **OS-Copilot** automatiza el sistema operativo con auto-aprendizaje, pero depende
de modelos en la nube y presenta *hardcodeo* de idioma (chino), lo que contradice el requisito
multilingüe; y **OpenHands** es un agente de software empresarial dependiente de la nube y de
Docker. Por su parte, los marcos de orquestación de agentes —**AutoGen** (multi-agente
conversacional), **LangGraph** (máquina de estados/DAG de agentes) y **AutoGPT** (bucle
autónomo con memoria episódica)— son *frameworks* para desarrolladores que requieren modelos
grandes y no entran en el presupuesto de 4 GB de VRAM; **openclaw**, finalmente, es un
*gateway* multicanal de infraestructura, no un asistente de voz de escritorio. Los techos de
referencia del propio dominio confirman además que la operación autónoma de la GUI es un
problema abierto: los benchmarks sitúan el estado del arte en ≈52,5 % (WindowsAgentArena) y
≈28 % (UFO2 + GPT-4o), de modo que ninguna solución —ni siquiera las basadas en modelos en la
nube— ofrece un desempeño físico perfecto.

### 1.7.4. Tabla comparativa (homologación) y vacío identificado

La Tabla 3 homologa las diez soluciones analizadas frente a las restricciones duras del
proyecto. La conclusión del análisis es contundente: **todas las soluciones examinadas son en
la nube, dependen de modelos grandes, o son marcos de orquestación para desarrolladores;
ninguna corre de manera local en 4 GB de VRAM con capacidad de voz** (`ANALISIS_COMPETENCIA.md`,
2026).

**Tabla 3.** *Homologación del estado del arte frente a las restricciones del proyecto.*

| Solución | Qué es | Modelo / recursos | ¿Local en 4 GB? | ¿Voz al usuario final? | ¿Gratis/OSS? |
|---|---|---|---|---|---|
| Amazon Alexa / Apple Siri / Google Assistant | Asistente de voz comercial masivo | Modelos grandes en la nube | No | Sí | No |
| Agent-S | *Computer-use* / GUI (OSWorld 72 %) | GPT-4V / UI-TARS, nube | No | No | No |
| OS-Copilot | Automatización del SO + auto-aprendizaje | GPT, nube, *hardcoded* chino | No | No | Parcial |
| Open Interpreter | Ejecuta código en lenguaje natural | Claude/LiteLLM (soporta local) | Parcial | No | Sí |
| OpenHands | Agente de software empresarial | Anthropic/OpenAI, nube/Docker | No | No | Sí |
| Goose | Agente local en Rust + MCP | 50+ proveedores, `llama-cpp` nativo | Parcial | No | Sí |
| Mark-XXXIX | Asistente de voz tipo JARVIS (el más parecido) | Gemini 2.5 LiveAPI, nube, **pago** | No | Sí | No |
| openclaw | *Gateway* multicanal (WhatsApp, etc.) | Agnóstico, infraestructura | No | No | Sí |
| AutoGen | Multi-agente conversacional (*framework*) | N modelos grandes | No | No | Sí |
| LangGraph | Máquina de estados / DAG de agentes (*framework*) | Modelos grandes | No | No | Sí |
| AutoGPT | Bucle autónomo + memoria episódica (*framework*) | Modelos grandes | No | No | Sí |
| **Baxy (propuesta)** | **Asistente de voz de escritorio local** | **Gemma 4 E2B-FT, GGUF Q4_K_M, `llama.cpp`** | **Sí (3,36 GB medido)** | **Sí** | **Sí** |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md`.*

En síntesis, el estado del arte confirma la existencia de un **nicho no cubierto**: la
intersección de operación local, presupuesto de 4 GB de VRAM, capacidad de voz para el
usuario final, privacidad, gratuidad/código abierto, multilingüismo y honestidad estructural
está vacía. El proyecto Baxy se posiciona precisamente en ese vacío. Su
diferenciador no es superar a los modelos en la nube en capacidad bruta, sino ser el mejor
asistente que se ejecuta de manera privada en la máquina del propio usuario, sobre hardware
que ya posee y sin costo recurrente.

---

## Referencias del Capítulo I (APA 7)

> *Nota de verificación.* Las cifras y fuentes de este capítulo fueron contrastadas contra la
> página original el 2026-06-03 (véase `_tesis_curso/entregables/bibliografia_verificada.md`).
> Se corrigieron los autores de las dos referencias académicas (cruce de citas en el borrador)
> y la atribución de la mención a Gemini Nano se reasignó a su fuente primaria (Android
> Developers), retirando la mención a Apple Intelligence por carecer de fuente verificada en
> esta revisión; la cifra de penetración de uso por voz se sustituyó por los valores
> efectivamente publicados por la fuente.

Android Developers. (s. f.). *Gemini Nano*. Google. Recuperado el 3 de junio de 2026, de
https://developer.android.com/ai/gemini-nano

Astute Analytica. (2026, 10 de febrero). *Voice assistant market to reach US$ 59.9 billion by
2033 driven by mass consumer adoption, enterprise voice AI, and smart device proliferation*.
GlobeNewswire.
https://www.globenewswire.com/news-release/2026/02/10/3235286/0/en/Voice-Assistant-Market-to-Reach-US-59-9-Billion-by-2033-Driven-by-Mass-Consumer-Adoption-Enterprise-Voice-AI-and-Smart-Device-Proliferation-Astute-Analytica.html

Cai, G., Tian, R., Yang, L., Jia, Y., Li, L., & Wang, J. (2026). Efficient inference for edge
large language models: A survey. *Tsinghua Science and Technology, 31*(3).
https://doi.org/10.26599/TST.2025.9010166

Secure Data Recovery Services. (2024, 5 de agosto). *Listening in: Privacy concerns of voice
assistants*. https://www.securedatarecovery.com/blog/smart-device-privacy-concerns

Zheng, Y., Chen, Y., Qian, B., Shi, X., Shu, Y., & Chen, J. (2024). *A review on edge large
language models: Design, execution, and applications* [Preprint]. arXiv.
https://arxiv.org/abs/2410.11845

---
---

# CAPÍTULO II — OBJETIVOS, MÉTRICAS, ALCANCE, NORMATIVAS Y PLAN DE PROYECTO

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
> subsistema de palabra de activación, **criterio que el sistema cumple**: la palabra de
> activación "Baxy" alcanzó un **recall de 0,872** sobre un *held-out* universal y diverso
> de 25 voces y 13 idiomas (por encima del umbral de 0,60), con recall perfecto (1,00) en
> español, inglés, italiano, francés, polaco y ruso. La voz extremo a extremo está
> implementada y operativa de forma *offline*. La distinción
> entre **validación sintética** y **prueba real** se mantiene explícita en todos los
> reportes, conforme al principio metodológico rector del proyecto: *medir, no
> celebrar*.

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
en una suscripción recurrente, sino en la adopción de un producto que el usuario ejecuta en
su propio hardware, con vías de monetización abiertas gracias a un *stack* de licencia
permisiva.

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
  opera sin GPU NVIDIA cayendo automáticamente al perfil CPU (`-ngl 0`).
- **Techo del computer-use.** La capacidad de operar la GUI está limitada por el estado
  del arte; los benchmarks de referencia sitúan el techo en ≈ 52,5 %
  (WindowsAgentArena) y ≈ 28 % (UFO2 + GPT-4o), de modo que el desempeño físico no
  puede prometerse perfecto.
- **Alcance de la medición de accesibilidad.** La capa lógica de accesibilidad está
  medida y operativa; la voz extremo a extremo está implementada y funciona de forma
  *offline*.
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

La metodología de desarrollo adoptada es **iterativa e incremental, orientada a un
Producto Mínimo Viable (MVP)** y, de manera distintiva, **dirigida por medición contra
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
  (*held-out*) en español, latencia por acción ≤ 4–5 s (medida en ≈ 2,2 s tras la
  optimización de caché de prefijo). El gate de wake-word (`recall ≥ 0,60 ∧ fp/hr ≤ 1,0`)
  está **cumplido**: la palabra de activación "Baxy" alcanzó **recall 0,872** sobre un
  *held-out* universal de 25 voces y 13 idiomas (por encima del umbral de 0,60), con recall
  perfecto (1,00) en español, inglés, italiano, francés, polaco y ruso.
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
  `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872 sobre held-out de 25 voces/13 idiomas).
  *Evidencia de liberación:* el agente responde por voz de forma offline.
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
  documentación de la hoja de ruta de evolución (binario CPU/Vulkan empaquetado, cliente
  MCP, modelo E4B cuando exista más VRAM) y redacción del informe final.

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
| Fase de Cierre | Jun 2026 (2 sem.) | Backlog Maestro + suite 2.740/0 + hoja de ruta de evolución + informe final | Suite 2.740 aprobadas, 0 fallidas (2026-06-02) |

---

## Referencias del Capítulo II

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

Osterwalder, A., Pigneur, Y., & Clark, T. (2010). *Business Model Generation: A Handbook
for Visionaries, Game Changers, and Challengers*. John Wiley & Sons.

Project Management Institute. (2021). *A Guide to the Project Management Body of Knowledge
(PMBOK Guide)* (7.ª ed.). Project Management Institute.

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

---
---

# CAPÍTULO III — METODOLOGÍA, ALTERNATIVAS Y FACTIBILIDADES, PLAN DE PROYECTO, ARQUITECTURA Y DISEÑO DE ALTO NIVEL, HERRAMIENTAS DE GESTIÓN, MONITOREO Y PRODUCTO FUNCIONAL

> Este capítulo es acumulativo respecto de los Capítulos I (problema, oportunidad, Ishikawa, estado del arte) y II (objetivos, métricas, alcance, normativa, plan inicial). Todos los valores numéricos consignados provienen de mediciones reales registradas en el repositorio del proyecto y no de estimaciones aspiracionales. Las fuentes internas principales son `documentacion/00_producto/{Gemma4_estado_y_limites_2026_05_29.md, ANALISIS_COMPETENCIA.md, AUDITORIA_licencias.md, PRODUCTION_READY.md}`, `documentacion/01_arquitectura/{ARCHITECTURE.md, README.md, 01_delta_system.md, 02_delta_components.md, 04_sequences/}`, `documentacion/datos_crudos/vram_real_medida.csv`, `documentacion/_backlog/BACKLOG_MAESTRO.md` y la memoria persistente del proyecto (`MEMORY.md`), además del historial del repositorio Git.

---

## 3.1. Introducción del Capítulo

El presente capítulo desarrolla la dimensión de **gestión y construcción** del proyecto *Baxy*. A partir del problema y la oportunidad caracterizados en el Capítulo I y de los objetivos, métricas, alcance y restricciones definidos en el Capítulo II, este capítulo expone: (a) la **metodología de gestión** elegida mediante una tabla comparativa ponderada (Scrum) y la **metodología de desarrollo** técnica adoptada (iterativa-incremental dirigida por medición); (b) las **alternativas de solución** evaluadas con sus respectivas factibilidades técnica, económica y social; (c) el **plan de proyecto**, que comprende los requisitos, la arquitectura y el diseño de alto nivel documentado mediante las cuatro vistas del modelo 4+1 de Kruchten (1995); (d) la **selección de herramientas de apoyo a la gestión** (control de versiones Git y backlog maestro); (e) el **plan de monitoreo y control** basado en *gates* medidos; y (f) la **evidencia del producto funcional** —el sistema terminado y operativo que se ejecuta hoy en hardware modesto—.

El principio metodológico rector que atraviesa todo el capítulo es el lema operativo del proyecto: **"medir, no celebrar"**. Ninguna decisión técnica se dio por válida sin un número contrastado contra un criterio de éxito (un *gate*) definido de antemano. Este rigor, formalizado en el documento de instrucciones de trabajo del repositorio (`CLAUDE.md`), es lo que confiere carácter empírico —y no aspiracional— a las cifras de este capítulo.

---

## 3.2. Metodología de Gestión del Proyecto

### 3.2.1. Selección de la metodología mediante tabla comparativa ponderada

La selección de la metodología de gestión no se realizó por preferencia, sino mediante una **tabla comparativa ponderada** que evalúa las metodologías candidatas frente a los factores críticos del proyecto, asignando pesos que suman 1,0 y una escala de evaluación de 1 (deficiente) a 3 (óptimo). Los factores de evaluación se derivan de la naturaleza real del desarrollo: alta incertidumbre técnica (no se sabía de antemano qué modelo entraría en 4 GB de VRAM ni qué tasa de *tool-calling* sería alcanzable), necesidad de retroalimentación frecuente, y la existencia de un único desarrollador en un horizonte académico acotado.

**Tabla 3.1.** *Selección ponderada de la metodología de gestión.*

| Factor de evaluación | Peso | Cascada | Kanban | Scrum |
|---|---|---|---|---|
| Adaptación a requisitos cambiantes / alta incertidumbre técnica | 0,30 | 1 | 3 | 3 |
| Entregas iterativas con valor incremental (MVP temprano) | 0,25 | 1 | 2 | 3 |
| Retroalimentación y validación frecuente (gates por iteración) | 0,20 | 1 | 2 | 3 |
| Ajuste a equipo pequeño / un solo desarrollador | 0,15 | 2 | 3 | 2 |
| Trazabilidad y planificación temporal (horizonte académico) | 0,10 | 3 | 2 | 3 |
| **Puntaje ponderado total** | **1,00** | **1,30** | **2,50** | **2,85** |

*Fuente: Elaboración propia (2026). Escala 1 (deficiente) – 3 (óptimo).*

### 3.2.2. Decisión y justificación

La metodología seleccionada es **Scrum** (puntaje ponderado 2,85), con incorporación de prácticas de **Kanban** (puntaje 2,50) para la visualización del flujo de trabajo en el backlog. La elección se justifica por tres líneas de evidencia:

1. **Compatibilidad con la incertidumbre técnica.** Schwaber y Sutherland (2020) definen Scrum precisamente como un marco para abordar problemas complejos y adaptativos mediante entregas incrementales, lo que coincide con el perfil de este proyecto: comprometerse a un plan cerrado al inicio (Cascada, puntaje 1,30) habría sido inviable cuando ni siquiera era seguro que la solución cupiera en el presupuesto de memoria del hardware objetivo.

2. **Evidencia empírica del propio desarrollo.** El historial del repositorio y la memoria persistente del proyecto documentan un trabajo **organizado explícitamente en sprints iterativos**. Existe rastro real de, entre otros, el "Sprint 7" del *wake-word*, los sprints "Sprint 1 R1–R8" de refactorización arquitectónica, los sucesivos sprints de optimización del enrutador (*router*) y los sprints de *fine-tuning* del modelo. El desarrollo fue, de hecho, gestionado de manera ágil e iterativa antes incluso de formalizar la metodología.

3. **Entrega temprana de valor.** El enfoque ágil permitió liberar tempranamente un incremento funcional —un asistente capaz de responder por voz de forma *offline*— y crecer en capacidades hasta el producto terminado sin congelar el alcance.

Scrum se materializó en el proyecto a través de iteraciones acotadas (los *sprints* del historial), cada una con un objetivo concreto y cerrada contra un criterio de éxito medible; un backlog único como artefacto central de planificación; y revisiones que, en lugar de demostraciones subjetivas, exigían la **validación en vivo** del incremento contra el modelo y el agente reales.

---

## 3.3. Metodología de Desarrollo

La metodología de desarrollo adoptada es **iterativa e incremental, orientada a un Producto Mínimo Viable (MVP)** y, de manera distintiva, **dirigida por medición contra criterios de éxito definidos a priori** (*gates*). Esta es la regla de oro operativa del proyecto, formalizada en su documento de instrucciones de trabajo (`CLAUDE.md`): *ninguna decisión técnica se da por hecha ni se declara exitosa sin un número contra un criterio definido de antemano*.

El ciclo de desarrollo de cada funcionalidad sigue un patrón disciplinado y reproducible de cuatro pasos:

1. **Medir primero.** Antes de implementar, se establece la línea base y el criterio de éxito numérico (el *gate*). Por ejemplo, el *wake-word* se evaluó contra el gate `recall ≥ 0,60 ∧ falsos positivos/hora ≤ 1,0` sobre un conjunto de prueba universal y diverso (no sobre la voz del operador, para preservar la universalidad multilingüe y multi-acento que exige el producto).

2. **Implementar de forma gateada.** Las funcionalidades nuevas se incorporan detrás de un *feature flag* en estado desactivado por defecto (*default-off*), de modo que no alteren el comportamiento estable mientras se validan.

3. **Validar en vivo.** Un cambio que afecta el comportamiento del agente no se considera terminado hasta ejecutarlo contra el modelo de lenguaje y el agente reales, con el mensaje que un usuario escribiría, verificando la cadena completa de herramientas y la respuesta final. Esta exigencia surgió de un fallo concreto: una corrección probada solo con pruebas unitarias *mockeadas* falló en producción porque el modelo, al ser no-determinista, eligió un camino de herramienta distinto al anticipado (corrigió `browser.open` pero el modelo enrutó hacia `browser.search`).

4. **Activar (*flip* a *default-on*).** Solo tras la validación en vivo se promueve la funcionalidad a comportamiento por defecto.

Este enfoque permitió liberar tempranamente un primer incremento funcional e ir agregando capacidades (*tool-calling*, *computer-use*, optimización de latencia, memoria de gustos, accesibilidad por cámara) en iteraciones sucesivas hasta el producto terminado, cada una cerrada contra su propio *gate*. Se mantiene, además, una distinción explícita en todos los reportes entre **validación sintética** (pruebas con datos generados) y **prueba real** (con la voz y el equipo del usuario), en línea con la práctica de medición honesta del proyecto.

---

## 3.4. Propuesta de Alternativas de Solución y sus Factibilidades

### 3.4.1. Alternativas de arquitectura general

Antes de fijar la arquitectura definitiva se evaluaron varias alternativas, tanto a nivel de arquitectura general (nube versus local) como de selección de modelo y de cuantización. La comparación se sustentó en criterios medibles y no en preferencias.

**Tabla 3.2.** *Alternativas de arquitectura general.*

| Criterio | (A) Asistente en la nube (tipo Alexa/Siri) | (B) LLM grande local (Gemma 4 E4B / 26B) | (C) **Gemma 4 E2B-FT local (elegida)** |
|---|---|---|---|
| Privacidad | ✗ Audio enviado a servidores | ✓ Todo local | ✓ Todo local |
| Costo de operación | ✗ Suscripción / APIs pagas | ✓ Gratis | ✓ Gratis |
| Funciona sin internet | ✗ Requiere conexión | ✓ Offline | ✓ Offline |
| Cabe en 4 GB de VRAM | n/a | ✗ No entra (ver 3.4.2) | ✓ Entra (3,36 GB medido) |
| Calidad de *tool-calling* | ✓ Alta (modelos enormes) | ✓ ≈91 % (E4B, estimado) | ◐ ≈75 % (E2B, estimado; mitigado con *forced-retry* + FT) |
| Hardware requerido | Servidores remotos | GPU ≥ 6–8 GB | GPU 4 GB o *fallback* CPU |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md` y `vram_real_medida.csv`.*

La alternativa **(A)** se descartó porque viola las tres restricciones de producto nucleares: privacidad, costo cero y operación *offline*. La alternativa **(B)** se descartó por una razón física medida: no entra en el presupuesto de VRAM del hardware objetivo. Se adoptó la alternativa **(C)** por ser la única que satisface simultáneamente privacidad, gratuidad, operación local y la restricción de 4 GB, asumiendo conscientemente el *trade-off* de un *tool-calling* algo menor, mitigado en producción mediante el mecanismo de reintento forzado de herramienta (*forced-retry*) y el *fine-tuning* del modelo.

### 3.4.2. Alternativa de selección de modelo y cuantización (datos de VRAM medidos)

La decisión E4B → E2B no fue intuitiva, sino que se respaldó con la medición directa del consumo de VRAM de cada variante y cuantización, registrada en `vram_real_medida.csv`:

**Tabla 3.3.** *Consumo de VRAM medido por variante de modelo y cuantización.*

| Variante | Cuantización | Delta de VRAM (MiB) | ¿Cabe en 4 GB? |
|---|---|---|---|
| **E2B** | **Q4_K_M** | **3 371** | **✓ Sí (elegida)** |
| E2B | Q5_K_M | 3 609 | ✓ Sí (más pesada) |
| E4B | UD-IQ2_M (mínima) | 4 057 | ✗ No (excede aun en su cuantización más baja) |
| E4B | Q4_K_M | 5 087 | ✗ No |
| 26B | UD-IQ2_XXS | 12 613 | ✗ No (lejos del presupuesto) |

*Fuente: `documentacion/datos_crudos/vram_real_medida.csv` (2026).*

El dato decisivo es que **ni siquiera la cuantización más agresiva de E4B (UD-IQ2_M, 4 057 MiB) entra en 4 GB**, dejando sin margen para el contexto y la visión residente. La variante E2B-Q4_K_M, con 3 371 MiB, deja espacio suficiente para la visión (≈1,2 GB de mmproj) y la caché KV de atención. Esto convirtió a E2B-Q4_K_M en la única opción viable para el hardware objetivo.

### 3.4.3. Otras decisiones técnicas respaldadas por medición

- **Whisper vs. Parakeet (STT):** ambos corren en CPU con cuantización int8 para no robar VRAM al LLM; Parakeet quedó como motor por defecto y Whisper como *fallback*.
- **Vulkan vs. CUDA (backend de inferencia):** el binario por defecto es solo CUDA; se identificó la necesidad de un *build* CPU/Vulkan para cubrir gráficas integradas Intel/AMD, ampliando el público objetivo.
- **flash-attention desactivada por defecto:** con FA activa, los *prompts* superiores a ~10 K tokens disparan un *crash* CUDA (#22527); con FA desactivada el sistema se midió estable (37/37 turnos), a costa de un *prefill* ≈2× más lento, mitigado.

### 3.4.4. Factibilidad de la solución elegida

#### 3.4.4.1. Factibilidad técnica

La factibilidad técnica está **probada, no proyectada**: el sistema corre hoy en 4 GB de VRAM, con el consumo medido de 3 371 MiB (≈3,36 GB) para el modelo E2B-Q4_K_M, lo que deja margen para visión (≈1,2 GB) y contexto (12 288 tokens de piso). La inferencia se sirve con `llama.cpp`/`llama-server`, una solución madura y ampliamente adoptada en la comunidad para correr LLM cuantizados en hardware de consumo. Los componentes del *stack* están verificados:

- **LLM:** Gemma 4 E2B *fine-tuneado*, GGUF Q4_K_M, servido por `llama-server` (perfil `vram4`); con *fallback* al perfil `cpu` (`-ngl 0`, 0 VRAM) cuando la GPU está ocupada.
- **STT/TTS en CPU:** transcripción (Parakeet/Whisper int8) y síntesis de voz (Piper) corren en CPU y no tocan la VRAM.
- **Estabilidad bajo carga:** se resolvió el *crash* del render WebView2 al competir por la GPU (la UI renderiza por CPU) y el *crash* CUDA #22527 (*flash-attention* desactivada por defecto, estable 37/37).
- **Hardware objetivo:** GPU NVIDIA de 4 GB (GTX 1650 / RTX 3050 4 GB) con piso de 8 GB de RAM; el modo CPU ya extiende el alcance a *laptops* sin gráfica dedicada, y el binario CPU/Vulkan amplía la cobertura a gráficas integradas Intel/AMD.

La conclusión técnica es que el producto es viable en el hardware objetivo y existe una ruta clara (*build* CPU/Vulkan) para extenderlo a "cualquier *laptop* razonable".

#### 3.4.4.2. Factibilidad económica

El proyecto presenta un **costo de operación de USD 0**, lo que constituye su principal ventaja económica frente a la competencia.

**Tabla 3.4.** *Costo operativo comparado.*

| Concepto | Asistente cloud (Alexa+/APIs) | **Baxy** |
|---|---|---|
| APIs de IA por uso | Costo recurrente por token/consulta | USD 0 (modelo local) |
| Suscripción mensual | Sí (tiers de pago) | USD 0 |
| Infraestructura de servidor | A cargo del proveedor | Ninguna (corre en el equipo del usuario) |
| Hardware | GPU cara o nube | GPU 4 GB que el usuario ya posee / CPU |

*Fuente: Elaboración propia (2026), sobre `AUDITORIA_licencias.md`.*

En cuanto a la **vendibilidad por licencias**, la auditoría sobre 511 paquetes concluyó que el *stack* núcleo es permisivo y, por tanto, comercializable: Gemma 4 (Apache-2.0), MediaPipe, OpenCV, Whisper (MIT), `llama.cpp`, Tesseract y `sentence-transformers` son todos de licencias permisivas. Existe **un único bloqueante real** para una eventual comercialización propietaria: `piper-tts` 1.4.2 está bajo GPL-3.0-or-later y se enlaza directamente, lo que obligaría a abrir el código. La auditoría propone una solución de bajo costo —aislar Piper por subproceso (agregación, no derivado) o reemplazarlo por un TTS Apache/MIT (p. ej., Kokoro)—, tras lo cual el producto sería comercializable.

#### 3.4.4.3. Factibilidad social

La factibilidad social del proyecto se articula en tres ejes que constituyen su propuesta de valor diferencial:

- **Privacidad.** Todo el procesamiento ocurre en la máquina del usuario; no se transmite audio, pantalla ni acciones a ningún servidor. Esto alinea al producto con la legislación chilena vigente sobre datos personales (Ley N.º 21.719), protección de la vida privada (Ley N.º 19.628), delitos informáticos (Ley N.º 21.459) y ciberseguridad (Ley N.º 21.663): al no transmitir datos fuera del equipo, el sistema minimiza estructuralmente el riesgo regulatorio.
- **Accesibilidad e inclusión.** El asistente incorpora tres modos de accesibilidad —respaldados por las pautas WCAG 2.1/2.2— que atienden necesidades distintas: el modo `no_vidente` narra cada acción y lee la pantalla a pedido (todo por audio), y el modo `movilidad` ofrece control 100 % por voz para personas que no pueden usar teclado o ratón. Los *gates* de activación por voz se midieron en verde (10/10 activaciones, *recall hands-free* 100 %, siempre con 0 falsos positivos). El control por cámara y gestos (MediaPipe) complementa el acceso para movilidad reducida.
- **Democratización y universalidad.** Al correr en hardware modesto —una *laptop* típica de 4 GB de VRAM, o incluso sin gráfica dedicada en modo CPU—, el sistema democratiza el acceso a un asistente inteligente sin exigir GPU cara ni suscripción. Su carácter multilingüe y multi-acento, resuelto por *embeddings* y no por listas de palabras codificadas por idioma, lo hace utilizable por hablantes de cualquier lengua. El cómputo local evita, además, la huella ambiental de los *datacenters* en la nube.

---

## 3.5. Plan de Proyecto

### 3.5.1. Requisitos del sistema

A partir de los objetivos del Capítulo II, los requisitos del producto se sintetizan en requisitos funcionales (RF) y no funcionales (RNF), todos trazables a un criterio de éxito medible.

**Tabla 3.5.** *Requisitos funcionales y no funcionales con su criterio de verificación.*

| ID | Tipo | Requisito | Criterio de éxito / verificación |
|---|---|---|---|
| RF1 | Funcional | Conversar por voz de extremo a extremo (wake → STT → LLM → TTS) de forma *offline*. | El agente responde por voz sin conexión a internet. |
| RF2 | Funcional | Ejecutar acciones sobre el sistema operativo mediante ~67 herramientas de dominio (67 esquemas únicos expuestos al modelo tras retirar `smart_home`; apps, web, mensajería, volumen, brillo, *computer-use*). | 0 % de herramientas inventadas en producción (modelo FT). |
| RF3 | Funcional | Verificar el resultado de cada acción contra el estado real del SO. | Verificación tri-estado (`True`/`False`/`None`) por registry/UIA/pycaw. |
| RF4 | Funcional | Ofrecer modos de accesibilidad (`normal`, `no_vidente`, `movilidad`) y control por gestos de cámara. | Activación por voz ≥ 90 % ∧ 0 falsos positivos. |
| RNF1 | No funcional | Operar dentro de 4 GB de VRAM. | Delta de VRAM ≤ 4 096 MiB (medido 3 371 MiB). |
| RNF2 | No funcional | Mantener latencia por turno dentro del rango tier-Alexa. | ≤ 4–5 s por turno (p50 por turno ~1,22 s; acción con *tool-call* ~2,2 s). |
| RNF3 | No funcional | Procesar 100 % en local, sin transmisión a la nube. | 0 datos transmitidos; costo de API USD 0. |
| RNF4 | No funcional | Ser multilingüe y multi-acento sin listas de palabras por idioma. | Clasificación por *embeddings* multilingües. |
| RNF5 | No funcional | Sustentarse en software de código abierto y licencias gratuitas. | Auditoría de licencias (*stack* permisivo). |

*Fuente: Elaboración propia (2026), a partir de la tabla de objetivos y métricas del Capítulo II.*

### 3.5.2. Planificación (fases del desarrollo)

La planificación del proyecto se organizó en un horizonte de tres meses, estructurado en una **fase de inicio/análisis**, seguida de **tres iteraciones de desarrollo incremental** y una **fase de cierre**, en coherencia con la metodología iterativa-incremental adoptada. Cada iteración corresponde a un agrupamiento de los sprints reales del historial del proyecto y cierra contra *gates* medidos.

- **Fase 0 — Inicio y Análisis.** Definición del problema, análisis de competencia y de licencias, definición de objetivos SMART con sus criterios de éxito y establecimiento del método dirigido por medición. Entregable: marco del proyecto y backlog inicial.
- **Iteración 1 — Núcleo: LLM local en 4 GB + voz básica.** Selección de modelo respaldada por la medición de VRAM, STT/TTS en CPU y *wake-word*. *Gates*: VRAM ≤ 4 GB (cumplido); *wake-word* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: la palabra de activación "Baxy" alcanzó recall 0,872 sobre un *held-out* de 25 voces y 13 idiomas, con recall perfecto en es/en/it/fr/pl/ru). Evidencia de liberación: el agente responde por voz *offline*.
- **Iteración 2 — Tool-calling, router y acciones (computer-use).** Construcción del enrutador (encoder *fine-tuneado* + *abstain head*), *fine-tuning* del modelo E2B y cascada de *computer-use* (UIA → OCR → visión). *Gates*: router 0,9964 (*held-out* ES); 0 % herramientas inventadas en producción.
- **Iteración 3 — Robustez, latencia, memoria y accesibilidad.** Optimización de latencia (caché de prefijo), capa de memoria de gustos, interfaz web "Baxy field", control por cámara/gestos y *fallback* a CPU. *Gates*: latencia ≤ 5 s; suite de pruebas verde.
- **Fase de Cierre.** Consolidación del Backlog Maestro, estabilización de la suite (2 740 pruebas aprobadas, 0 fallidas) y documentación de la hoja de ruta de evolución.

La siguiente carta Gantt resume el cronograma sobre el horizonte de tres meses; las fechas agrupan los sprints reales del historial del repositorio y cada iteración cierra contra su *gate* medido. La representación gráfica (diagrama de Gantt y flujo gateado) se entrega como artefacto de planificación complementario (figuras "Metodología Gantt" y "flujo gateado" del documento de diagramas del proyecto, `diagramas_mermaid.md`).

**Tabla 3.6.** *Carta Gantt: fases, iteraciones e hitos de cierre.*

| Fase / Iteración | Período aproximado | Foco principal | Hito de cierre (gate) |
|---|---|---|---|
| Fase 0 — Inicio / Análisis | Mar 2026 (2 sem.) | Problema, competencia, licencias, objetivos SMART, método *measure-then-ship* | Marco del proyecto y *backlog* inicial |
| Iteración 1 — Núcleo | Mar–Abr 2026 (~3–4 sem.) | LLM local en 4 GB (E2B-Q4) + herramientas + enrutador + voz básica | VRAM ≤ 4 GB (cumplido); *wake-word* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872) |
| Iteración 2 — Tool-calling / acciones | Abr–May 2026 (~3–4 sem.) | Router (encoder-FT + *abstain*) + computer-use (UIA/OCR/visión) + visión/cámara | Router 0,9964 (held-out ES); 0 % herramientas inventadas en producción |
| Iteración 3 — Robustez / latencia / memoria / accesibilidad | May–Jun 2026 (~3–4 sem.) | Fine-tuning E2B + optimización de latencia + memoria "Jarvis" + UI + fallback CPU | Latencia ≤ 5 s (p50 ≈ 1,22 s); suite verde |
| Fase de Cierre | Jun 2026 (2 sem.) | Backlog Maestro + suite 2 740/0 + hoja de ruta de evolución + informe final | Suite 2 740 aprobadas, 0 fallidas (2026-06-02) |

*Fuente: Elaboración propia (2026); fechas derivadas del historial del repositorio (sprints de 2026-05-26 a 2026-06-02 en la memoria del proyecto).*

---

## 3.6. Arquitectura y Diseño de Alto Nivel (Modelo 4+1 de Kruchten)

El diseño de alto nivel del sistema se documenta mediante una **adaptación del modelo de vistas arquitectónicas "4+1" propuesto por Kruchten (1995)**, el cual describe la arquitectura a través de vistas concurrentes y complementarias —en su forma canónica: Lógica, de Proceso, de Desarrollo y Física, más la vista de Escenarios (+1)—, cada una orientada a un grupo distinto de interesados. Dado que el producto se ejecuta sobre un **nodo único** (el equipo del usuario, sin componentes distribuidos ni concurrencia entre nodos), esta sección adapta el modelo y presenta cuatro vistas: **Lógica, Física, de Despliegue y de Escenarios**; la vista de **Proceso** se subsume en las vistas Lógica (el *hot-path* del turno) y de Despliegue (los procesos `llama-server`/`server`/UI), y la vista de **Desarrollo** se refleja en la organización en sub-paquetes descrita en la vista Lógica. Se reconoce explícitamente esta adaptación para no presentarla como el 4+1 estándar. La arquitectura aquí descrita no es de diseño teórico: fue **verificada leyendo el código y midiendo en vivo** durante una auditoría de tres sesiones (rondas 1–13), según consta en `documentacion/01_arquitectura/ARCHITECTURE.md`.

El norte rector de la arquitectura, ante cualquier conflicto de prioridades, es: (1) preservación de invariantes y aislamiento del *blast radius* (radio de daño); (2) fiabilidad —nunca caer el turno, perder una respuesta ni degradar en silencio—; y (3) latencia dentro del presupuesto tier-Alexa.

### 3.6.1. Diagrama de contexto (propuesta de solución macro)

El siguiente diagrama de contexto (nivel 0 de un Diagrama de Flujo de Datos, o nivel 1 del modelo C4) sitúa al sistema **Baxy** en el centro y representa sus interacciones con los actores externos. La característica determinante es la **ausencia total de la nube**: ninguna flecha sale del perímetro del equipo del usuario hacia servicios remotos.

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
*Figura 3.1. Diagrama de contexto del sistema Baxy. Fuente: Elaboración propia (2026).*

### 3.6.2. Vista Lógica (paquetes del agente)

La Vista Lógica describe la descomposición funcional del sistema en paquetes y su relación. El sistema se organiza en torno a un **hot-path del turno** (el camino de ejecución de un comando del usuario) y un conjunto de subsistemas de soporte. Los paquetes principales son:

- **`agent_core` (orquestación del turno).** Núcleo coordinador en `agent.py` (clase `Gemma4Agent`). Su método `run_content()` es un orquestador delgado que delega en tres fases: `_decide_turn` (elección de modo y enrutamiento), `_execute_turn` (llamada al modelo y despacho de herramientas) y `_finalize_turn` (saneamiento y entrega). Esta separación, producto del refactor del Sprint R1, redujo `run_content` de 2 193 a 22 líneas de código, conteniendo el radio de daño de cambios futuros.
- **`routing` (enrutamiento semántico de intención).** En `routing/planner.py`. Determina qué subconjunto de herramientas ofrecer al modelo en cada turno, combinando clasificación por *embeddings* multilingües (`sentence-transformers` MiniLM), un encoder *fine-tuneado*, una **cabeza de abstención** (*abstain head*), cabezas por herramienta y *clustering*. Alcanza un *holdout* en español de **0,9964** de exactitud y aplica un tope de 5 herramientas por turno (`MAX_SELECTED_TOOLS=5`) como cota anti-*crash*. El criterio de diseño es la **precisión** (ofrecer solo lo necesario), no el *recall*, ya saturado.
- **`voice` (canal de audio "siempre activo").** Cadena `audio_io → vad → wake → pipeline → tts`: captura por PortAudio, detección de actividad de voz (VAD), *wake-word* LiveKit (ONNX), STT (Whisper/Parakeet int8 en CPU) y TTS (Piper en CPU). El invariante clave es que el *callback* de audio nunca bloquea (solo copia y encola), y que el *wake* corre diezmado (~8 Hz en lugar de ~31 Hz) para reducir el uso de CPU en reposo en un ~74 % medido, sin perder *recall*.
- **`computer_use` (control del SO).** En `computer_use_pkg/`, ejecuta acciones sobre cualquier aplicación mediante una **cascada de tres niveles**: accesibilidad UIA → OCR → visión. Garantiza honestidad estructural mediante un verificador tri-estado (`confirmed` ∈ {True, False, None}), donde `None` (no medible) ≠ `False` (falló).
- **`tools` (herramientas de dominio).** Más de sesenta herramientas de dominio (67 esquemas únicos expuestos al modelo tras retirar `smart_home`) en el sub-paquete `domain_tools/` (Sprint R2, 30+ áreas: navegador, apps, WhatsApp, Steam, multimedia, sistema). Cada herramienta se despacha en `try/except` dentro de `_run_with_timeout`, de modo que una herramienta que falla se convierte en `{ok:False, error}` sin abortar el turno.
- **Subsistemas transversales.** `safety_pkg` (clasificador de confirmación, verificadores, políticas de honestidad); la capa de memoria / "Jarvis" (observador de ambiente + perfil de gustos, inspirada en *Generative Agents* de Park et al., 2023); `infra` (`LlamaServerManager`, cliente HTTP, *tracing*, telemetría); y la UI web (React/Vite servida por pywebview).

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
*Figura 3.2. Vista Lógica: paquetes del agente y flujo del hot-path. Fuente: Elaboración propia (2026), a partir de `ARCHITECTURE.md`.*

### 3.6.3. Vista Física (mapeo a hardware)

La Vista Física describe cómo los componentes lógicos se asignan a los recursos de hardware de **un único nodo: el computador del usuario**. No existe nodo remoto. El hardware *target* es una *laptop* o PC modesta con **GPU de 4 GB de VRAM** (GTX 1650 / RTX 3050 4 GB) o, en su defecto, sin GPU dedicada (*fallback* a CPU). El reparto de recursos medido es:

**Tabla 3.6.** *Reparto de recursos físicos del nodo único.*

| Recurso | Componentes asignados | Consumo medido |
|---|---|---|
| **GPU (VRAM)** | LLM (Gemma 4 E2B-Q4_K_M) + encoder de visión (mmproj, residente) + caché KV | **3 371 MiB (delta)**; visión ≈ 1,2 GB |
| **CPU** | STT (Whisper/Parakeet int8), TTS (Piper), enrutador (MiniLM), render de la UI (`--disable-gpu`) | 0 VRAM; ~30 tok/s en CPU de escritorio |
| **RAM** | Modelo mapeado con `mmap` (páginas evictables) + caché KV residente | Piso recomendado: 8 GB; costo fijo KV ≈ 0,5–1 GB |
| **Periféricos** | Micrófono (captura), cámara (control por gestos, opcional), parlante (TTS) | — |

*Fuente: Elaboración propia (2026), datos de `vram_real_medida.csv`.*

El dato de selección de modelo es determinante: **E2B-Q4_K_M (3 371 MiB) entra completo en 4 GB**, mientras que **E4B-Q4_K_M (5 087 MiB) no entra** ni en su cuantización más baja viable. Esta restricción física es la que fuerza el uso de E2B y, por consiguiente, todo el trabajo de *fine-tuning* y optimización.

### 3.6.4. Vista de Despliegue (instalación y ejecución)

La Vista de Despliegue describe cómo se instala y arranca el sistema. El producto se despliega como un conjunto de **procesos locales coordinados**, sin servicios remotos:

1. **Proceso `llama-server`** (motor de inferencia): binario de `llama.cpp` que sirve el modelo GGUF por HTTP en el puerto local `:8080`. Se gestiona desde `LlamaServerManager`, que selecciona un **perfil de VRAM**: `vram4` (por defecto, CUDA, `-ngl` completo) o `cpu` (`-ngl 0`, 0 VRAM, visión desactivada) como *fallback*. El gestor garantiza el cierre del proceso en `atexit` (en Windows el hijo de `Popen` no muere con el padre y retendría ~3,4 GB de VRAM).
2. **Proceso de la UI** (`pywebview` + WebView2 renderizando React/Vite), que renderiza por CPU (`--disable-gpu`) para no competir por la GPU.
3. **Entornos virtuales (venvs) aislados**, por incompatibilidad de dependencias: `.venv` (Python 3.10, runtime del agente), `.venv_livekit` (Python 3.11, *training* del *wake-word*) y un venv Python 3.12 (*fine-tuning* con Unsloth). En tiempo de ejecución NO se importa el paquete `livekit`; los modelos ONNX se sirven con `onnxruntime` puro (verificado idéntico al oficial al 4.º decimal).

Configuración de estabilidad (perfil `vram4`, todo medido): `flash-attn` **OFF** por defecto, contexto **12 288** tokens y *throttle* de ONNX Runtime antes de cargar modelos en *loops* pesados. **Estado del despliegue sin GPU:** operativo a nivel de software (el LLM cae automáticamente al perfil `cpu`); el empaquetado del binario `llama-server` CPU/Vulkan figura en la hoja de ruta de evolución del producto.

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
*Figura 3.3. Vista de Despliegue: procesos y entornos. Fuente: Elaboración propia (2026).*

### 3.6.5. Vista de Escenarios (casos de uso clave)

La Vista de Escenarios concreta las anteriores mediante casos de uso reales, **validados en vivo** contra el agente y el LLM:

- **Escenario 1 — Comando de voz simple ("subí el volumen").** El usuario pronuncia la palabra de activación; el *wake-word* dispara la captura; el STT transcribe en CPU; `run_content` elige el modo, el enrutador ofrece la herramienta de audio, el LLM emite la llamada, el despacho ejecuta y un verificador re-lee el estado del SO (pycaw) para confirmar el cambio; finalmente Piper sintetiza la respuesta. Latencia medida ~2,2 s por acción.
- **Escenario 2 — Computer-use ("abrí Spotify y poné rock" / "instalá DOOM").** El enrutador discrimina entre encadenar herramientas (`abre X y pon Y`) y una misión-con-objetivo (`ve a X y luego a Y` → `computer_use(goal)`). La ejecución usa la cascada UIA → OCR → visión. Casos reales validados: envío real a un grupo de WhatsApp, descarga de Terraria al 99 % vía Steam, y detección honesta de "sin espacio en disco" para DOOM (no miente sobre el resultado).
- **Escenario 3 — Accesibilidad (control manos-libres por voz y gestos).** El módulo `vision_input` permite controlar el cursor y el clic mediante gestos de cámara (MediaPipe). El agente actúa como **orquestador de meta-acciones**: el usuario dicta "escribí X en la app Y" y el agente lo traduce a una acción de *computer-use*. El *grounding* multilingüe permite que funcione para hablantes de distintos idiomas y acentos.

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
*Figura 3.4. Vista de Escenarios: los tres casos de uso clave. Fuente: Elaboración propia (2026), casos validados en `MEMORY.md`.*

---

## 3.7. Herramientas de Apoyo a la Gestión

La gestión del proyecto se apoyó en un conjunto deliberadamente austero de herramientas, coherente con el carácter de desarrollador único y con el principio de reproducibilidad.

**Tabla 3.7.** *Herramientas de apoyo a la gestión y su función.*

| Herramienta | Función en la gestión | Justificación |
|---|---|---|
| **Git (control de versiones)** | Trazabilidad de cada cambio; los *commits* y sus mensajes documentan el "porqué" y las métricas de cada incremento, sirviendo como bitácora de los sprints. | El historial es la evidencia empírica de la gestión por sprints; cada *commit* cierra una unidad de trabajo verificada. |
| **Backlog Maestro** (`BACKLOG_MAESTRO.md`) | Fuente única de verdad de tareas; cada ítem se verifica contra el código real y se marca con estado explícito (aplicado/verificado, pendiente, bloqueado, rechazado, en progreso). | Sustituye un tablero subjetivo por un registro auditable; la verificación cruzada (tres auditores cotejando >50 ítems) constituye el reporte de progreso consolidado. |
| **Suite de pruebas automatizadas** | Control de calidad y de no-regresión; debe permanecer verde como condición de avance. | 2 740 pruebas aprobadas y 0 fallidas en el cierre; incluye guardas estructurales y regresión por idioma/dominio. |
| **Scripts de evaluación reproducibles** | Evaluaciones *batch* versionadas (p. ej., *replay* de 1 071 mensajes reales del usuario). | Garantizan que toda métrica reportada pueda regenerarse desde un script versionado, no desde un artefacto suelto. |
| **Memoria persistente** (`MEMORY.md`) | Registro de decisiones de producto, *gotchas* y *baselines* que no debe perderse entre sesiones de trabajo. | Preserva el conocimiento no obvio (causas raíz, decisiones medidas y rechazadas) como activo del proyecto. |

*Fuente: Elaboración propia (2026).*

---

## 3.8. Plan de Monitoreo y Control

El monitoreo y control del avance se estructuró sobre tres dimensiones —**integridad técnica, valor de negocio y calidad**— y se materializó en instrumentos concretos y reproducibles, no en estimaciones subjetivas de porcentaje de avance.

**a) Gates medidos por subsistema.** Cada objetivo tiene asociado un criterio de éxito numérico que actúa como control de progreso. Los *gates* efectivamente medidos incluyen: consumo de VRAM ≤ 4 GB (medido 3,36 GB para E2B-Q4); 0 % de herramientas inventadas en producción para el modelo *fine-tuneado*; precisión de *routing* de 0,9964 sobre un *held-out* en español; y latencia por acción ≤ 4–5 s (medida en ~2,2 s tras la optimización de caché de prefijo). El *gate* de *wake-word* (`recall ≥ 0,60 ∧ fp/hr ≤ 1,0`) está **cumplido**: la palabra de activación "Baxy" alcanzó un **recall de 0,872** sobre un *held-out* universal de 25 voces y 13 idiomas (por encima del umbral de 0,60), con recall perfecto (1,00) en español, inglés, italiano, francés, polaco y ruso.

**b) Suite de pruebas automatizadas como control de calidad.** La suite creció a lo largo del desarrollo y debe permanecer verde como condición de avance. El estado de cierre reportado es de **2 740 pruebas aprobadas y 0 fallidas** (`BACKLOG_MAESTRO.md`, 2026), incluyendo pruebas de regresión por idioma y por dominio, y guardas estructurales (anti-*loop*, anti-alucinación, verificación de honestidad). El principio anti-regresión es explícito: ningún subgrupo fuerte debe degradarse al mejorar el promedio.

**c) Evaluaciones batch reproducibles.** El control incorpora evaluaciones a gran escala desde scripts versionados. Un ejemplo es el *replay* de los 1 071 mensajes únicos reales del usuario, re-ejecutados a través de la interfaz del agente con la ejecución física *mockeada*, que arrojó una latencia mediana (p50) de 2,5 s, cero errores y cero fugas de herramientas, permitiendo detectar y corregir incidencias sistémicas.

**d) Backlog maestro como tablero de control.** Se consolidó un único **Backlog Maestro** como fuente de verdad, en el que cada ítem se verifica contra el código real y se marca con un estado explícito. Esta verificación cruzada —tres auditores cotejando más de 50 ítems— constituye el reporte de progreso consolidado.

### 3.8.1. Gestión de riesgos

La gestión de riesgos se aborda como un **análisis a priori de la fase de planificación**, cuantificando cada riesgo por su **probabilidad (P)** e **impacto (I)** en escala 1–5, con **exposición E = P × I** (zonas Baja 1–6, Media 7–12, Alta 15–25) y **riesgo residual** esperado tras la mitigación (criterio PMBOK / ISO 31000). La matriz completa de doce riesgos consta en la Sección 2.7.4 (Plan de Gestión de Riesgos) del Capítulo II; aquí se reproduce el extracto de los riesgos de mayor exposición.

**Tabla 3.8.** *Matriz de riesgos del proyecto (evaluación a priori; extracto de mayor exposición).*

| ID | Riesgo | P | I | E = P×I | Zona | Estrategia de mitigación preventiva | Residual |
|---|---|:-:|:-:|:-:|:-:|---|:-:|
| R1 | El modelo capaz no entra en 4 GB de VRAM | 5 | 5 | **25** | Alta | Medir el consumo real de cada candidato antes de comprometerse; modelo de menor tamaño como plan B (E2B). | 6 |
| R2 | *Tool-calling* poco fiable en un modelo de 2B | 4 | 5 | **20** | Alta | *Fine-tuning* con *gate* de 0 % de herramientas inventadas + router (*abstain head*) + reintento forzado. | 8 |
| R5 | El asistente ejecuta acciones dañinas/no deseadas | 4 | 5 | **20** | Alta | Confirmación obligatoria para acciones de riesgo + honestidad estructural (verificar antes de declarar). | 8 |
| R3 | La voz local no alcanza latencia usable (≤ 4–5 s) | 4 | 4 | **16** | Alta | Presupuesto de latencia como criterio de éxito; STT/TTS en CPU int8; optimización de caché de prefijo. | 8 |
| R4 | Inestabilidad del *stack* de inferencia en GPU modesta | 4 | 4 | **16** | Alta | Validar estabilidad antes de adoptar; defaults conservadores; *fallback* a CPU. | 6 |
| R6 | El modelo "alucina" haber actuado | 4 | 4 | **16** | Alta | Verificación tri-estado por el estado real del SO; declara "no verificado" si no puede comprobarlo. | 6 |
| R10 | El alcance excede el tiempo de un estudiante/semestre | 4 | 4 | **16** | Alta | Desarrollo iterativo (Scrum) con MVP temprano e incrementos gateados; recortar alcance antes que calidad. | 8 |
| R12 | Declarar logros sin medición ("celebrar sin medir") | 4 | 4 | **16** | Alta | Regla *medir, no celebrar*: *gate* numérico por objetivo + validación en vivo obligatoria. | 6 |

*Fuente: Elaboración propia (2026). Evaluación a priori de la planificación; el seguimiento de los riesgos efectivamente gatillados se documenta en la ejecución por iteraciones.*

---

## 3.9. Evidencia del Producto Funcional

El proyecto **no es una propuesta teórica, sino un producto funcional terminado y operativo** que se ejecuta hoy en hardware modesto. Esta sección documenta qué hace el sistema, sustentado en mediciones reales.

### 3.9.1. Qué hace el sistema

- **Conversa por voz de extremo a extremo y de forma *offline*.** El usuario activa el asistente con una palabra clave (*wake-word*); su voz se transcribe en CPU; el modelo Gemma 4 E2B *fine-tuneado* genera la respuesta o decide invocar una herramienta; y la respuesta se sintetiza por voz (Piper, en CPU). Toda la cadena ocurre en la máquina del usuario, sin transmitir datos a la nube.
- **Controla el sistema operativo mediante más de sesenta herramientas de dominio** (67 esquemas únicos expuestos al modelo tras retirar `smart_home`)**.** Abre y opera aplicaciones, navega la web, gestiona mensajería (WhatsApp), instala y controla juegos (Steam), ajusta volumen y brillo, gestiona archivos y recordatorios, y ejecuta *computer-use* sobre cualquier aplicación mediante la cascada UIA → OCR → visión.
- **Opera dentro de 4 GB de VRAM.** El barrido de VRAM sobre los modelos **base** midió que E2B-Q4_K_M base consume 3 371 MiB (≈3,36 GB) de delta de VRAM (`documentacion/datos_crudos/vram_real_medida.csv`), dato que decidió el pivote E4B → E2B y deja margen para la visión residente (≈1,2 GB) y la caché KV; el modelo *fine-tuneado* finalmente desplegado consume 2,07 GB de VRAM efectiva (medición del despliegue del FT v2, `dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`; el CSV cubre los modelos base, no el FT), con aún más holgura dentro de los 4 GB.
- **Responde con latencia tier-Alexa.** La latencia mediana por acción es ~2,2 s tras la optimización de caché de prefijo, holgadamente dentro del presupuesto de 4–5 s; las colas problemáticas (Spotify) se redujeron de 12,6 s a 5,6 s.
- **Garantiza honestidad estructural.** No declara haber ejecutado una acción si no puede verificarla contra el estado real del SO (registry, UIA, pycaw), mediante verificación tri-estado (`True`/`False`/`None`).
- **Ofrece modos de accesibilidad y control multilingüe.** Tres modos (`normal`, `no_vidente`, `movilidad`) y control por cámara/gestos (MediaPipe), con clasificación multilingüe por *embeddings* en lugar de listas de palabras por idioma.
- **Cuenta con una interfaz web propia ("Baxy field").** Renderizada por CPU (`--disable-gpu`) para no competir por la GPU, con retroalimentación visual del estado del asistente.

### 3.9.2. Evidencia de liberación por iteración

**Tabla 3.9.** *Evidencia de liberación del sistema por iteración, contra su gate.*

| Iteración | Incremento liberado | Gate | Resultado medido |
|---|---|---|---|
| 1 — Núcleo | Agente responde por voz *offline* en 4 GB | VRAM ≤ 4 GB; router *holdout* | 3 371 MiB; router 0,9964 |
| 2 — Voz/percepción y acciones | *Wake-word* + STT/TTS + computer-use; casos de voz reales | *wake* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (gate **cumplido**: recall 0,872 sobre held-out de 25 voces/13 idiomas); FT | 10/10 casos de voz PASS en vivo; 0 % herramientas inventadas; *wake-word* recall 0,872 (perfecto en es/en/it/fr/pl/ru); voz extremo a extremo operativa *offline* |
| 3 — Robustez/latencia/accesibilidad | Optimización, memoria de gustos, accesibilidad, *fallback* CPU | latencia ≤ 5 s; suite verde | p50 ~1,22 s; **2 740 tests verdes, 0 fallos** |

*Fuente: Elaboración propia (2026), a partir de `04_diseno_ejecucion_cierre.md`, `BACKLOG_MAESTRO.md` y `MEMORY.md`.*

### 3.9.3. Limitaciones honestas del sistema

En coherencia con el principio "medir, no celebrar", se declaran las limitaciones reales del sistema: el *tool-calling* del modelo de 2B se estima en ~75 % de exactitud de selección (frente al ~91 % del modelo E4B; estimación comparativa, no medición formal), *trade-off* aceptado por entrar en 4 GB; la visión se desactiva en modo CPU por costo; sin GPU NVIDIA el sistema opera cayendo al perfil CPU (el empaquetado del binario CPU/Vulkan figura en la hoja de ruta de evolución); y el techo del *computer-use* está limitado por el estado del arte (benchmarks de referencia ~52,5 % en WindowsAgentArena). Estas limitaciones se declaran con transparencia y no se ocultan.

---

## 3.10. Síntesis del Capítulo

El Capítulo III demuestra que el proyecto *Baxy* no es una propuesta teórica, sino un sistema construido, medido y operativo. Se seleccionó **Scrum** como metodología de gestión mediante una tabla ponderada (puntaje 2,85), coherente con la evidencia de sprints reales del repositorio, y una metodología de **desarrollo iterativo dirigido por medición** (*measure-then-ship*). Las **alternativas** de arquitectura y modelo se decidieron con datos de VRAM medidos (E2B-Q4_K_M es la única opción que entra en 4 GB), y las tres **factibilidades** —técnica (probada en hardware real), económica (costo de operación USD 0) y social (privacidad, accesibilidad, universalidad)— resultaron favorables. La **arquitectura** se documentó con las cuatro vistas del modelo 4+1 de Kruchten, verificadas contra el código. La **gestión** se apoyó en Git, un Backlog Maestro auditable y una suite de pruebas verde, y el **monitoreo** se ejerció mediante *gates* numéricos y una matriz de riesgos cuantificada (probabilidad × impacto) evaluada a priori en la planificación. Finalmente, la **evidencia del producto funcional** confirma que el sistema cumple sus criterios de éxito medibles: opera por voz, *offline*, dentro de 4 GB, con latencia tier-Alexa y honestidad estructural.

---

## Referencias del Capítulo III

Kruchten, P. (1995). The 4+1 view model of architecture. *IEEE Software, 12*(6), 42–50. https://doi.org/10.1109/52.469759

Park, J. S., O'Brien, J. C., Cai, C. J., Morris, M. R., Liang, P., & Bernstein, M. S. (2023). *Generative agents: Interactive simulacra of human behavior*. arXiv. https://arxiv.org/abs/2304.03442

Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide: The definitive guide to Scrum: The rules of the game*. Scrum.org. https://scrumguides.org/

Villacura, E. (2026a). *BACKLOG_MAESTRO.md* [documento interno del proyecto Baxy]. Repositorio del proyecto.

Villacura, E. (2026b). *Baxy — Estado técnico y límites de hardware* [documento interno]. Repositorio del proyecto.

Villacura, E. (2026c). *vram_real_medida.csv* [conjunto de datos de mediciones]. Repositorio del proyecto.

Villacura, E. (2026d). *AUDITORIA_licencias.md* [auditoría de licencias de 511 paquetes, documento interno]. Repositorio del proyecto.

*Fuente: Elaboración propia (2026). Las cards de los modelos (Gemma 4, Whisper, LiveKit, Piper, MediaPipe), los benchmarks WindowsAgentArena/OSWorld y la legislación chilena citada se consolidan en la sección **Referencias (general, APA 7)** al final de este portafolio.*

---
---

# Referencias (general, APA 7)

> Sección única de referencias en formato **APA 7**, en orden alfabético, que consolida
> todas las fuentes citadas a lo largo de los tres capítulos del portafolio (las listas
> por capítulo se reproducen para conveniencia de lectura; esta sección es la consolidación
> única exigida por el temario). Las fuentes de mercado, privacidad y literatura técnica de
> *edge LLM* fueron verificadas contra su página original el 2026-06-03 (autores, años y
> cifras contrastados). Las entradas marcadas **[VERIFICAR]** existen y se citan en el
> cuerpo, pero su entrada bibliográfica exacta (autoría, año o URL) debe reconfirmarse
> contra la fuente primaria antes de la impresión final, en coherencia con el principio
> metodológico del proyecto de no presentar como verificado lo que no se contrastó.

Android Developers. (s. f.). *Gemini Nano*. Google. Recuperado el 3 de junio de 2026, de
https://developer.android.com/ai/gemini-nano

Astute Analytica. (2026, 10 de febrero). *Voice assistant market to reach US$ 59.9 billion
by 2033 driven by mass consumer adoption, enterprise voice AI, and smart device
proliferation*. GlobeNewswire.
https://www.globenewswire.com/news-release/2026/02/10/3235286/0/en/Voice-Assistant-Market-to-Reach-US-59-9-Billion-by-2033-Driven-by-Mass-Consumer-Adoption-Enterprise-Voice-AI-and-Smart-Device-Proliferation-Astute-Analytica.html

Biblioteca del Congreso Nacional de Chile. (1970). *Ley N.º 17.336, sobre Propiedad
Intelectual*. Diario Oficial de la República de Chile. https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (1999). *Ley N.º 19.628, sobre Protección de la
Vida Privada*. Diario Oficial de la República de Chile. https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (2022). *Ley N.º 21.459, que establece normas
sobre delitos informáticos*. Diario Oficial de la República de Chile.
https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (2024a). *Ley N.º 21.663, Ley Marco de
Ciberseguridad*. Diario Oficial de la República de Chile. https://www.bcn.cl/leychile

Biblioteca del Congreso Nacional de Chile. (2024b). *Ley N.º 21.719, que regula la
protección y el tratamiento de los datos personales y crea la Agencia de Protección de
Datos Personales*. Diario Oficial de la República de Chile. https://www.bcn.cl/leychile

Bonatti, R., Zhao, D., Bonacci, F., Dupont, D., Abdali, S., Li, Y., Lu, Y., Wagle, J.,
Koishida, K., Bucker, A., Jang, L., & Hui, Z. (2024). *Windows Agent Arena: Evaluating
multi-modal OS agents at scale* [Preprint]. arXiv. https://arxiv.org/abs/2409.08264

Cai, G., Tian, R., Yang, L., Jia, Y., Li, L., & Wang, J. (2026). Efficient inference for
edge large language models: A survey. *Tsinghua Science and Technology, 31*(3).
https://doi.org/10.26599/TST.2025.9010166

Google DeepMind. (s. f.). *Gemma — Model card* [tarjeta de modelo]. Google. Recuperado el
3 de junio de 2026, de https://ai.google.dev/gemma **[VERIFICAR: año/versión de la card]**

Kruchten, P. (1995). The 4+1 view model of architecture. *IEEE Software, 12*(6), 42–50.
https://doi.org/10.1109/52.469759

LiveKit. (s. f.). *livekit-wakeword: An open-source wake word library* [biblioteca de
software / modelo de detección de palabra de activación]. GitHub. Recuperado el 3 de junio
de 2026, de https://github.com/livekit/livekit-wakeword **[VERIFICAR: año de la versión]**

MediaPipe (Google). (s. f.). *Hand landmarks detection guide* [documentación de modelo].
Google. Recuperado el 3 de junio de 2026, de
https://ai.google.dev/edge/mediapipe/solutions/vision/hand_landmarker **[VERIFICAR]**

OpenAI. (2022). *Robust speech recognition via large-scale weak supervision (Whisper)*
[tarjeta de modelo / repositorio]. https://github.com/openai/whisper **[VERIFICAR]**

Osterwalder, A., Pigneur, Y., & Clark, T. (2010). *Business Model Generation: A Handbook
for Visionaries, Game Changers, and Challengers*. John Wiley & Sons.

Park, J. S., O'Brien, J. C., Cai, C. J., Morris, M. R., Liang, P., & Bernstein, M. S.
(2023). *Generative agents: Interactive simulacra of human behavior* [Preprint]. arXiv.
https://arxiv.org/abs/2304.03442

Project Management Institute. (2021). *A Guide to the Project Management Body of Knowledge
(PMBOK Guide)* (7.ª ed.). Project Management Institute.

Rhasspy. (s. f.). *Piper: A fast, local neural text to speech system* [sistema de síntesis
de voz / modelos VITS-ONNX]. GitHub. Recuperado el 3 de junio de 2026, de
https://github.com/rhasspy/piper **[VERIFICAR: año de la versión]**

Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide: The definitive guide to Scrum: The
rules of the game*. Scrum.org. https://scrumguides.org/

Secure Data Recovery Services. (2024, 5 de agosto). *Listening in: Privacy concerns of
voice assistants*. https://www.securedatarecovery.com/blog/smart-device-privacy-concerns

Villacura, E. (2026a). *AUDITORIA_licencias.md* [auditoría de licencias de 511 paquetes,
documento interno del proyecto Baxy]. Repositorio del proyecto.

Villacura, E. (2026b). *BACKLOG_MAESTRO.md* [documento interno del proyecto Baxy].
Repositorio del proyecto.

Villacura, E. (2026c). *Baxy — Estado técnico y límites de hardware* [documento
interno]. Repositorio del proyecto.

Villacura, E. (2026d). *vram_real_medida.csv* [conjunto de datos de mediciones].
Repositorio del proyecto.

Web Accessibility Initiative (W3C). (2023). *Web Content Accessibility Guidelines (WCAG)
2.2* [recomendación]. World Wide Web Consortium. https://www.w3.org/TR/WCAG22/
**[VERIFICAR]**

Zhang, C., Huang, H., Ni, C., Mu, J., Qin, S., He, S., Wang, L., Yang, F., Zhao, P., Du, C.,
Li, L., Kang, Y., Jiang, Z., Zheng, S., Wang, R., Qian, J., Ma, M., Lou, J.-G., Lin, Q.,
Rajmohan, S., & Zhang, D. (2025). *UFO2: The desktop AgentOS* [Preprint]. arXiv.
https://arxiv.org/abs/2504.14603

Zheng, Y., Chen, Y., Qian, B., Shi, X., Shu, Y., & Chen, J. (2024). *A review on edge large
language models: Design, execution, and applications* [Preprint]. arXiv.
https://arxiv.org/abs/2410.11845

---
---

*Fin del Portafolio de Proyectos — Baxy. Capítulos I, II y III acumulativos.
Universidad Andrés Bello, INSW410 — Portafolio de Proyectos. Emmanuel Villacura Arancibia, 2026.*
