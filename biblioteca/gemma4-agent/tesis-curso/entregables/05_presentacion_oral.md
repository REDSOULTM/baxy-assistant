# Presentación Oral Final — Baxy

> **Examen / Clase 7 — Seminario de Título UNAB (INSW410)**
> **Estudiante:** Emmanuel Villacura Arancibia · **Profesor guía:** Nicolás Caselli · **UNAB, 2026**
>
> Exposición de **10 minutos**. 14 slides · ~45 s c/u. Cada slide trae *bullets* para
> proyectar y una **nota del orador** (qué decir, defendible con datos reales del
> Informe Final). Marcas `[DEMO]` / `[CAPTURA]` señalan dónde mostrar evidencia en vivo.
> Todos los números provienen de mediciones reales del proyecto, no de estimaciones.

---

## Slide 1 — Portada

**Título:**
ASISTENTE DE VOZ DE ESCRITORIO LOCAL Y PRIVADO PARA WINDOWS EN HARDWARE MODESTO (≤ 4 GB DE VRAM) MEDIANTE UN MODELO GEMMA 4 E2B FINE-TUNEADO

- **Autor:** Emmanuel Villacura Arancibia
- **Profesor guía:** Nicolás Caselli
- Asignatura: INSW410 — Portafolio de Proyectos
- Universidad Andrés Bello · Viña del Mar, Chile · 2026
- Nombre del producto: **Baxy** (motor *Baxy*)

> **Nota del orador (~30 s):** "Buenos días, profesor. Mi proyecto de título es Baxy,
> un asistente de voz para Windows que corre 100% en el equipo del usuario —sin nube,
> sin pagos— en una tarjeta gráfica de apenas 4 GB. Hoy quiero mostrarles el vacío de
> mercado que ataca, cómo lo resolví y, sobre todo, los números medidos que respaldan
> cada decisión." Mantener la portada breve; el peso está en lo que sigue.

---

## Slide 2 — El problema

**Los asistentes de voz dominantes son cloud, invasivos y pagos.**

- Mercado masivo: ~USD 9.163 M en 2025 → USD 59.900 M en 2033 (CAGR ≈ 26,8 %); +8.400 M de dispositivos (Astute Analytica, 2026).
- Alexa, Siri, Google Assistant: el audio **sale del equipo** hacia servidores remotos.
- Consecuencias: pérdida de privacidad, dependencia de internet, latencia de red, costo recurrente.
- Demanda insatisfecha: 77 % usaría más un asistente con mejores garantías de privacidad (Secure Data Recovery, 2024).
- **El vacío:** no existe uno que sea, a la vez, local + privado + gratis + OSS + multilingüe + que corra en 4 GB.

> **Nota del orador (~50 s):** "El control por voz ya es masivo, pero descansa en una
> arquitectura con un costo oculto: tu voz —dato biométrico— viaja a servidores de
> terceros. Casi el 60 % de los usuarios está preocupado por su privacidad y el 77 %
> preferiría una alternativa que la respete. Revisé diez soluciones del estado del arte
> y ninguna cumple, a la vez, ser local, privada, gratis, de código abierto, multilingüe
> y operable en 4 GB de VRAM. Ese cruce es el vacío concreto que ataco." Si preguntan
> por las fuentes: Astute Analytica 2026 y Secure Data Recovery 2024, ambas en el informe.

---

## Slide 3 — Diagrama de Ishikawa (causas raíz)

**Problema central:** *"No existe un asistente de voz local, privado y gratuito usable en hardware modesto (≤ 4 GB de VRAM)."*

- **Tecnología/Máquina:** LLMs grandes no caben en 4 GB; el encoder de visión (mmproj) pesa ~1,2 GB.
- **Costo:** APIs cloud de pago, GPUs de alta gama caras, suscripciones.
- **Privacidad:** audio enviado a la nube, datos en servidores ajenos.
- **Conectividad:** exige internet permanente; latencia de red por turno.
- **Idioma/Usuario:** competidores monolingües o anglocéntricos, *hardcodeo* de idioma.
- **Método:** *tool-calling* difícil en modelos chicos (estimado ~75 % vs ~91 %); riesgo de "alucinar" acciones.

> [CAPTURA: insertar el diagrama de espina de pescado del Informe Final, Tabla 1.]
>
> **Nota del orador (~45 s):** "Para no decidir de memoria, apliqué un Ishikawa y
> organicé las causas en seis categorías. Las cuatro principales son: el hardware —los
> modelos buenos no entran en 4 GB—, el costo, la privacidad y un problema de método: el
> tool-calling es más difícil en modelos pequeños. Cada espina aterriza en un dato real;
> la de tecnología, por ejemplo, la confirmé midiendo la VRAM de cada modelo."

---

## Slide 4 — Objetivos

**Objetivo general**
Desarrollar un asistente de voz de escritorio para Windows, íntegramente local y privado, en ≤ 4 GB de VRAM, con un Gemma 4 E2B fine-tuneado (GGUF Q4_K_M, llama.cpp), sin nube ni costos de suscripción.

**Objetivos específicos (SMART)**
1. **OE1 —** Ejecutar LLM + visión + voz dentro de **4 GB de VRAM**.
2. **OE2 —** Lograr **tool-calling fiable** en un modelo de 2B (no inventar herramientas).
3. **OE3 —** Mantener **latencia tier-Alexa** por turno de voz.
4. **OE4 —** Garantizar operación **100 % local y privada** (0 nube, USD 0).
5. **OE5 —** Asegurar **honestidad estructural** (no reportar acciones no ejecutadas).
6. **OE6 —** Habilitar **accesibilidad** por voz/gestos, multilingüe sin listas por idioma.

> **Nota del orador (~45 s):** "El objetivo general se formula como verbo + variable
> medible + unidad + contexto: desarrollar el asistente bajo el techo verificable de 4 GB.
> Lo bajé a seis objetivos específicos SMART, cada uno con un criterio de éxito numérico que
> verán en dos slides. Lo importante: ninguno es aspiracional, todos son medibles —operar
> en 4 GB, no inventar herramientas, responder en pocos segundos, no enviar nada a la nube,
> nunca mentir sobre lo que hizo y ser accesible y multilingüe."

---

## Slide 5 — La solución: qué es Baxy

**Un asistente de voz local que controla tu PC por la palabra.**

- Pipeline de voz extremo a extremo: *wake-word* → STT → **Gemma 4 E2B-FT** → ~67 herramientas → TTS.
- ~67 *tools* (67 esquemas únicos expuestos al modelo tras retirar `smart_home`): abrir apps, navegar web, mensajería, volumen/brillo, *computer-use*, archivos.
- Percepción: visión por cámara y gestos (MediaPipe) para accesibilidad.
- **Honestidad estructural:** verifica contra el estado real del SO (registry/UIA/pycaw), no por juicio del LLM.
- Todo OSS y gratis. Cero datos a la nube.

> [DEMO: comando de voz en vivo — p. ej. "subí el volumen" o "abrí Spotify y poné rock".]
>
> **Nota del orador (~50 s):** "Esto es Baxy. El usuario activa con una palabra, su voz
> se transcribe en CPU para no robar VRAM, Gemma 4 decide si conversar o invocar una de
> más de sesenta herramientas, y la respuesta se sintetiza también en CPU. Un diferenciador clave:
> Baxy nunca dice 'listo' sin verificarlo contra el estado real del sistema operativo
> —lee el registro de Windows, la accesibilidad UIA o el control de audio—, así no alucina
> acciones." Si la demo en vivo falla, tener una captura/video de respaldo.

---

## Slide 6 — Arquitectura de alto nivel

**Tres capas, un solo nodo: el equipo del usuario (sin nube).**

- **Voz (CPU):** audio → VAD → wake (LiveKit ONNX) → STT (Whisper/Parakeet int8) → TTS (Piper).
- **Núcleo + Router:** `agent.run_content` → enrutador semántico (MiniLM + encoder FT + abstain head, tope 5 tools).
- **Acción:** ~67 *tools* (67 esquemas únicos expuestos tras retirar `smart_home`) + *computer-use* en cascada **UIA → OCR → visión** + verificadores tri-estado.
- **GPU 4 GB:** solo el LLM Gemma 4 + visión residente. **STT/TTS/UI render en CPU.**
- Modelo de vistas **4+1 (Kruchten)** documentado y verificado leyendo el código.

> [CAPTURA: diagrama de contexto / vista lógica del Informe Final, Figura 6.1.]
>
> **Nota del orador (~50 s):** "La arquitectura tiene tres capas y un principio físico:
> la GPU de 4 GB es sagrada, así que solo vive ahí el LLM y la visión. Todo lo demás
> —reconocimiento de voz, síntesis y hasta el render de la interfaz— corre en CPU para no
> competir por esos 4 GB. El enrutador combina embeddings multilingües con un encoder
> fine-tuneado, y ofrece como máximo cinco herramientas por turno, que además es una cota
> anti-crash. La documenté con el modelo 4+1 y la verifiqué en una auditoría de código."

---

## Slide 7 — Diferenciadores / MOAT vs. competencia

**Ninguno de los 10 competidores cumple las 6 restricciones a la vez.**

| Característica | Alexa/Siri | Agent-S / OS-Copilot | Mark-XXXIX (el más parecido) | **Baxy** |
|---|---|---|---|---|
| Local (sin nube) | ✗ | ✗ (GPT-4V/Gemini) | ✗ (Gemini LiveAPI) | **✓** |
| Privado (audio no sale) | ✗ | ✗ | ✗ | **✓** |
| Gratis / OSS | ✗ | parcial | ✗ (pago) | **✓** |
| Corre en 4 GB VRAM | n/a | ✗ | ✗ | **✓ (3,36 GB medido)** |
| Multilingüe (sin hardcode) | ✓ | ✗ (chino hardcoded) | parcial | **✓ (embeddings)** |
| Voz manos-libres | ✓ | ✗ | ✓ | **✓** |

> **Nota del orador (~50 s):** "Hice una tabla de homologación con diez competidores
> reales. Los agentes de computer-use como Agent-S u OS-Copilot dependen de GPT-4V o
> Gemini en la nube. Los frameworks como AutoGen o LangGraph exigen modelos grandes. Y el
> más parecido, Mark-XXXIX, un asistente de voz tipo Jarvis, usa la API de Gemini en la
> nube, que es de pago. Baxy es el único que junta las seis condiciones: local, privado,
> gratis, en 4 GB, multilingüe y por voz. Ese es el foso defensivo del proyecto."

---

## Slide 8 — Metodología: medir, no celebrar

**Scrum/iterativo, dirigido por gates medidos.**

- Selección de metodología por **tabla ponderada** (no por opinión): Scrum 2,85 > Kanban 2,50 > Cascada 1,30.
- Regla de oro (`CLAUDE.md`): ninguna decisión vale sin **un número contra un criterio definido de antemano**.
- Ciclo por feature: **medir → implementar gateado (flag off) → validar EN VIVO → activar**.
- Control de calidad: suite de tests verde + evals batch reproducibles + Backlog Maestro verificado contra código.

> **Nota del orador (~45 s):** "La gestión fue Scrum, elegida por una tabla ponderada
> contra Cascada y Kanban, no por gusto; y el repositorio lo confirma: está lleno de
> sprints reales. El método de desarrollo es la columna vertebral del proyecto: 'medir,
> no celebrar'. Nada se da por hecho sin un número contra un gate, todo se valida en vivo
> contra el modelo real —no con tests mockeados— porque el modelo es no-determinista y
> elige caminos que un test no anticipa. Esa disciplina fue la que destrabó los bugs más
> difíciles."

---

## Slide 9 — Las 3 iteraciones

**Desarrollo incremental, cada iteración cerrada contra su gate.**

- **Iteración 1 — Núcleo:** LLM local en 4 GB + ~67 tools (67 esquemas únicos expuestos tras retirar `smart_home`) + router. Selección de modelo por VRAM medida; router 0,9964 (held-out ES).
- **Iteración 2 — Voz y percepción:** wake-word + STT/TTS en CPU + computer-use (cascada UIA→OCR→visión) + cámara/gestos.
- **Iteración 3 — Fine-tuning + optimización + accesibilidad:** FT de E2B (0 % tools inventadas), latencia ~2,2 s, capa de memoria "Jarvis", fallback CPU.

> **Nota del orador (~45 s):** "El trabajo se ejecutó en tres iteraciones. La primera
> estableció el cerebro: el modelo que entra en 4 GB y el sistema de herramientas, con un
> enrutador al 99,6 %. La segunda lo convirtió en asistente de voz manos-libres y le dio
> percepción. La tercera cerró la brecha del modelo pequeño con fine-tuning, optimizó la
> latencia y consolidó la accesibilidad y la memoria. Cada una cerró contra un gate medido,
> no contra una sensación de 'parece que anda'. La voz quedó implementada y operativa
> extremo a extremo, 100 % offline —10/10 casos de voz pasaron en vivo—, y la aceptación
> cuantitativa formal del wake-word también se cumplió: 'Baxy' alcanzó recall 0,872 sobre un
> held-out universal de 25 voces y 13 idiomas, por encima del umbral de 0,60 exigido, con
> recall perfecto (1,00) en español, inglés, italiano, francés, polaco y ruso. El gate de
> voz quedó cerrado y verificado."

---

## Slide 10 — Resultados medidos

**Todos los objetivos cumplidos, con números reales.**

| Objetivo | Criterio | Resultado medido |
|---|---|---|
| OE1 — VRAM | ≤ 4.096 MiB | **3.371 MiB (≈3,36 GB)** con E2B-Q4_K_M ✓ |
| OE2 — Tool-calling | 0 % inventadas | **0 % en producción**; ~75 % accuracy (estimado) ✓ |
| OE3 — Latencia | ≤ 4–5 s | **~2,2 s/acción** (p50 global ~1,22 s) ✓ |
| OE4 — Local/gratis | 0 nube, USD 0 | **0 transmisión externa, USD 0** ✓ |
| OE5 — Honestidad | no mentir | **verificación tri-estado por estado del SO** ✓ |
| OE6 — Accesibilidad | activación ≥ 90 %, 0 FP | **10/10, 0 falsos positivos** ✓ |

- Dato decisivo: ni la cuantización más baja de E4B (4.057 MiB) entra en 4 GB → pivote a **E2B**.
- Suite de pruebas: **2.740 tests verdes, 0 fallidos** al cierre.

> **Nota del orador (~55 s):** "Estos son los resultados, todos medidos. El modelo ocupa
> 3.371 MiB —entra holgado en 4 GB—; medí 21 combinaciones de modelo y cuantización y ni
> el E4B en su versión más comprimida entraba, lo que justificó objetivamente el pivote a
> E2B. El fine-tuning logró cero herramientas inventadas en producción. La latencia bajó a
> unos 2,2 segundos por acción, muy dentro del presupuesto tier-Alexa. Cero datos a la nube,
> cero costo de operación. Y la suite cerró con 2.740 pruebas en verde, cero fallas. Estos
> números son la respuesta a cada objetivo específico."

---

## Slide 11 — Factibilidades

**Técnica probada · Económica USD 0 · Social: privacidad + accesibilidad.**

- **Técnica (probada, no proyectada):** corre HOY en 4 GB (3,36 GB medido); stack maduro (llama.cpp); fallback CPU.
- **Económica:** costo de operación **USD 0** (sin APIs, sin suscripción, hardware ya existente). Stack permisivo y vendible; único bloqueante = Piper GPL, resoluble por subprocess.
- **Social:** privacidad (datos no salen del equipo, alineado a leyes chilenas 21.719/19.628/21.459/21.663); accesibilidad (modos `no_vidente` y `movilidad`, control por voz/gestos); democratización (hardware modesto, multilingüe).

> **Nota del orador (~45 s):** "La factibilidad técnica está probada, no proyectada:
> el sistema corre hoy en 4 GB. La económica es su mayor ventaja: cero costo de operación,
> porque todo es de código abierto y reutiliza hardware que el usuario ya tiene; el único
> obstáculo para venderlo es una licencia GPL en el TTS, con solución conocida y barata.
> Y la social es el corazón del proyecto: privacidad por diseño —alineada con la ley de
> datos chilena—, accesibilidad para personas no videntes o con movilidad reducida, y
> democratización para quien no tiene una GPU cara."

---

## Slide 12 — Riesgos clave + mitigación

**Matriz de 12 riesgos evaluada a priori (probabilidad × impacto, escala 1–5).**

- **El modelo capaz no entra en 4 GB (E = 25, crítico):** plan B medido — modelo más chico (E2B) tras barrido de VRAM. → residual 6.
- **Tool-calling débil del 2B (E = 20):** fine-tuning con *gate* de 0 % inventadas + router (*abstain head*) + reintento forzado. → residual 8.
- **Acciones dañinas/no deseadas del agente (E = 20):** confirmación obligatoria + honestidad estructural (verificar antes de declarar). → residual 8.
- **Latencia de voz / inestabilidad del stack / alucinar acciones (E = 16):** presupuesto de latencia, STT/TTS en CPU int8, defaults conservadores, verificación tri-estado. → residual 6–8.
- **Alcance vs. tiempo de un estudiante (E = 16):** desarrollo iterativo (Scrum), MVP temprano, incrementos gateados.
- **"Celebrar sin medir" (E = 16):** regla *medir, no celebrar* — *gate* numérico por objetivo + validación en vivo.

> **Nota del orador (~45 s):** "La gestión de riesgos la hice como corresponde: un análisis
> a priori en la planificación, cuantificando cada riesgo por probabilidad e impacto en
> escala uno a cinco, con su exposición y el riesgo residual que busco tras mitigar. Los de
> mayor exposición salen de las dos restricciones nucleares —que el modelo entre en 4 GB y
> que el tool-calling sea fiable en un modelo chico— más los propios de un agente que actúa
> sobre el sistema. Para cada uno definí una mitigación preventiva que orientó las decisiones
> de diseño; varios de estos riesgos previstos efectivamente se materializaron durante el
> desarrollo y fueron contenidos con la estrategia planificada."

---

## Slide 13 — Trabajos futuros

**Versión 2.0: lo que expande el mercado y lo que profundiza la calidad.**

- **Soporte sin GPU NVIDIA (binario CPU/Vulkan):** ampliar el alcance de "PC con NVIDIA 4 GB" a "cualquier laptop razonable".
- **Profundizar la cobertura multilingüe:** sobre la base multi-idioma ya operativa, ampliar el corpus de evaluación y la calibración por idioma para certificar más lenguas con medición.
- **Cliente MCP:** ya construido y cableado; validar que un catálogo grande de tools no degrade el 2B antes de activarlo.
- **Migrar a E4B con > 4 GB:** elevaría el tool-calling de un estimado ~75 % a ~91 % en mejor hardware.

> **Nota del orador (~40 s):** "Para una versión 2.0, la prioridad uno es el binario
> CPU/Vulkan: el software ya opera en modo CPU sin NVIDIA, y empaquetarlo en esa modalidad
> abre el mercado a cualquier laptop. Después, profundizar el frente multilingüe ya operativo y activar
> el cliente MCP que ya está construido pero gateado. Y para quien tenga más de 4 GB,
> conmutar al modelo E4B subiría el tool-calling a un estimado ~91 %. Todo está priorizado en el
> backlog maestro, verificado contra el código."

---

## Slide 14 — Cierre / conclusiones

**Baxy llena un vacío real, medido y defendible.**

- Se construyó un asistente de voz **local, privado, gratuito y multilingüe en 4 GB** — la combinación que no existía.
- **6/6 objetivos cumplidos con números:** 3,36 GB, 0 % tools inventadas, ~2,2 s, USD 0, honestidad tri-estado, accesibilidad 10/10.
- Lección central: **"medir, no celebrar"** destrabó causas raíz contraintuitivas (padding del wake-word, busy-wait de ONNX, dataset corrupto).
- Los bugs reales viven en los **bordes** (recovery, teardown, persistencia), no en el camino feliz.

> [DEMO de respaldo: si queda tiempo, segundo comando de voz o el modo accesibilidad.]
>
> **Nota del orador (~45 s):** "Para cerrar: el proyecto demuestra que sí es posible un
> asistente de voz local, privado, gratis y multilingüe en una GPU de 4 GB, y lo demuestra
> con números, no con intuición —los seis objetivos se cumplieron contra su criterio de
> éxito. La lección que me llevo es metodológica: 'medir, no celebrar' me hizo encontrar
> causas raíz que un parche al síntoma habría escondido. Baxy es un producto terminado y
> funcional: corre hoy, en hardware modesto, sin enviar un solo byte a la nube. Gracias,
> quedo atento a sus preguntas."

---

### Apéndice — Datos defensivos para preguntas (no proyectar)

- **VRAM por variante (vram_real_medida.csv):** E2B-Q4_K_M 3.371 MiB ✓ · E2B-Q5_K_M 3.609 ✓ · E4B-UD-IQ2_M 4.057 ✗ · E4B-Q4_K_M 5.087 ✗ · 26B-UD-IQ2_XXS 12.613 ✗.
- **Tool-calling:** medir SIEMPRE con el array `tools`; sin él, el arnés reporta 47 % de "inventadas" — artefacto de medición, no del modelo.
- **Router:** held-out ES 0,9964; norte = precisión (ofrecer solo lo necesario), no recall (saturado).
- **Latencia:** palanca real = cache-hit del summary-pass (keep-tools); cola de Spotify 12,6 → 5,6 s.
- **Stack/licencias:** Gemma 4, Whisper, MediaPipe, OpenCV, llama.cpp, Tesseract, sentence-transformers = permisivos; único GPL = piper-tts (resoluble por subprocess).
- **Techo honesto computer-use:** SOTA WindowsAgentArena ≈ 52,5 %; UFO2+GPT-4o ≈ 28 % — el desempeño físico no se promete perfecto.
- **Fuentes citables:** Astute Analytica (2026); Secure Data Recovery (2024); Zheng et al. (2024); Cai et al. (2026); Schwaber & Sutherland (2020); Kruchten (1995); Park et al. (2023).
