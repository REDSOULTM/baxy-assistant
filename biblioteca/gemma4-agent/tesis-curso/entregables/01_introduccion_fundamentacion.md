**Introducción y Fundamentación del Problema**

> Secciones del Informe Final correspondientes a los bloques 1.1 (Introducción) y
> 2.1–2.4 (Fundamentación del Problema: Contexto, Situación Actual, Técnica de
> Identificación de la Problemática y Descripción de Causas). Redactado en español
> académico, tercera persona, con citas en formato APA 7. Datos técnicos anclados en
> mediciones reales del proyecto (`documentacion/datos_crudos/vram_real_medida.csv`,
> `ANALISIS_COMPETENCIA.md`, `Gemma4_estado_y_limites_2026_05_29.md`).

---

# Introducción

Durante la última década, los asistentes de voz se consolidaron como una de las
interfaces de interacción humano-computador de mayor crecimiento. La conversación en
lenguaje natural —dictar un mensaje, consultar el clima, abrir una aplicación o
controlar dispositivos del hogar mediante la palabra— dejó de ser una promesa de la
ciencia ficción para convertirse en una práctica cotidiana de cientos de millones de
personas. El mercado global de asistentes de voz se valoró en aproximadamente USD 9.163
mil millones en 2025 y se proyecta que alcance USD 59.900 millones hacia 2033, con una
tasa de crecimiento anual compuesta (CAGR) cercana al 26,8 %, sostenida por más de 8.400
millones de dispositivos habilitados para voz operando a nivel mundial (Astute Analytica,
2026). Estas cifras evidencian que el control por voz se ha vuelto un canal de acceso
masivo a la tecnología.

Sin embargo, este crecimiento descansa sobre una arquitectura tecnológica con
limitaciones estructurales que afectan directamente al usuario final. Los asistentes
dominantes del mercado —tales como Amazon Alexa, Apple Siri y Google Assistant—
operan mayoritariamente bajo un paradigma dependiente de la nube: el audio capturado por
el micrófono se transmite a servidores remotos propiedad de las empresas proveedoras,
donde se realiza el reconocimiento de voz, la comprensión del lenguaje y la generación de
la respuesta. Este modelo, si bien permite desplegar modelos de gran capacidad, traslada
los datos de voz del usuario —información biométrica y conductual sensible— fuera de su
control, exige conectividad permanente a internet, introduce latencia de red y, en sus
versiones más avanzadas, conlleva costos de suscripción o el uso de interfaces de
programación de aplicaciones (API) de pago.

A nivel micro, se identifica un vacío concreto: **no existe en el mercado un asistente de
voz que sea, simultáneamente, local (que procese todo en el equipo del usuario), privado
(que no envíe audio ni datos a la nube), gratuito y de código abierto, multilingüe, y que
opere en hardware modesto —específicamente en una tarjeta gráfica de apenas 4 GB de
memoria de video (VRAM)—**. El análisis comparativo de diez soluciones contemporáneas del
estado del arte confirma que ninguna cumple este conjunto de restricciones de forma
conjunta: todas son soluciones en la nube, dependen de modelos de gran tamaño o
constituyen marcos de orquestación para desarrolladores, no productos de voz para el
usuario final en hardware limitado (documentación interna del proyecto,
`ANALISIS_COMPETENCIA.md`, 2026).

El presente proyecto, denominado **Baxy**, resuelve este vacío: es un
asistente de voz para el sistema operativo Windows, desarrollado y operativo, que opera
íntegramente de manera local y privada, sin transmitir información a servidores externos,
empleando un modelo de lenguaje Gemma 4 E2B ajustado mediante *fine-tuning* y servido con
la biblioteca de código abierto llama.cpp. El asistente controla el sistema operativo a
través de más de sesenta herramientas de dominio (67 esquemas únicos expuestos al modelo tras
retirar `smart_home`): control por voz, visión por cámara,
automatización de la interfaz —*computer-use*— mediante accesibilidad UIA y reconocimiento
óptico de caracteres, gestión de aplicaciones y navegación web; e incorpora modos de
accesibilidad orientados a personas con movilidad reducida.

La importancia de esta investigación radica en que aborda, de manera simultánea, tres
dimensiones críticas habitualmente tratadas por separado: la **privacidad** (los datos
nunca abandonan el equipo), la **accesibilidad económica** (operación sin costo
recurrente, sobre hardware que el usuario ya posee) y la **inclusión tecnológica**
(funcionamiento en máquinas modestas y soporte multilingüe). El objetivo general —desarrollar
dicho asistente respetando límites de hardware verificables— se alcanzó; los objetivos
específicos se cumplieron: la ejecución del modelo dentro de 4 GB de VRAM, la fiabilidad en la
selección de acciones (*tool-calling*), la latencia por turno de voz comparable a la de
los asistentes comerciales y la garantía de operación local con honestidad estructural en
las respuestas. Metodológicamente, el proyecto adoptó un enfoque iterativo basado en la
medición sistemática contra criterios de éxito definidos previamente (*gates*), donde
ninguna decisión técnica se dio por válida sin evidencia cuantitativa.

El presente informe se organiza de la siguiente manera. El primer capítulo expone la
**fundamentación del problema**, incluyendo el contexto de la industria, la situación
actual y el análisis de causas mediante un diagrama de Ishikawa. Los capítulos
subsiguientes desarrollan el **alcance del proyecto** con sus objetivos y métricas, la
**propuesta de solución**, el **plan de proyecto** con su gestión de riesgos, el **diseño
de alto nivel** según las vistas arquitectónicas, la **ejecución** documentada en tres
iteraciones, y finalmente los **trabajos futuros** y las **conclusiones**.

---

# Fundamentación del Problema

## Contexto

La industria de los asistentes de voz constituye uno de los segmentos de mayor dinamismo
dentro del ecosistema de la inteligencia artificial aplicada al consumidor. Como se
señaló, el mercado se valoró en aproximadamente USD 9.163 mil millones en 2025 y se
proyecta una expansión hasta los USD 59.900 millones en 2033 (CAGR ≈ 26,8 %), impulsada
por una base instalada que ya supera los 8.400 millones de dispositivos habilitados para
voz (Astute Analytica, 2026). El uso es masivo: la misma fuente reporta, para el mercado de
Estados Unidos, una penetración mensual del 88,1 % entre los propietarios de teléfonos
inteligentes y un 60 % de uso semanal de la búsqueda por voz, frente al 45 % en 2024
(Astute Analytica, 2026). Este segmento está dominado por un
reducido grupo de actores —Amazon (Alexa), Apple (Siri) y Google (Google Assistant)— cuyo
modelo operativo descansa, de forma predominante, en el procesamiento en la nube.

De manera paralela, se observa una tendencia tecnológica creciente hacia la **inteligencia
artificial en el borde (*edge AI*)**, es decir, hacia la ejecución de modelos directamente
en el dispositivo del usuario en lugar de en servidores remotos. La literatura técnica
reciente identifica que el despliegue de modelos de lenguaje de gran escala (LLM) en el
dispositivo elimina la necesidad de transmitir datos en bruto del usuario hacia servidores
externos, manteniendo toda la información personal de forma localizada, lo que mitiga los
riesgos de privacidad, costo, latencia y fiabilidad de red propios del enfoque en la nube;
no obstante, este despliegue se ve severamente restringido por las limitaciones de
recursos del hardware de borde (Zheng et al., 2024; Cai et al., 2026). Fabricantes como
Google (con Gemini Nano integrado en Android) ya han comenzado a ejecutar modelos
localmente en sus dispositivos, lo que confirma la direccionalidad de la industria hacia
el procesamiento en el borde (Android Developers, s. f.).

En tercer lugar, existe una preocupación documentada y creciente respecto de la
**privacidad** en el uso de asistentes de voz. Una encuesta aplicada a más de mil personas
en Estados Unidos reveló que cerca del 60 % de los usuarios manifestaba estar, al menos
ocasionalmente, preocupado por su privacidad al utilizar asistentes de voz; que el 49 %
desconocía que estos dispositivos escuchan de forma continua a la espera de la palabra de
activación; que el 68 % nunca había adoptado medida alguna para mejorar su privacidad; y,
de manera reveladora, que el 77 % se mostraría más propenso a usar asistentes de voz si
estos ofrecieran mayores garantías de privacidad y transparencia (Secure Data Recovery
Services, 2024). Esta última cifra es particularmente significativa para el presente
proyecto, pues señala una demanda insatisfecha por soluciones que prioricen la privacidad.

En síntesis, el contexto configura una tensión clara: una industria masiva y en expansión,
una tendencia tecnológica que habilita el procesamiento local, y una preocupación
ciudadana por la privacidad que el modelo dominante no resuelve. El cruce de estos tres
vectores delimita la oportunidad que este proyecto materializó.

## Situación Actual

El proceso actual mediante el cual un usuario interactúa con un asistente de voz comercial
puede describirse, de forma simplificada, en la siguiente secuencia: (1) el usuario emite
un comando de voz; (2) el micrófono del dispositivo captura el audio; (3) el dispositivo
transmite ese audio a través de internet hacia los servidores del proveedor; (4) en la
nube se realiza el reconocimiento del habla y la interpretación de la intención; (5) los
servidores generan la respuesta; (6) la respuesta se devuelve por la red al dispositivo;
y (7) el dispositivo reproduce el resultado. Este flujo, aparentemente fluido para el
usuario, presenta una serie de problemas estructurales que destruyen valor:

**Dependencia de la nube y de la conectividad.** El asistente no funciona sin conexión a
internet. Una caída de red, una zona sin cobertura o una interrupción del servicio del
proveedor inhabilitan por completo la herramienta. El usuario no posee control real sobre
la disponibilidad de un servicio del que depende para tareas cotidianas.

**Pérdida de privacidad.** El audio del usuario —que constituye información biométrica y
puede contener datos sensibles— se transmite y, en muchos casos, se almacena en servidores
de terceros. La evidencia indica que una proporción considerable de usuarios desconoce el
alcance de esta captura (Secure Data Recovery Services, 2024). El control sobre los datos
de voz queda, de hecho, en manos del proveedor y no del usuario.

**Latencia de red.** Cada turno de interacción requiere, como mínimo, un viaje de ida y
vuelta de los datos a través de internet. Esta latencia se suma al tiempo de
procesamiento y depende de condiciones de red ajenas al control del usuario, degradando la
experiencia en conexiones lentas o congestionadas.

**Costo.** Las versiones más avanzadas de estos servicios, o el acceso programático a sus
capacidades mediante API, conllevan costos de suscripción o por uso. El usuario que desea
funcionalidades plenas debe asumir un gasto recurrente, y el desarrollador que quisiera
construir sobre estas plataformas queda atado a tarifas externas.

**Datos de voz en servidores ajenos.** Más allá de la latencia y el costo, el problema de
fondo es la **asimetría de control**: la voz del usuario, una vez transmitida, deja de ser
suya en términos prácticos. La transparencia sobre qué se almacena, por cuánto tiempo y con
qué fines de perfilamiento es limitada, y la posibilidad efectiva de borrar o controlar
esos datos resulta restringida.

Frente a este panorama, las alternativas locales existentes en el estado del arte tampoco
resuelven el problema para el usuario objetivo. El análisis de competencia interno examinó
diez soluciones representativas y concluyó que ninguna corre de manera local en 4 GB de
VRAM con capacidad de voz: las orientadas a *computer-use* (como Agent-S u OS-Copilot)
dependen de modelos en la nube (GPT-4V, Gemini); los marcos de orquestación (AutoGen,
LangGraph, AutoGPT) requieren modelos grandes; y la única solución de asistencia personal
por voz comparable (Mark-XXXIX) depende de la API de Gemini en la nube, que es de pago
(documentación interna del proyecto, `ANALISIS_COMPETENCIA.md`, 2026). En consecuencia, el
usuario que valora la privacidad, carece de hardware potente o no desea pagar
suscripciones, queda hoy sin una opción que satisfaga sus necesidades.

## Técnica de Identificación de la Problemática: Diagrama de Ishikawa

Para identificar y organizar de manera sistemática las causas que originan la
problemática, se aplicó la técnica del **diagrama de Ishikawa** (también denominado diagrama
de causa-efecto o de espina de pescado). Esta técnica permite descomponer un problema
central en categorías de causas, facilitando el análisis de su origen antes de proponer una
solución. El problema central (la "cabeza del pescado") se define como:

> **"No existe un asistente de voz local, privado y gratuito que sea usable en hardware
> modesto (≤ 4 GB de VRAM)."**

Las causas se organizaron en seis categorías (adaptación de las clásicas "6M" del método de
Ishikawa al dominio tecnológico del proyecto). La siguiente representación textual y
tabular sintetiza la espina de pescado:

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

**Tabla 1.** *Diagrama de Ishikawa: categorías y causas de la problemática*

| Categoría (6M) | Causas identificadas |
|---|---|
| **Tecnología / Máquina** | LLMs de gran tamaño no entran en 4 GB de VRAM; el encoder de visión (mmproj) consume ~1,2 GB; el hardware del usuario objetivo es modesto (GPU de 4 GB o sin GPU dedicada). |
| **Costo** | Las API en la nube son de pago; las GPU de alta gama son costosas; las versiones avanzadas de los asistentes exigen suscripción recurrente. |
| **Privacidad** | El audio del usuario se envía a la nube; los datos de voz se almacenan en servidores ajenos; el usuario pierde el control sobre su información. |
| **Conectividad** | El servicio requiere conexión permanente a internet; la latencia de red se suma a cada turno de interacción; no hay operación offline. |
| **Idioma / Usuario** | Los competidores son monolingües o anglocéntricos; existe *hardcodeo* de idioma; se excluye a hablantes de otros idiomas y acentos. |
| **Método** | El *tool-calling* (selección de la acción correcta) es difícil en modelos pequeños (estimado ~75 % de acierto frente a ~91 % en modelos grandes); riesgo de que el modelo "alucine" acciones; ausencia de medición contra criterios de éxito conduce a declarar logros no verificados. |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md`,
`Gemma4_estado_y_limites_2026_05_29.md` y `vram_real_medida.csv`.*

## Descripción de Causas

A continuación se describe cada causa identificada en el diagrama de Ishikawa,
especificando **en qué consiste**, **qué efecto produce** y **cómo incide** en la
problemática central. Se distingue, además, entre causas principales (las de mayor peso en
el origen del problema) y secundarias, y entre factores internos (propios de la tecnología)
y externos (propios del entorno de mercado y del usuario).

#### Categoría Tecnología / Máquina (causa principal)

- **Los modelos de lenguaje grandes no caben en 4 GB de VRAM.** *En qué consiste:* los
  modelos de lenguaje de mayor capacidad requieren varios gigabytes de memoria de video
  incluso en sus cuantizaciones más agresivas. *Qué efecto produce:* obliga a renunciar al
  modelo más capaz y a buscar uno que entre en el presupuesto de memoria. *Cómo incide:* es
  la restricción de hardware fundamental que define la viabilidad del proyecto. La medición
  real lo confirma: el modelo Gemma 4 E2B en cuantización Q4_K_M ocupa 3.371 MiB de VRAM
  (delta), por lo que entra en una tarjeta de 4 GB; en cambio, el modelo E4B en la misma
  cuantización ocupa 5.087 MiB y **no entra**, y las variantes de 26B/31B superan los
  12.000 MiB (`vram_real_medida.csv`, 2026). Esta es la causa raíz que motivó el pivote de
  E4B a E2B en el diseño.

- **El encoder de visión pesa ~1,2 GB.** *En qué consiste:* habilitar la capacidad de
  visión (procesar imágenes de la pantalla) requiere mantener residente un componente
  adicional (mmproj) que consume aproximadamente 1,2 GB. *Qué efecto produce:* reduce el
  margen disponible para el resto del modelo y del contexto. *Cómo incide:* en una GPU de
  exactamente 4 GB, el margen tras cargar la visión es estrecho, lo que obliga a gestionar
  la visión como un recurso residente y gateado, y a desactivarla en el modo de respaldo por
  CPU (`Gemma4_estado_y_limites_2026_05_29.md`, 2026).

- **Hardware de usuario modesto.** *En qué consiste:* el usuario objetivo posee una laptop o
  PC típica, con GPU de 4 GB o sin GPU dedicada. *Qué efecto produce:* impide asumir
  hardware de gama alta como base de diseño. *Cómo incide:* es un factor externo que fija el
  techo de recursos y, junto con la causa anterior, define el carácter "modesto" del target.

#### Categoría Costo (causa principal, factor externo)

- **API en la nube de pago y suscripciones recurrentes.** *En qué consiste:* las
  capacidades avanzadas de los asistentes comerciales y el acceso programático a modelos de
  gran escala se ofrecen bajo modelos de pago. *Qué efecto produce:* impone una barrera
  económica de entrada y un gasto continuo. *Cómo incide:* excluye a usuarios y
  desarrolladores que no pueden o no desean pagar, y es incompatible con la restricción de
  producto de ser íntegramente gratuito y de código abierto.

- **GPU de alta gama costosas.** *En qué consiste:* ejecutar localmente modelos grandes
  exigiría tarjetas gráficas de mucha memoria, de precio elevado. *Qué efecto produce:*
  traslada el costo del servicio en la nube a un costo de hardware. *Cómo incide:* refuerza,
  desde el lado económico, la necesidad de operar en hardware modesto.

#### Categoría Privacidad (causa principal)

- **Audio enviado a la nube y datos en servidores ajenos.** *En qué consiste:* el modelo
  dominante transmite el audio del usuario a servidores de terceros para su procesamiento.
  *Qué efecto produce:* el usuario pierde el control sobre información biométrica y sensible,
  y queda expuesto a riesgos de almacenamiento, perfilamiento y filtración. *Cómo incide:*
  es la causa que da identidad al proyecto; la evidencia de mercado muestra que cerca del
  60 % de los usuarios está preocupado por su privacidad y que el 77 % preferiría una
  alternativa con mayores garantías (Secure Data Recovery Services, 2024), lo que confirma
  que esta causa representa también una oportunidad de demanda insatisfecha.

#### Categoría Conectividad (causa secundaria, factor externo)

- **Requiere internet permanente y añade latencia de red.** *En qué consiste:* el
  procesamiento en la nube exige conexión continua y un viaje de datos por turno. *Qué
  efecto produce:* inhabilita el uso offline y degrada la experiencia bajo redes lentas.
  *Cómo incide:* limita la fiabilidad y la disponibilidad del asistente; su resolución es un
  beneficio directo del procesamiento local.

#### Categoría Idioma / Usuario (causa secundaria)

- **Competidores monolingües o anglocéntricos y *hardcodeo* de idioma.** *En qué consiste:*
  varias soluciones del estado del arte están optimizadas para un único idioma o codifican
  rígidamente reglas dependientes del idioma (por ejemplo, OS-Copilot presenta *hardcodeo*
  en chino). *Qué efecto produce:* excluye a hablantes de otros idiomas y a usuarios con
  acentos diversos. *Cómo incide:* contradice el requisito de uso universal y multilingüe, y
  evidencia un vacío que el proyecto cubre mediante clasificación por *embeddings*
  multilingües en lugar de listas de palabras clave por idioma
  (`ANALISIS_COMPETENCIA.md`, 2026).

#### Categoría Método (causa principal, factor interno)

- **El *tool-calling* es difícil en modelos pequeños.** *En qué consiste:* la selección
  correcta de la acción a ejecutar es más exigente para un modelo de 2.000 millones de
  parámetros que para uno grande. *Qué efecto produce:* una tasa de acierto menor (estimada
  ~75 % en E2B frente a ~91 % en E4B), con riesgo de elegir una acción equivocada. *Cómo incide:* es
  el principal compromiso técnico aceptado al elegir el modelo que entra en 4 GB, y motiva
  mecanismos correctivos como el reintento forzado de herramienta
  (`Gemma4_estado_y_limites_2026_05_29.md`, 2026).

- **Riesgo de "alucinar" acciones.** *En qué consiste:* un modelo puede inventar una acción
  o afirmar haberla ejecutado sin que sea cierto. *Qué efecto produce:* erosiona la
  confianza del usuario y puede provocar comportamientos incorrectos. *Cómo incide:* obliga
  a incorporar honestidad estructural —verificación del resultado por el estado real del
  sistema operativo, no por el juicio del propio modelo— como requisito de diseño.

- **Ausencia de medición frente a criterios de éxito.** *En qué consiste:* declarar logros
  sin un número contra un criterio definido de antemano. *Qué efecto produce:* lleva a dar
  por resueltos problemas que persisten. *Cómo incide:* es una causa metodológica que el
  proyecto neutraliza mediante un enfoque de desarrollo dirigido por mediciones y *gates*.

En síntesis, las **causas principales** de la problemática son las restricciones de
hardware (Tecnología/Máquina), el costo, la privacidad y la dificultad de *tool-calling*
en modelos pequeños (Método); mientras que la dependencia de la conectividad y las
limitaciones de idioma operan como **causas secundarias** que agravan el problema y
estrechan aún más la oferta disponible. La convergencia de todas ellas explica por qué, al
día de hoy, no existe una solución que satisfaga de forma conjunta los requisitos de ser
local, privada, gratuita, multilingüe y operable en hardware modesto —el vacío que el
presente proyecto llenó con la solución desarrollada.

---

# Referencias

Android Developers. (s. f.). *Gemini Nano*. Google. Recuperado el 3 de junio de 2026, de
https://developer.android.com/ai/gemini-nano

Astute Analytica. (2026, 10 de febrero). *Voice assistant market to reach US$ 59.9 billion
by 2033 driven by mass consumer adoption, enterprise voice AI, and smart device
proliferation*. GlobeNewswire.
https://www.globenewswire.com/news-release/2026/02/10/3235286/0/en/Voice-Assistant-Market-to-Reach-US-59-9-Billion-by-2033-Driven-by-Mass-Consumer-Adoption-Enterprise-Voice-AI-and-Smart-Device-Proliferation-Astute-Analytica.html

Cai, G., Tian, R., Yang, L., Jia, Y., Li, L., & Wang, J. (2026). Efficient inference for
edge large language models: A survey. *Tsinghua Science and Technology, 31*(3).
https://doi.org/10.26599/TST.2025.9010166

Secure Data Recovery Services. (2024, 5 de agosto). *Listening in: Privacy concerns of
voice assistants*. https://www.securedatarecovery.com/blog/smart-device-privacy-concerns

Zheng, Y., Chen, Y., Qian, B., Shi, X., Shu, Y., & Chen, J. (2024). *A review on edge large
language models: Design, execution, and applications* [Preprint]. arXiv.
https://arxiv.org/abs/2410.11845
