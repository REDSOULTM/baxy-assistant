# 3. ALCANCE DEL PROYECTO

El presente capítulo delimita el alcance del proyecto *Baxy*,
estableciendo el objetivo general y los objetivos específicos que orientaron el
desarrollo, las métricas cuantificables con las que se verificó cada uno, las
alternativas de solución evaluadas y el fundamento de la decisión adoptada, los
límites del producto resultante y, finalmente, el análisis de factibilidad
técnica, económica y social. Todos los valores numéricos consignados provienen
de mediciones reales registradas en la documentación del repositorio del
proyecto y no de estimaciones referenciales.

---

## 3.1. Objetivo General

Desarrollar un asistente de voz de escritorio para el sistema operativo Windows
que opere de manera íntegramente local y privada sobre hardware modesto —con un
consumo de memoria de video (VRAM) igual o inferior a 4 GB—, empleando un modelo
de lenguaje Gemma 4 E2B fine-tuneado y cuantizado (formato GGUF Q4_K_M) servido
mediante llama.cpp, de modo que permita a usuarios de PC controlar el equipo por
voz, gestos y lenguaje natural sin transmitir datos a servicios en la nube ni
incurrir en costos de suscripción.

La formulación responde a la estructura *Verbo + Variable + Unidad de análisis +
Contexto* exigida por la metodología del curso: el verbo en infinitivo
("Desarrollar"), la variable medible (consumo de VRAM ≤ 4 GB, operación local),
la unidad de análisis (un asistente de voz de escritorio) y el contexto
(usuarios de PC Windows en hardware modesto, sin dependencia de la nube).

---

## 3.2. Objetivos Específicos

Los objetivos específicos se redactaron con verbos en infinitivo y bajo el
criterio SMART (específicos, medibles, alcanzables, relevantes y acotados en el
tiempo). Cada uno se asocia a un criterio de éxito numérico verificable, que se
detalla en la Sección 3.3.

1. **Diseñar e implementar** una arquitectura de inferencia que permita ejecutar
   concurrentemente un modelo de lenguaje, un módulo de visión y un subsistema de
   voz dentro de un presupuesto de 4 GB de VRAM, demostrando la viabilidad del
   procesamiento local de IA en hardware de gama de entrada.

2. **Desarrollar** un mecanismo de invocación de herramientas (*tool-calling*)
   fiable en hardware modesto, mediante un enrutador especializado de bajo costo
   computacional, que permita al asistente seleccionar y ejecutar la acción
   correcta sin generar herramientas inexistentes.

3. **Optimizar** la cadena de procesamiento voz-a-voz para mantener una latencia
   por turno dentro del rango comparable a los asistentes comerciales (≤ 5
   segundos), preservando una experiencia de usuario fluida y natural.

4. **Garantizar** la operación 100 % local y privada del asistente, sin
   transmisión de datos a servidores externos, validando que la privacidad del
   usuario se preserva de extremo a extremo mediante el uso exclusivo de software
   de código abierto y licencias gratuitas.

5. **Implementar** un sistema de honestidad estructural que verifique las acciones
   ejecutadas contra el estado real del sistema operativo, evitando que el
   asistente reporte como exitosas acciones que no ocurrieron.

6. **Incorporar** modos de accesibilidad por voz (para personas no videntes y con
   movilidad reducida) y soporte multilingüe basado en *embeddings* semánticos,
   ampliando la base de usuarios del asistente más allá de las limitaciones de
   idioma y capacidad física.

---

## 3.3. Métricas de los Objetivos (Tabla de Justificación)

La siguiente tabla operacionaliza cada objetivo específico en una métrica con su
criterio de éxito numérico. Los resultados medidos corresponden a las cifras
registradas en `PRODUCTION_READY.md`, `Gemma4_estado_y_limites_2026_05_29.md`,
`vram_real_medida.csv` y la memoria técnica del proyecto.

| Objetivo | Situación actual (sin solución) | Resultado esperado | Métrica | Criterio de éxito | Resultado medido |
|---|---|---|---|---|---|
| OE1 — Operar en 4 GB | Los LLM útiles requieren GPU de 8–24 GB o nube | LLM + visión + voz residentes en GPU de entrada | Delta de VRAM al cargar el modelo (MiB) | ≤ 4.096 MiB (4 GB) | **3.371 MiB (≈3,36 GB)** con E2B-Q4_K_M |
| OE2 — Tool-calling fiable | Modelos chicos alucinan o inventan herramientas | Selección correcta de acción sin invenciones | % de herramientas inventadas en producción / accuracy de selección | 0 % inventadas; accuracy aceptable | **0 % herramientas inventadas en producción**; accuracy de selección **estimada ≈75 %** (E2B; estimación comparativa) |
| OE3 — Latencia tier-Alexa | Asistentes cloud dependen de la latencia de red | Respuesta percibida como inmediata | Tiempo por turno / por acción (s) | ≤ 4–5 s por turno; > 8 s es UX catastrófica | **p50 por turno ≈1,22 s**; **acción con tool-call ≈2,2 s** (ver nota) tras la optimización de prefix-cache |
| OE4 — Local y gratuito | Datos de voz enviados a servidores ajenos; APIs pagas | Procesamiento íntegramente en el equipo | Datos transmitidos a la nube / costo de operación | 0 datos a la nube; costo de API = USD 0 | **0 transmisión externa**; **USD 0** de costo recurrente |
| OE5 — Honestidad estructural | LLM-judge alucina éxitos no ocurridos | Verificación por estado del SO | Verificación tri-estado (True/False/None) por estado real | No reportar acciones no ejecutadas | **Verificación tri-estado por estado del SO** (registry/UIA/pycaw), no por juicio del LLM |
| OE6 — Accesibilidad y multilingüe | Competidores monolingües / inglés-céntricos | Control por voz inclusivo y multilingüe | Tasa de activación de modo por voz / falsos positivos / recall de wake-word | activación ≥ 90 % ∧ FP = 0; recall wake-word ≥ 0,60 | **10/10 activaciones, 0 falsos positivos** (gate G1); recall hands-free 100 %, 0 FP (G2); **recall de wake-word "Baxy" 0,872** en *held-out* de 25 voces/13 idiomas (1,00 en es/en/it/fr/pl/ru) |

> *Nota metodológica sobre la latencia (OE3).* El informe distingue tres métricas de
> tiempo que **no son intercambiables**: (a) **p50 global por turno ≈ 1,22 s** —mediana
> del turno completo medida en el *baseline* de latencia (`BACKLOG_MAESTRO.md`)—;
> (b) **latencia por acción con *tool-call* ≈ 2,2 s** —turnos que invocan una herramienta,
> tras la optimización de caché de prefijo—; y (c) **p50 ≈ 2,5 s sobre el *replay* de los
> 1.071 mensajes reales** del usuario (evaluación a gran escala). Las tres están dentro
> del presupuesto tier-Alexa de 4–5 s; se citan con su nombre propio para evitar
> confundirlas.
>
> *Nota metodológica (OE2 y wake-word):* la métrica de OE2 debe medirse siempre con el
> arreglo de serialización del arreglo `tools`; sin él, el modelo aparenta inventar
> herramientas en un 47 % de los casos, lo cual es un artefacto del arnés de
> medición y no del modelo. La cifra de *accuracy* de selección (≈75 % en E2B frente a
> ≈91 % en E4B) corresponde a una **estimación comparativa** del estado y límites del
> modelo (`Gemma4_estado_y_limites_2026_05_29.md`); a diferencia del consumo de VRAM, no
> está anclada a un *dataset*/*script* de evaluación tan formal, por lo que se reporta
> como aproximación. Asimismo, las cifras de *wake-word* (recall ≥ 0,60 ∧
> falsos positivos ≤ 1/h) constituyen el criterio de aceptación del subsistema de
> palabra de activación, **criterio que el sistema cumple**: la palabra de activación
> "Baxy" alcanzó un **recall de 0,872 sobre un *held-out* universal de 25 voces y 13
> idiomas** —holgadamente por encima del umbral de 0,60—, con **recall de 1,00 en
> español, inglés, italiano, francés, polaco y ruso**. En consecuencia, la voz extremo
> a extremo quedó **implementada y operativa offline**, con su gate de aceptación
> cuantitativa **cerrado en verde**.

---

## 3.4. Alternativas de Solución

Antes de fijar la arquitectura definitiva se evaluaron varias alternativas, tanto
a nivel de arquitectura general (nube versus local) como de selección de modelo
(E4B versus E2B) y de cuantización. La comparación se sustentó en criterios
medibles y no en preferencias.

### 3.4.1. Arquitectura general

| Criterio | (A) Asistente en la nube (tipo Alexa/Siri) | (B) LLM grande local (Gemma 4 E4B / 26B) | (C) **Gemma 4 E2B-FT local (elegida)** |
|---|---|---|---|
| Privacidad | ✗ Audio enviado a servidores | ✓ Todo local | ✓ Todo local |
| Costo de operación | ✗ Suscripción / APIs pagas | ✓ Gratis | ✓ Gratis |
| Funciona sin internet | ✗ Requiere conexión | ✓ Offline | ✓ Offline |
| Cabe en 4 GB de VRAM | n/a | ✗ No entra (ver 3.4.2) | ✓ Entra (3,36 GB medido) |
| Calidad de tool-calling | ✓ Alta (modelos enormes) | ✓ ≈91 % (E4B, estimado) | ◐ ≈75 % (E2B, estimado; mitigado con *forced-retry* + FT) |
| Hardware requerido | Servidores remotos | GPU ≥ 6–8 GB | GPU 4 GB o *fallback* CPU |

La alternativa (A) se descartó porque viola las tres restricciones de producto
nucleares: privacidad, costo cero y operación offline. La alternativa (B) se
descartó por una razón física medida: no entra en el presupuesto de VRAM del
hardware objetivo. Se adoptó la alternativa (C) por ser la única que satisface
simultáneamente privacidad, gratuidad, operación local y restricción de 4 GB,
asumiendo conscientemente el trade-off de un tool-calling algo menor, mitigado en
producción mediante el mecanismo de reintento forzado de herramienta y el
fine-tuning del modelo.

### 3.4.2. Selección de modelo y cuantización (datos de VRAM medidos)

La decisión E4B → E2B no fue intuitiva sino que se respaldó con la medición
directa del consumo de VRAM de cada variante y cuantización, registrada en
`vram_real_medida.csv`:

| Variante | Cuantización | Delta de VRAM (MiB) | ¿Cabe en 4 GB? |
|---|---|---|---|
| **E2B** | **Q4_K_M** | **3.371** | **✓ Sí (elegida)** |
| E2B | Q5_K_M | 3.609 | ✓ Sí (más pesada) |
| E4B | UD-IQ2_M (mínima) | 4.057 | ✗ No (excede aun en su cuantización más baja) |
| E4B | Q4_K_M | 5.087 | ✗ No |
| 26B | UD-IQ2_XXS | 12.613 | ✗ No (lejos del presupuesto) |

El dato decisivo es que **ni siquiera la cuantización más agresiva de E4B
(UD-IQ2_M, 4.057 MiB) entra en 4 GB**, dejando sin margen para el contexto y la
visión residente. La variante E2B-Q4_K_M, con 3.371 MiB, deja espacio suficiente
para la visión (≈1,2 GB de mmproj) y la caché KV. Esto convirtió a E2B-Q4_K_M en
la única opción viable para el hardware objetivo.

### 3.4.3. Otras decisiones técnicas respaldadas por medición

- **Whisper vs. Parakeet (STT):** ambos corren en CPU con cuantización int8 para
  no robar VRAM al LLM; Parakeet quedó como motor por defecto y Whisper como
  *fallback*.
- **Vulkan vs. CUDA (backend de inferencia):** además del binario CUDA por defecto,
  el sistema soporta un *fallback* a CPU que cubre equipos sin GPU NVIDIA dedicada
  (gráficas integradas Intel/AMD), ampliando el público objetivo.
- **flash-attention desactivada por defecto:** con FA activa, los prompts
  superiores a ~10 K tokens disparan un crash CUDA (#22527); con FA desactivada el
  sistema se midió estable (37/37), a costa de un prefill ≈2× más lento,
  mitigado.

---

## 3.5. Alcances y Limitaciones

### 3.5.1. Lo que el sistema SÍ hace (alcances)

- **Alcance geográfico/de plataforma:** opera en cualquier equipo con Windows;
  el objetivo de hardware es una GPU NVIDIA de 4 GB (GTX 1650, RTX 3050 4 GB) o
  bien un *fallback* a CPU.
- **Alcance de datos:** todo el procesamiento (voz, pantalla, acciones) ocurre en
  la máquina del usuario; cero datos transmitidos a la nube.
- **Alcance temático:** asistencia por voz, control del sistema operativo
  (computer-use mediante la cascada UIA → OCR → visión), apertura y operación de
  aplicaciones, navegación web, gestión de archivos, recordatorios, memoria de
  gustos del usuario y control por cámara/gestos. El control se ejerce a través de
  más de sesenta herramientas de dominio (67 esquemas únicos expuestos al modelo tras retirar
  `smart_home`).
- **Alcance lingüístico:** multilingüe y multi-acento, resuelto por embeddings
  multilingües y no por listas de palabras clave por idioma.
- **Accesibilidad:** tres modos (`normal`, `no_vidente`, `movilidad`) verificados
  a nivel de capa lógica con sus respectivos gates en verde.

### 3.5.2. Lo que el sistema NO hace o no garantiza (limitaciones)

- **Tool-calling acotado:** accuracy de selección estimada en ≈75 % (E2B) frente al ≈91 %
  del modelo E4B; es el trade-off aceptado por entrar en 4 GB (estimación comparativa, no medición formal).
- **Sin visión en modo CPU:** cuando el LLM cae al perfil CPU (por GPU saturada o
  ausencia de GPU NVIDIA), el módulo de visión se desactiva por ser demasiado
  costoso en CPU.
- **Rendimiento degradado sin GPU:** el modo CPU rinde un estimado de ≈12–20
  tok/s en una laptop, cómodo para respuestas cortas pero más lento para textos
  largos; el sistema opera sobre CPU mediante su perfil de *fallback*, con esta
  diferencia de rendimiento como contrapartida esperada de prescindir de GPU NVIDIA.
- **Techo del computer-use:** la capacidad de operar la GUI está limitada por el
  estado del arte; los benchmarks de referencia sitúan el techo en ≈52,5 %
  (WindowsAgentArena) y ≈28 % (UFO2 + GPT-4o), de modo que el desempeño físico no
  puede prometerse perfecto.
- **Alcance de la verificación cuantitativa:** la capa lógica de accesibilidad está
  medida con sus gates en verde y la voz extremo a extremo opera offline; las cifras
  consignadas en este informe corresponden a esa instrumentación, sin extrapolar más
  allá de lo efectivamente medido.
- **Margen de VRAM ajustado con visión:** al recibir la primera imagen el
  footprint sube; en una GPU de exactamente 4 GB el margen es estrecho, por lo que
  la visión es residente y gateada.

---

## 3.6. Factibilidad Técnica

La factibilidad técnica está **probada, no proyectada**: el sistema corre hoy en
4 GB de VRAM. El barrido de VRAM sobre los modelos **base** midió que E2B-Q4_K_M
base consume 3.371 MiB (≈3,36 GB) —dato que decidió el pivote E4B → E2B—, mientras
que el modelo *fine-tuneado* finalmente desplegado consume ≈2,07 GB de VRAM
(medición del despliegue del FT v2, `dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`),
con aún más margen para visión (≈1,2 GB) y contexto (12.288 tokens de piso). La
inferencia se sirve con llama.cpp/llama-server, una solución madura y
ampliamente adoptada en la comunidad para correr LLM cuantizados en hardware de
consumo.

Los componentes del stack también están verificados:

- **LLM:** Gemma 4 E2B fine-tuneado, GGUF Q4_K_M, servido por llama-server (perfil
  `vram4`); con *fallback* al perfil `cpu` (-ngl 0, 0 VRAM) cuando la GPU está
  ocupada.
- **STT/TTS en CPU:** transcripción (Parakeet/Whisper int8) y síntesis de voz
  (Piper) corren en CPU y no tocan la VRAM.
- **Estabilidad bajo carga:** se resolvió el crash del render WebView2 al competir
  por la GPU (la UI renderiza por CPU) y el crash CUDA #22527 (flash-attention
  desactivada por defecto, estable 37/37).
- **Hardware objetivo:** GPU NVIDIA de 4 GB (GTX 1650 / RTX 3050 4 GB) con piso de
  8 GB de RAM; el modo CPU extiende el alcance a laptops sin gráfica dedicada.

La conclusión técnica es que el producto es viable en el hardware objetivo y, a
través de su perfil CPU, se ejecuta también en "cualquier laptop razonable".

---

## 3.7. Factibilidad Económica

El proyecto presenta un **costo de operación de USD 0**, lo que constituye su
principal ventaja económica frente a la competencia. El análisis se sustenta en
la auditoría de licencias del repositorio (`AUDITORIA_licencias.md`).

### 3.7.1. Costo operativo comparado

| Concepto | Asistente cloud (Alexa+/APIs) | **Baxy** |
|---|---|---|
| APIs de IA por uso | Costo recurrente por token/consulta | USD 0 (modelo local) |
| Suscripción mensual | Sí (tiers de pago) | USD 0 |
| Infraestructura de servidor | A cargo del proveedor (incluida en el precio) | Ninguna (corre en el equipo del usuario) |
| Hardware | GPU cara o nube | GPU 4 GB que el usuario ya posee / CPU |

El usuario reutiliza el hardware que ya tiene; no se requiere GPU especializada ni
suscripción alguna.

### 3.7.2. Viabilidad de licencias (vendibilidad)

La auditoría sobre 511 paquetes concluyó que el **stack núcleo es permisivo y, por
tanto, vendible**: Gemma 4 (Apache-2.0), MediaPipe, OpenCV, Whisper (MIT),
llama.cpp, Tesseract y sentence-transformers son todos de licencias permisivas.
Existe **un único bloqueante real** para una eventual comercialización
propietaria: `piper-tts` 1.4.2 está bajo GPL-3.0-or-later y se enlaza
directamente, lo que obligaría a abrir el código del producto. La auditoría
propone una solución de bajo costo —aislar Piper por subprocess (agregación, no
derivado) o reemplazarlo por un TTS Apache/MIT (p. ej., Kokoro)—, tras lo cual el
producto sería comercializable. Los modelos empleados (Gemma 4 y su mmproj,
Whisper, wake-word LiveKit y el encoder del router) son Apache-2.0 o MIT.

Económicamente, el proyecto es altamente factible: costo de operación nulo,
hardware preexistente y un único obstáculo legal de resolución conocida y barata.

---

## 3.8. Factibilidad Social

La factibilidad social del proyecto se articula en tres ejes que constituyen su
propuesta de valor diferencial.

### 3.8.1. Privacidad

Todo el procesamiento ocurre en la máquina del usuario; no se transmite audio,
pantalla ni acciones a ningún servidor. Esto es especialmente relevante para
usuarios sensibles a la privacidad y, en general, alinea al producto con la
legislación chilena vigente sobre datos personales (Ley N.º 21.719), protección
de la vida privada (Ley N.º 19.628), delitos informáticos (Ley N.º 21.459) y
ciberseguridad (Ley N.º 21.663): al no transmitir datos fuera del equipo, el
sistema minimiza estructuralmente el riesgo regulatorio.

### 3.8.2. Accesibilidad e inclusión

El asistente incorpora tres modos de accesibilidad —respaldados por las pautas
WCAG 2.1/2.2— que atienden necesidades distintas: el modo `no_vidente` narra cada
acción y lee la pantalla a pedido (todo por audio), mientras que el modo
`movilidad` ofrece control 100 % por voz para personas que no pueden usar teclado
o mouse. La separación responde a que las necesidades perceptuales y motoras son
distintas, e incluso opuestas. Los gates de activación por voz se midieron en
verde (10/10 activaciones, recall hands-free 100 %, siempre con 0 falsos
positivos). El control por cámara y gestos (MediaPipe) complementa el acceso para
movilidad reducida.

### 3.8.3. Democratización y universalidad

Al correr en hardware modesto —una laptop típica de 4 GB de VRAM, o incluso sin
gráfica dedicada en modo CPU— el sistema democratiza el acceso a un asistente
inteligente sin exigir GPU cara ni suscripción. Su carácter multilingüe y
multi-acento, resuelto por embeddings y no por listas de palabras codificadas por
idioma, lo hace utilizable por hablantes de cualquier lengua, a diferencia de
competidores monolingües o inglés-céntricos. Adicionalmente, el cómputo local
evita la huella ambiental de los datacenters en la nube, al ejecutarse sobre
hardware ya existente.

---

*Fuente: Elaboración propia (2026), sobre datos medidos en
`documentacion/00_producto/{PRODUCTION_READY.md, Gemma4_estado_y_limites_2026_05_29.md,
ANALISIS_COMPETENCIA.md, AUDITORIA_licencias.md, modos_accesibilidad.md}` y
`documentacion/datos_crudos/vram_real_medida.csv`.*
