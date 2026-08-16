# CAPÍTULO I — PROBLEMA / OPORTUNIDAD Y PLANTEAMIENTO

**Portafolio de Proyectos (INSW410) — Sumativa 1**
**Proyecto:** Baxy — Asistente de voz de escritorio local y privado para Windows en hardware modesto (≤ 4 GB de VRAM) mediante un modelo Gemma 4 E2B *fine-tuneado*.
**Autor:** Emmanuel Villacura Arancibia. **Profesor guía:** Nicolás Caselli.
**Viña del Mar, Chile — 2026.**

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

El presente proyecto, denominado **Baxy**, resolvió este vacío con el
desarrollo de un asistente de voz para el sistema operativo Windows que opera
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
multilingüe y operable en hardware modesto —el vacío que el presente proyecto vino a
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
delimita la oportunidad que este proyecto atiende, y fundamenta que el problema no es un
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
NVIDIA de 4 GB (GTX 1650 / RTX 3050 4 GB) con un piso de 8 GB de RAM, y el sistema incorpora
un modo de ejecución por CPU que extiende su alcance a laptops sin gráfica dedicada. La viabilidad
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

## Referencias (APA 7)

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

*Documento de origen interno (datos reales del repositorio):*
`documentacion/00_producto/{ANALISIS_COMPETENCIA.md, Gemma4_estado_y_limites_2026_05_29.md,
AUDITORIA_licencias.md, PRODUCTION_READY.md}` y
`documentacion/datos_crudos/vram_real_medida.csv`. *Fuente de elaboración: propia (2026).*
