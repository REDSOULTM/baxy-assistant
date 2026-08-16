# Guion de Defensa Oral — Baxy

> **Examen final · Seminario de Título UNAB (INSW410) — Portafolio de Proyectos**
> **Estudiante:** Emmanuel Villacura Arancibia · **Profesor guía:** Nicolás Caselli · **UNAB, Viña del Mar, Chile, 2026**
>
> Guion detallado para una exposición de **10 minutos**, alineado con el archivo
> `PRESENTACION_FINAL.pptx` (**15 diapositivas**: 14 de contenido + 1 apéndice de
> apoyo que NO se proyecta). Para cada diapositiva se entrega: (a) el **texto exacto
> hablado** (párrafo natural, en torno a 40 segundos), (b) la **transición** hacia la
> siguiente, y (c) **indicaciones escénicas** (postura, dónde mirar, qué señalar).
> Al cierre se incluye un **banco de respuestas preparadas** a las preguntas más
> probables del jurado. Todos los números provienen de mediciones reales del proyecto
> registradas en el Informe Final y en la documentación del repositorio; ninguno es
> estimación referencial.

---

## Indicaciones generales para el expositor

- **Tono:** seguro, sereno y técnico, pero accesible. Defensa de tesis, no venta. La
  autoridad proviene de los números, no del volumen de la voz.
- **Ritmo:** ~40 segundos por diapositiva de contenido. Si el tiempo aprieta, las
  diapositivas comprimibles son la 9 (iteraciones) y la 13 (trabajos futuros); las
  intocables son la 2 (problema), la 7 (diferenciadores) y la 10 (resultados).
- **Regla de oro al hablar:** cada vez que se afirme un logro, anclarlo a un número y
  a su criterio de éxito definido de antemano. El lema del proyecto —*"medir, no
  celebrar"*— también gobierna la exposición.
- **Honestidad:** mencionar de forma proactiva el techo del *tool-calling* (estimado ≈75 %) y
  la próxima palanca de expansión de mercado (empaquetar el binario CPU/Vulkan, ya que el
  software corre en CPU). Adelantarse a la crítica desarma la objeción y proyecta rigor.
- **Demo:** tener un video o capturas de respaldo. Si la demo en vivo falla, no
  detenerse: "tengo la evidencia grabada" y continuar. Nunca improvisar reparaciones.
- **Cierre de cada bloque:** una frase puente que enlace con la diapositiva siguiente
  (se indican abajo como *Transición*).

---

## Diapositiva 1 — Portada

**[Texto hablado, ~30 s]**

> "Buenos días, profesor Caselli. Mi proyecto de título se llama **Baxy**, construido
> sobre el motor *Baxy*. Es un asistente de voz para Windows que corre **cien
> por ciento en el equipo del usuario** —sin nube, sin pagos, sin enviar un solo byte a
> servidores externos— y que cabe en una tarjeta gráfica de apenas **cuatro gigabytes
> de memoria de video**. En los próximos diez minutos quiero mostrarles el vacío de
> mercado que este proyecto ataca, cómo lo resolví y, sobre todo, los **números medidos**
> que respaldan cada decisión técnica que tomé."

**[Indicación escénica]** Postura abierta, contacto visual con el jurado. No leer la
portada palabra por palabra; está proyectada. Mantener este *slide* breve: el peso de la
exposición está en lo que sigue.

**[Transición]** "Empecemos por el problema que da origen al proyecto."

---

## Diapositiva 2 — El problema

**[Texto hablado, ~50 s]**

> "El control por voz ya es un fenómeno masivo: el mercado se valoró en torno a los
> **nueve mil ciento sesenta y tres millones de dólares en 2025** y se proyecta que
> alcance **casi sesenta mil millones hacia 2033**, con más de **ocho mil cuatrocientos
> millones de dispositivos** habilitados. Pero todo ese crecimiento descansa sobre una
> arquitectura con un costo oculto: la voz del usuario —que es un **dato biométrico**—
> viaja a servidores de terceros. Esto trae cuatro consecuencias: pérdida de privacidad,
> dependencia de internet, latencia de red y costo recurrente. Y hay una demanda
> insatisfecha clara: cerca del **60 por ciento** de los usuarios está preocupado por su
> privacidad, y el **77 por ciento** usaría más un asistente que la respetara. El vacío
> concreto que ataco es este: **no existe ninguno que sea, a la vez, local, privado,
> gratis, de código abierto, multilingüe y operable en cuatro gigabytes**."

**[Indicación escénica]** Señalar la última línea de la diapositiva (el cruce de las seis
condiciones) al pronunciar "el vacío concreto". Es la tesis del proyecto; marcarla con
una pausa.

**[Transición]** "Para no decidir esto de memoria, descompuse el problema con una técnica
formal de análisis de causa raíz."

---

## Diapositiva 3 — Diagrama de Ishikawa (causas raíz)

**[Texto hablado, ~45 s]**

> "Apliqué un **diagrama de Ishikawa** para organizar las causas en seis categorías. La
> cabeza del pescado es el problema central: *no existe un asistente de voz local, privado
> y gratuito usable en hardware modesto*. Las cuatro causas **principales** son: el
> **hardware** —los modelos buenos no entran en cuatro gigabytes, y el encoder de visión
> ya pesa por sí solo un coma dos gigabytes—; el **costo** de las APIs y las GPU de gama
> alta; la **privacidad**, con el audio saliendo del equipo; y una causa de **método**:
> el *tool-calling*, es decir, elegir la acción correcta, es más difícil en modelos
> pequeños, con una estimación comparativa de alrededor de un **75 por ciento de acierto
> frente al 91 por ciento** de un modelo grande. Lo importante es que cada espina aterriza
> en un dato real: la de
> tecnología, por ejemplo, la confirmé midiendo la VRAM de cada modelo, no asumiéndola."

**[Indicación escénica]** Recorrer con la mano las cuatro espinas principales a medida que
se nombran. Si el diagrama está como figura, apuntar a la categoría correspondiente.

**[Transición]** "Con las causas identificadas, fijé objetivos medibles para atacarlas
una por una."

---

## Diapositiva 4 — Objetivos

**[Texto hablado, ~45 s]**

> "El objetivo general lo formulé con la estructura **verbo, variable medible, unidad y
> contexto**: *desarrollar* el asistente bajo el techo verificable de cuatro gigabytes de
> VRAM, local, en Windows, con un Gemma 4 E2B fine-tuneado. De ahí bajé a seis objetivos
> específicos, todos **SMART y con criterio numérico**, no aspiracionales. En síntesis
> son: ejecutar el modelo, la visión y la voz dentro de cuatro gigabytes; lograr un
> *tool-calling* fiable que **no invente herramientas**; mantener una latencia
> *tier-Alexa*, de pocos segundos por turno; operar **cien por ciento local y privado**;
> **nunca mentir** sobre lo que el agente hizo; y habilitar **accesibilidad** por voz y
> gestos, multilingüe, sin listas de palabras codificadas por idioma. Cada uno tiene un
> número de éxito que verán en concreto dos diapositivas más adelante."

**[Indicación escénica]** Enumerar los seis con los dedos o ritmo de lista. No detenerse
en cada uno; se profundizan en la diapositiva 10.

**[Transición]** "Veamos ahora qué es exactamente la solución que construí."

---

## Diapositiva 5 — La solución: qué es Baxy

**[Texto hablado, ~50 s]**

> "Esto es **Baxy**. El usuario lo activa con una palabra; su voz se transcribe **en
> CPU**, para no robarle memoria a la tarjeta gráfica; el modelo **Gemma 4 E2B** decide si
> conversar o invocar una de más de **sesenta herramientas** —abrir aplicaciones, navegar la web,
> mensajería, volumen, brillo, control del sistema operativo, archivos—; y la respuesta se
> sintetiza de vuelta a voz, también en CPU. Además percibe el entorno: tiene visión por
> cámara y control por gestos con MediaPipe, pensado para accesibilidad. Y aquí está el
> **diferenciador clave**: Baxy **nunca dice 'listo' sin verificarlo contra el estado
> real del sistema operativo**. Lee el registro de Windows, la capa de accesibilidad UIA
> o el control de audio, y solo entonces confirma. Eso es lo que llamo *honestidad
> estructural*: el agente no alucina acciones."

**[DEMO opcional]** Si hay demo en vivo, ejecutarla aquí: por ejemplo *"subí el volumen"*
o *"abrí Spotify y poné rock"*. Si falla, decir con naturalidad: "tengo el video de
respaldo" y seguir.

**[Indicación escénica]** Al decir "honestidad estructural", bajar levemente el ritmo: es
un concepto que el jurado puede preguntar y conviene dejarlo grabado.

**[Transición]** "Para que todo eso quepa en cuatro gigabytes hizo falta una arquitectura
con un principio físico muy estricto."

---

## Diapositiva 6 — Arquitectura de alto nivel

**[Texto hablado, ~50 s]**

> "La arquitectura tiene **tres capas y un solo nodo**: el equipo del usuario, sin nube. Y
> obedece a un principio físico: la **GPU de cuatro gigabytes es sagrada**, así que ahí
> solo viven el modelo de lenguaje y la visión residente. Todo lo demás —el
> reconocimiento de voz, la síntesis de voz e incluso el render de la interfaz— corre en
> **CPU**, para no competir por esos cuatro gigabytes. La capa de voz hace
> audio, VAD, *wake-word* y transcripción. El núcleo enruta con un **enrutador semántico**
> que combina *embeddings* multilingües con un encoder fine-tuneado y una cabeza de
> abstención, y ofrece **como máximo cinco herramientas por turno**, que además es una cota
> anti-crash. Y la capa de acción ejecuta sobre el sistema operativo con una cascada
> **UIA, OCR y visión**, más verificadores tri-estado. Documenté todo esto con el modelo de
> vistas **4+1 de Kruchten** y lo verifiqué leyendo el código en una auditoría."

**[Indicación escénica]** Trazar las tres capas de arriba abajo con la mano. Enfatizar
"la GPU es sagrada": es la frase que mejor explica por qué la arquitectura es así.

**[Transición]** "Esta arquitectura es la que permite cumplir, todas juntas, las seis
restricciones que ningún competidor cumple."

---

## Diapositiva 7 — Diferenciadores / MOAT vs. competencia

**[Texto hablado, ~50 s]**

> "Hice una tabla de homologación contra **diez competidores reales**, y la conclusión es
> contundente: **ninguno cumple las seis restricciones a la vez**. Los agentes de
> *computer-use*, como Agent-S u OS-Copilot, dependen de GPT-4V o Gemini **en la nube**.
> Los frameworks de orquestación, como AutoGen o LangGraph, exigen modelos grandes. Y el
> más parecido, **Mark-XXXIX**, un asistente de voz tipo Jarvis, usa la API de Gemini en la
> nube, que es **de pago**. Baxy es el único que junta las seis condiciones: local,
> privado, gratis, en cuatro gigabytes —**3,36 gigabytes medidos**—, multilingüe por
> *embeddings* y por voz manos libres. Ese cruce de seis condiciones es el **foso
> defensivo** del proyecto: no es una característica más, es una combinación que hoy no
> existe en el mercado."

**[Indicación escénica]** Apuntar a la columna de Baxy en la tabla, la única con todos
los visto bueno. Recalcar "los diez" para subrayar que el estudio de mercado fue real y
amplio.

**[Transición]** "Ahora bien, ese resultado no salió de la intuición: salió de un método
de trabajo muy disciplinado."

---

## Diapositiva 8 — Metodología: medir, no celebrar

**[Texto hablado, ~45 s]**

> "La gestión del proyecto fue **Scrum**, y no la elegí por gusto: la decidí con una **tabla
> ponderada** contra Cascada y Kanban, donde Scrum obtuvo **2,85**, Kanban 2,50 y Cascada
> apenas 1,30; descarté Cascada por la alta incertidumbre técnica —al inicio ni siquiera
> sabía qué modelo iba a entrar en cuatro gigabytes—. El método de desarrollo es la
> columna vertebral del proyecto y se resume en tres palabras: **'medir, no celebrar'**.
> Nada se da por hecho sin un número contra un *gate* definido de antemano. Y todo cambio de
> comportamiento se valida **en vivo contra el modelo real**, no con tests mockeados,
> porque el modelo es **no-determinista** y elige caminos que un test no anticipa. Esa
> disciplina —medir primero, implementar gateado, validar en vivo, recién después
> activar— fue justamente la que me permitió destrabar los bugs más difíciles."

**[Indicación escénica]** Pronunciar "medir, no celebrar" con énfasis y una breve pausa.
Es el hilo conductor de toda la defensa y conviene que el jurado lo retenga.

**[Transición]** "Con ese método, el desarrollo se ejecutó en tres iteraciones."

---

## Diapositiva 9 — Las 3 iteraciones

**[Texto hablado, ~45 s]**

> "El trabajo se organizó en tres iteraciones, cada una cerrada contra su propio *gate*. La
> **primera** estableció el cerebro: el modelo que entra en cuatro gigabytes, el sistema de
> más de sesenta herramientas (67 esquemas únicos expuestos al modelo tras retirar `smart_home`) y un enrutador que alcanzó **0,9964 de exactitud** en un *held-out* en
> español. La **segunda** lo convirtió en asistente de voz manos libres —*wake-word*, STT
> y TTS en CPU— y le dio percepción, con computer-use y control por cámara; la voz quedó
> implementada y operativa de extremo a extremo, **cien por ciento offline** (diez de diez casos
> de voz pasaron en vivo), y el *wake-word* "Baxy" **cumple su gate**: alcanzó **0,872 de recall**
> sobre un *held-out* universal de 25 voces y 13 idiomas —por encima del umbral de 0,60—, con
> **recall perfecto, 1,00, en español, inglés, italiano, francés, polaco y ruso**. La **tercera**
> cerró la brecha del modelo pequeño con **fine-tuning**, optimizó la latencia y consolidó
> la accesibilidad y una capa de memoria de gustos a la que llamé *Jarvis*. Lo relevante,
> de nuevo, es que cada iteración cerró contra un número, no contra una sensación de 'parece
> que anda'."

**[Indicación escénica]** Tres bloques claros con la mano (uno, dos, tres). Si el reloj
apremia, esta es una de las diapositivas que se puede recortar a la mitad.

**[Transición]** "¿Y cuáles fueron los resultados? Esta es, para mí, la diapositiva más
importante."

---

## Diapositiva 10 — Resultados medidos

**[Texto hablado, ~55 s]**

> "Estos son los resultados, **todos medidos contra su criterio de éxito**. Objetivo uno,
> VRAM: el modelo ocupa **3.371 mebibytes —unos 3,36 gigabytes—**, entra holgado bajo el
> tope de cuatro. Medí **21 combinaciones** de modelo y cuantización, y ni siquiera el E4B
> en su versión más comprimida entraba; eso justificó **objetivamente** el pivote a E2B.
> Objetivo dos, *tool-calling*: el fine-tuning logró **cero por ciento de herramientas
> inventadas en producción**. Objetivo tres, latencia: bajó a unos **2,2 segundos por
> acción**, muy dentro del presupuesto *tier-Alexa*. Objetivo cuatro: **cero datos a la
> nube, cero dólares** de costo. Objetivo cinco: honestidad por verificación tri-estado.
> Y objetivo seis, accesibilidad: **diez de diez activaciones, cero falsos positivos**. La
> suite de pruebas cerró con **2.740 tests en verde y cero fallos**. Estos números son la
> respuesta directa, uno a uno, a cada objetivo específico."

**[Indicación escénica]** Esta es la diapositiva ancla. Hablar despacio, dejar que cada
número se asiente. Recorrer la tabla fila por fila. No saltarse ninguno; es el corazón de
la defensa.

**[Transición]** "Con los objetivos cumplidos, la pregunta natural es si esto es viable
más allá del laboratorio."

---

## Diapositiva 11 — Factibilidades

**[Texto hablado, ~45 s]**

> "La factibilidad **técnica está probada, no proyectada**: el sistema corre **hoy** en
> cuatro gigabytes, con un stack maduro como llama.cpp, y con *fallback* a CPU. La
> factibilidad **económica** es su mayor ventaja: **costo de operación de cero dólares**,
> porque todo es código abierto y reutiliza hardware que el usuario ya tiene. Audité **511
> paquetes**, y el stack núcleo es permisivo y vendible; el **único** obstáculo para una
> venta propietaria es una licencia GPL en el motor de TTS, Piper, con una solución conocida
> y barata: aislarlo por subproceso. Y la factibilidad **social** es el corazón del
> proyecto: **privacidad por diseño** —alineada con la ley de datos chilena, la 21.719—,
> **accesibilidad** para personas no videntes o con movilidad reducida, y
> **democratización** para quien no tiene una GPU cara."

**[Indicación escénica]** Tres factibilidades, tres gestos. Al mencionar la ley chilena,
ser preciso pero breve; el detalle legal se reserva para el Q&A.

**[Transición]** "Ser honesto también significa hablar de los riesgos que enfrenté y de
cómo los resolví."

---

## Diapositiva 12 — Riesgos clave + mitigación

**[Texto hablado, ~45 s]**

> "La gestión de riesgos la hice como un **análisis a priori** en la planificación:
> cuantifiqué los **12 riesgos** por **probabilidad e impacto** en escala uno a cinco, con su
> **exposición** —el producto de ambos— y el **riesgo residual** que busco después de mitigar.
> Los de mayor exposición no son casuales: salen de las dos **restricciones nucleares** —que
> el modelo entre en 4 GB y que el *tool-calling* sea fiable en un modelo chico— más los
> propios de un **agente que actúa sobre el sistema**, como ejecutar una acción dañina o
> afirmar que actuó sin hacerlo. Para cada uno definí una **mitigación preventiva** que
> orientó el diseño: medir la VRAM antes de elegir el modelo, fine-tuning con *gate*,
> confirmación de acciones y honestidad estructural. Y soy transparente sobre la **palanca de
> expansión**: el binario que distribuyo está optimizado para NVIDIA y el software ya corre en
> CPU; empaquetar también una versión CPU o Vulkan multiplica el mercado alcanzable, y es la
> prioridad uno de la hoja de ruta."

**[Indicación escénica]** Al decir "cada riesgo ocurrió de verdad", mirar al jurado: es un
punto de credibilidad. Mencionar la palanca de expansión sin titubear; la transparencia suma.

**[Transición]** "Y esa palanca de expansión encabeza, justamente, la hoja de ruta del producto."

---

## Diapositiva 13 — Trabajos futuros

**[Texto hablado, ~40 s]**

> "Sobre un producto que **ya está terminado y operativo**, la hoja de ruta para una versión
> 2.0 tiene una **prioridad número uno**: el binario **CPU/Vulkan**. El software **ya corre en
> modo CPU** sin NVIDIA, así que empaquetar ese binario abre el mercado de 'PC con NVIDIA de
> cuatro gigabytes' a **cualquier laptop razonable**. Después, ampliar la **evidencia
> multilingüe** —el sistema ya clasifica por *embeddings* multilingües y es multi-acento— con un
> corpus de evaluación en más idiomas, y **activar el cliente MCP**, que ya está construido y
> cableado, validando antes que un catálogo grande de herramientas no degrade al modelo de 2B. Y
> para quien tenga más de cuatro gigabytes, **conmutar al modelo E4B** subiría el *tool-calling*
> de un estimado 75 a alrededor del 91 por ciento. Todo esto está priorizado en el backlog
> maestro, verificado contra el código."

**[Indicación escénica]** Ritmo ágil; es una diapositiva de cierre técnico. Comprimible si
falta tiempo.

**[Transición]** "Permítanme cerrar con las conclusiones."

---

## Diapositiva 14 — Cierre / conclusiones

**[Texto hablado, ~45 s]**

> "Para cerrar: este proyecto demuestra que **sí es posible** un asistente de voz local,
> privado, gratis y multilingüe en una GPU de apenas cuatro gigabytes —la combinación que no
> existía—, y lo demuestra **con números, no con intuición**: los **seis objetivos se
> cumplieron** contra su criterio de éxito. La lección que me llevo es metodológica:
> **'medir, no celebrar'** me hizo encontrar causas raíz contraintuitivas —el *padding* del
> *wake-word*, el *busy-wait* de ONNX, un dataset corrupto— que un parche al síntoma habría
> escondido. Y un hallazgo de ingeniería que ordena dónde invertir esfuerzo de testing: los
> bugs reales viven en los **bordes** —recovery, teardown, persistencia—, no en el camino
> feliz. Baxy es un producto terminado y operativo: **corre hoy, en hardware modesto, sin
> enviar un solo byte a la nube**. Muchas gracias, quedo atento a sus preguntas."

**[DEMO de respaldo]** Si sobra tiempo, ejecutar aquí un segundo comando de voz o mostrar
el modo accesibilidad.

**[Indicación escénica]** Bajar el ritmo en la última frase. Cerrar con contacto visual y
una pausa antes de "muchas gracias". Proyectar calma y dominio del tema.

---

## Diapositiva 15 — Apéndice de datos defensivos (NO PROYECTAR)

Esta diapositiva **no se muestra durante la exposición**; es el apoyo del expositor para la
ronda de preguntas. Mantenerla a mano (impresa o en notas). Contiene: la tabla de VRAM por
variante, la nota de medición del *tool-calling* con el array `tools`, la precisión del
router, la palanca de latencia, el resumen de licencias, el techo honesto de computer-use y
las fuentes citables. Las respuestas del banco siguiente se apoyan en estos datos.

---

# Banco de respuestas preparadas (ronda de preguntas del jurado)

> Formato de cada respuesta: una **frase de apertura** que responde de frente, seguida del
> **dato medido** que la respalda y, si corresponde, el **matiz honesto**. Responder con
> seguridad; si no se sabe algo, decirlo y ofrecer dónde se mediría.

### P1. ¿Por qué local y no en la nube, si la nube da modelos más potentes?

> "Porque la nube viola las tres restricciones nucleares del producto, que además son la
> razón de ser del proyecto. Primero, **privacidad**: el audio es un dato biométrico y en la
> nube sale del equipo; mi propuesta de valor es que **no sale ningún byte**. Segundo,
> **costo**: la nube implica suscripción o APIs de pago, y mi objetivo era costo de
> operación **cero dólares**. Y tercero, **operación offline y sin latencia de red**. La
> evidencia de mercado lo respalda: el 77 por ciento de los usuarios preferiría una
> alternativa con mejores garantías de privacidad. Es cierto que la nube da más potencia
> bruta; el proyecto **acepta ese trade-off conscientemente** y lo compensa con fine-tuning
> y guardas estructurales, porque privacidad y costo cero no son negociables."

### P2. ¿Cómo logra que todo entre en 4 GB de VRAM?

> "Con dos decisiones, ambas respaldadas por medición. La primera es la **elección del
> modelo**: medí **21 combinaciones** de modelo y cuantización, y el Gemma 4 E2B en Q4_K_M
> ocupa **3.371 mebibytes**, mientras que el E4B en la misma cuantización pide **5.087** y no
> entra ni en su versión más comprimida, que ya da 4.057. La segunda es un **principio de
> arquitectura**: en la GPU solo viven el LLM y el encoder de visión; el reconocimiento de
> voz, la síntesis y hasta el render de la interfaz corren **en CPU**, con cuantización int8,
> para no tocar la VRAM. Así dejo margen para la visión residente, que pesa alrededor de 1,2
> gigabytes, y para la caché de atención."

### P3. ¿No se pierde demasiada precisión al usar un modelo de 2B en vez del 4B?

> "Hay una pérdida, y la declaro con transparencia: el *tool-calling* del E2B ronda, según
> una estimación comparativa, el **75 por ciento** de acierto frente al **91 por ciento** del E4B
> —es una aproximación, no una medición tan formal como la de la VRAM—. Pero hago dos distinciones
> importantes. Una: lo que **nunca** quise tolerar era que el modelo **inventara**
> herramientas o **mintiera** sobre lo que hizo; y eso sí lo resolví, con un gate medido de
> **cero por ciento de herramientas inventadas en producción** tras el fine-tuning. Dos: el
> 75 por ciento no es el techo de la experiencia, porque en producción hay un mecanismo de
> **reintento forzado de herramienta** y un router fine-tuneado que recuperan muchos de esos
> casos. Y para quien tenga más de 4 GB, dejé lista la ruta de conmutar a E4B y recuperar
> ese 91 por ciento. Es un trade-off elegido, medido y mitigado, no un defecto oculto."

### P4. ¿Es vendible? ¿Hay un modelo de negocio?

> "Técnicamente es vendible, con una salvedad que ya tengo resuelta en el papel. Audité
> **511 paquetes** de licencias y el stack núcleo —Gemma 4, Whisper, MediaPipe, OpenCV,
> llama.cpp, Tesseract, sentence-transformers— es **permisivo**: Apache o MIT. El **único**
> bloqueante para una venta propietaria es el motor de TTS, **Piper, que es GPL**; la
> solución es conocida y barata: **aislarlo por subproceso** —que es agregación, no obra
> derivada— o reemplazarlo por un TTS Apache o MIT como Kokoro. Sobre el negocio: la ventaja
> competitiva es el **costo de operación cero** y la privacidad por diseño, que abren
> mercados sensibles —salud, legal, sector público, accesibilidad—. La principal **palanca de
> expansión de mercado**, más que la legal, es empaquetar el binario CPU/Vulkan para llegar a
> equipos sin NVIDIA; eso está priorizado como número uno de la hoja de ruta."

### P5. ¿Qué tan honesto es el agente? ¿No puede igual alucinar acciones?

> "La honestidad es un requisito de diseño, no una esperanza. El agente **no afirma haber
> hecho algo si no puede verificarlo contra el estado real del sistema operativo**: lee el
> registro de Windows, la capa de accesibilidad UIA o el control de audio con pycaw, según
> la acción. La clave es que uso una **verificación de tres estados** —verdadero, falso y
> 'no verificable'—, y crucialmente **'no verificable' nunca se confunde con éxito**. Un
> ejemplo real: cuando intenté instalar DOOM por Steam y no había espacio en disco, el
> agente **detectó el botón deshabilitado y reportó el fallo en vez de mentir**. Es decir,
> la honestidad no la juzga el propio modelo —que sí podría alucinar—, la juzga el **estado
> objetivo del sistema operativo**."

### P6. ¿Cuál es el aporte de ingeniería del proyecto? ¿No es solo integrar librerías existentes?

> "El aporte no es entrenar un modelo nuevo; es de **ingeniería de sistemas** y se sostiene
> en cuatro pilares. Primero, hacer **caber** todo el pipeline cognitivo en 4 GB con un
> reparto medido CPU/GPU —eso es ingeniería de recursos, no integración trivial—. Segundo, la
> **honestidad estructural tri-estado**, que ataca un problema abierto de los agentes LLM: la
> alucinación de acciones. Tercero, el **enrutador semántico** multilingüe por embeddings con
> encoder fine-tuneado, abstain head y un tope de cinco herramientas que es a la vez precisión
> y cota anti-crash, alcanzando **0,9964** en español. Y cuarto, una **disciplina metodológica
> medible** que destrabó causas raíz que ninguna librería resuelve sola: el padding invertido
> del wake-word que daba scores de 0,002, el busy-wait de ONNX que congelaba Windows, un
> dataset con 393 ejemplos mal serializados. El sistema tiene más de 60.000 líneas de código
> de producción; integrar librerías es lo fácil, hacerlas **convivir en 4 GB sin mentir y sin
> caer** es el aporte."

### P7. ¿Por qué Scrum y no otra metodología? ¿No es sobreingeniería para un solo desarrollador?

> "Elegí Scrum por una **tabla ponderada**, no por moda: obtuvo 2,85 frente a 2,50 de Kanban
> y 1,30 de Cascada. El factor decisivo fue la **alta incertidumbre técnica**: al empezar no
> sabía qué modelo entraría en 4 GB ni qué tasa de tool-calling era alcanzable, y comprometer
> un plan cerrado tipo Cascada habría sido inviable. Para un solo desarrollador no apliqué la
> ceremonia completa de Scrum —no hay *daily* con un equipo de una persona—, sino su núcleo:
> **iteraciones cortas cerradas contra un gate medido**. Y eso no es teoría: el historial del
> repositorio está lleno de sprints reales, como el Sprint 7 del wake-word o los sprints R1 a
> R8 de refactorización arquitectónica."

### P8. ¿Cómo garantiza la latencia "tier-Alexa"? ¿Qué pasa con casos lentos?

> "El presupuesto era de 4 a 5 segundos por turno, y más de 8 sería catastrófico. Hoy mido
> **alrededor de 2,2 segundos por acción**, con un p50 global aún menor. La palanca de
> optimización principal que encontré —y la encontré **midiendo**— fue el **cache-hit del
> *summary-pass***: al preservar las herramientas y alinear la bandera de *thinking* entre
> pasadas, el prefijo del prompt se cachea. ¿Casos lentos? Existieron: la cola de Spotify
> llegaba a **12,6 segundos** por una tormenta de reintentos; diagnostiqué la causa raíz —un
> verificador con falso negativo que disparaba reintentos en cadena— y al corregirla y poner
> un tope al decode, esa cola bajó a **5,6 segundos**, ya por debajo del umbral catastrófico.
> El punto metodológico es que no 'optimicé a ciegas': medí, hallé la causa y la ataqué."

### P9. ¿Y la parte multilingüe? ¿Realmente funciona en otros idiomas o está optimizado al español?

> "Multilingüe es un requisito de producto, no un extra, y lo resolví por **arquitectura**:
> la clasificación de intención se hace por **embeddings multilingües**, no por listas de
> palabras clave codificadas por idioma —que es justamente el error de competidores como
> OS-Copilot, que tiene *hardcodeo* en chino—. Y hay evidencia multilingüe **medida**: el
> *wake-word* "Baxy" alcanzó **0,872 de recall** sobre un *held-out* de 25 voces y 13 idiomas,
> por encima del umbral de 0,60, con **recall perfecto en español, inglés, italiano, francés,
> polaco y ruso**. El mecanismo de intención es multilingüe por diseño y multi-acento, y está
> **implementado y operativo**; el corpus de evaluación del router es hoy mayoritariamente en
> español, porque ahí tengo los logs reales, y la hoja de ruta amplía ese corpus a más idiomas
> para respaldar el routing con números en cada uno. Hago la distinción entre *el diseño es
> multilingüe y operativo* —verificable en el código y medido ya en el wake-word— y *ampliar la
> medición del router a más idiomas*, que es la siguiente iteración de evidencia, no una carencia."

### P10. ¿Probaste esto de verdad o solo con tests automáticos?

> "Ambas cosas, y la distinción es deliberada. Tengo una suite de **2.740 tests en verde**,
> pero **los tests mockeados no bastan**, y eso lo aprendí por un fallo concreto: arreglé una
> intención que pasaba en los tests, pero en producción el modelo —que es **no-determinista**—
> eligió otra herramienta y el fix no se activaba. Desde entonces, una regla del proyecto es
> que **todo cambio de comportamiento se valida en vivo contra el modelo y el agente reales**,
> con varias variantes de fraseo, antes de darlo por cerrado. Por ejemplo, re-ejecuté los
> **1.071 mensajes reales** únicos del usuario contra el agente y medí p50 de 2,5 segundos,
> cero errores y cero fugas de herramientas. Lo único que dejo para verificación física real
> son las acciones irreversibles que no puedo simular sin riesgo, y eso lo digo
> explícitamente."

### P11. ¿Por qué desactivaste flash-attention si eso hace el prefill más lento?

> "Porque medí que con flash-attention activa, los prompts mayores a unos 10.000 tokens
> disparaban el **crash de CUDA número 22527** —un acceso ilegal a memoria que **tiraba el
> servidor del LLM**—. Con flash-attention desactivada, el sistema se midió **estable en 37
> de 37 turnos**. El costo es un prefill aproximadamente dos veces más lento, pero lo
> **mitigué** por otras vías: reduje el system prompt de 9,4 K a 5 K tokens y limité las
> herramientas ofrecidas a cinco. Entre **un prefill más lento pero mitigado** y **un servidor
> que se cae**, la elección de ingeniería es clara: priorizo la fiabilidad, que es el segundo
> norte de la arquitectura después de preservar invariantes."

### P12. ¿Qué pasa si el usuario no tiene una GPU NVIDIA?

> "A nivel de **software ya funciona**: el gestor del servidor detecta la situación y cae al
> perfil **CPU**, con cero VRAM, y el STT y el TTS siempre fueron en CPU de todas formas. La
> hoja de ruta suma **empaquetar también el binario CPU o Vulkan** de llama-server, porque el
> que distribuyo hoy está optimizado para CUDA. Es trabajo de **empaquetado, no de diseño**, y
> por eso lo declaro como la **principal palanca de expansión de mercado** y la prioridad número
> uno de la versión 2.0. Con
> Vulkan, además, aprovecharía gráficas integradas Intel y AMD. El caveat honesto es el
> rendimiento: en CPU estimo entre 12 y 20 tokens por segundo —cómodo para respuestas cortas,
> lento para textos largos— y sin visión, porque la visión es demasiado costosa en CPU."

### P13. ¿No es un riesgo de seguridad o privacidad que el agente controle todo el sistema operativo?

> "Es una preocupación legítima, y la respondo en dos planos. En **privacidad**, el agente
> controla el SO **localmente**: nada de lo que ve o hace sale del equipo, que es exactamente
> lo contrario del modelo en la nube. En **seguridad de la acción**, hay **guardas
> estructurales**: las acciones de riesgo —por ejemplo, enviar un mensaje— exigen
> **confirmación** y resuelven el destinatario por *deeplink* verificado, no por una búsqueda
> ciega en la interfaz. Esto nació de un incidente real que documenté: una versión temprana
> podía abrir un chat con un **destinatario equivocado**; lo corregí con resolución por
> teléfono o *deeplink*, verificación por la región del encabezado y política de confirmación.
> Así, el control total del SO convive con frenos estructurales para las acciones
> irreversibles."

### P14. Si tuvieras más tiempo o recursos, ¿qué es lo primero que mejorarías?

> "El **binario CPU/Vulkan**, sin dudarlo, porque convierte el proyecto de 'corre en PC con
> NVIDIA de 4 GB' a 'corre en cualquier laptop razonable', y eso multiplica el alcance social
> y de mercado de un solo golpe. En segundo lugar, **ampliar la evidencia multilingüe del
> router**: el diseño ya es multilingüe y operativo —y el wake-word ya lo demostró con números
> en 13 idiomas—, así que construiría un corpus de evaluación del router en varios idiomas para
> respaldar también ese subsistema con **números** en cada uno. Y como tercer eje, **activar
> el cliente MCP** que ya está construido, midiendo antes que un catálogo grande de
> herramientas no degrade al modelo de 2B. Las tres están priorizadas en el backlog maestro,
> verificadas contra el código."

---

## Apéndice — Datos defensivos rápidos (cábala del expositor)

> Tabla de consulta veloz para responder con cifras exactas bajo presión.

- **VRAM por variante (medida):** E2B-Q4_K_M **3.371 MiB** ✓ · E2B-Q5_K_M 3.609 ✓ ·
  E4B-UD-IQ2_M 4.057 ✗ · E4B-Q4_K_M **5.087** ✗ · 26B-UD-IQ2_XXS 12.613 ✗. Modelo
  fine-tuneado: **2,07 GB de VRAM efectiva**, 3,2 GB en disco.
- **Tool-calling:** **0 %** inventadas en producción (gate aprobado); accuracy estimada ≈ **75 %**
  (E2B) vs ≈ **91 %** (E4B) — estimación comparativa, no medición formal. **Medir SIEMPRE con el array `tools`**; sin él el arnés reporta
  un falso 47 % de inventadas (artefacto de medición, no del modelo).
- **Router:** held-out español **0,9964**; norte = precisión, no recall (ya saturado); tope
  **5 herramientas/turno** (anti-crash).
- **Latencia:** ≈ **2,2 s/acción**; p50 global ≈ 1,22 s; palanca real = cache-hit del
  *summary-pass* (keep-tools); cola Spotify reducida de **12,6 → 5,6 s**.
- **Pruebas:** **2.740 tests verdes, 0 fallos** al cierre (rama Dev, 2026-06-02). *Replay*
  de **1.071 mensajes reales**: p50 2,5 s, 0 errores, 0 fugas.
- **Estabilidad:** crash CUDA #22527 eliminado con flash-attn OFF (**37/37 estables**).
- **Licencias:** **511 paquetes** auditados; núcleo permisivo (Apache/MIT); único GPL =
  **piper-tts** (resoluble por subprocess).
- **Accesibilidad:** **10/10** activaciones, **0 falsos positivos**; gestos: palma
  0,928 → 0,992, falsos clics 14 → 0.
- **Techo honesto computer-use:** SOTA WindowsAgentArena ≈ **52,5 %**; UFO2+GPT-4o ≈ **28 %**
  (el desempeño físico no se promete perfecto).
- **Legal (Chile):** Ley 21.719 (datos personales), 19.628 (vida privada), 21.459 (delitos
  informáticos), 21.663 (ciberseguridad), 17.336 (propiedad intelectual / uso de IA).
- **Fuentes citables:** Astute Analytica (2026); Secure Data Recovery (2024); Zheng et al.
  (2024); Cai et al. (2026); Schwaber & Sutherland (2020); Kruchten (1995); Park et al. (2023).

---

*Fuente: Elaboración propia (2026), sobre datos medidos en `INFORME_FINAL_COMPLETO.md`,
`05_presentacion_oral.md`, `documentacion/00_producto/{PRODUCTION_READY, ANALISIS_COMPETENCIA,
AUDITORIA_licencias, Gemma4_estado_y_limites}.md`, `documentacion/datos_crudos/vram_real_medida.csv`
y `documentacion/_backlog/BACKLOG_MAESTRO.md`. Alineado con `PRESENTACION_FINAL.pptx` (15 slides).*
