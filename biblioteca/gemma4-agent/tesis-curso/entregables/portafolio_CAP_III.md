# PORTAFOLIO DE PROYECTOS — CAPÍTULO III

## Metodología, Alternativas y Factibilidades, Plan de Proyecto, Arquitectura y Diseño de Alto Nivel, Herramientas de Gestión, Monitoreo y Producto Funcional

> **Proyecto:** *Baxy* — Asistente de voz de escritorio local y privado para Windows en hardware modesto (≤ 4 GB de VRAM) mediante un modelo Gemma 4 E2B *fine-tuneado*.
> **Asignatura:** INSW410 — Portafolio de Proyectos (Universidad Andrés Bello).
> **Autor:** Emmanuel Villacura Arancibia. **Profesor guía:** Nicolás Caselli. **Viña del Mar, Chile, 2026.**
>
> Este capítulo es acumulativo respecto de los Capítulos I (problema, oportunidad, Ishikawa, estado del arte) y II (objetivos, métricas, alcance, normativa, plan inicial). Todos los valores numéricos consignados provienen de mediciones reales registradas en el repositorio del proyecto y no de estimaciones aspiracionales. Las fuentes internas principales son `documentacion/00_producto/{Gemma4_estado_y_limites_2026_05_29.md, ANALISIS_COMPETENCIA.md, AUDITORIA_licencias.md, PRODUCTION_READY.md}`, `documentacion/01_arquitectura/{ARCHITECTURE.md, README.md, 01_delta_system.md, 02_delta_components.md, 04_sequences/}`, `documentacion/datos_crudos/vram_real_medida.csv`, `documentacion/_backlog/BACKLOG_MAESTRO.md` y la memoria persistente del proyecto (`MEMORY.md`), además del historial del repositorio Git.

---

## 1. Introducción del Capítulo

El presente capítulo desarrolla la dimensión de **gestión y construcción** del proyecto *Baxy*. A partir del problema y la oportunidad caracterizados en el Capítulo I y de los objetivos, métricas, alcance y restricciones definidos en el Capítulo II, este capítulo expone: (a) la **metodología de gestión** elegida mediante una tabla comparativa ponderada (Scrum) y la **metodología de desarrollo** técnica adoptada (iterativa-incremental dirigida por medición); (b) las **alternativas de solución** evaluadas con sus respectivas factibilidades técnica, económica y social; (c) el **plan de proyecto**, que comprende los requisitos, la arquitectura y el diseño de alto nivel documentado mediante las cuatro vistas del modelo 4+1 de Kruchten (1995); (d) la **selección de herramientas de apoyo a la gestión** (control de versiones Git y backlog maestro); (e) el **plan de monitoreo y control** basado en *gates* medidos; y (f) la **evidencia del producto funcional** —el sistema Baxy, terminado y operativo hoy en hardware modesto—.

El principio metodológico rector que atraviesa todo el capítulo es el lema operativo del proyecto: **"medir, no celebrar"**. Ninguna decisión técnica se dio por válida sin un número contrastado contra un criterio de éxito (un *gate*) definido de antemano. Este rigor, formalizado en el documento de instrucciones de trabajo del repositorio (`CLAUDE.md`), es lo que confiere carácter empírico —y no aspiracional— a las cifras de este capítulo.

---

## 2. Metodología de Gestión del Proyecto

### 2.1. Selección de la metodología mediante tabla comparativa ponderada

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

### 2.2. Decisión y justificación

La metodología seleccionada es **Scrum** (puntaje ponderado 2,85), con incorporación de prácticas de **Kanban** (puntaje 2,50) para la visualización del flujo de trabajo en el backlog. La elección se justifica por tres líneas de evidencia:

1. **Compatibilidad con la incertidumbre técnica.** Schwaber y Sutherland (2020) definen Scrum precisamente como un marco para abordar problemas complejos y adaptativos mediante entregas incrementales, lo que coincide con el perfil de este proyecto: comprometerse a un plan cerrado al inicio (Cascada, puntaje 1,30) habría sido inviable cuando ni siquiera era seguro que la solución cupiera en el presupuesto de memoria del hardware objetivo.

2. **Evidencia empírica del propio desarrollo.** El historial del repositorio y la memoria persistente del proyecto documentan un trabajo **organizado explícitamente en sprints iterativos**. Existe rastro real de, entre otros, el "Sprint 7" del *wake-word*, los sprints "Sprint 1 R1–R8" de refactorización arquitectónica, los sucesivos sprints de optimización del enrutador (*router*) y los sprints de *fine-tuning* del modelo. El desarrollo fue, de hecho, gestionado de manera ágil e iterativa antes incluso de formalizar la metodología.

3. **Entrega temprana de valor.** El enfoque ágil permitió liberar tempranamente un incremento funcional —un asistente capaz de responder por voz de forma *offline*— y crecer en capacidades hasta el producto completo sin congelar el alcance.

Scrum se materializó en el proyecto a través de iteraciones acotadas (los *sprints* del historial), cada una con un objetivo concreto y cerrada contra un criterio de éxito medible; un backlog único como artefacto central de planificación; y revisiones que, en lugar de demostraciones subjetivas, exigían la **validación en vivo** del incremento contra el modelo y el agente reales.

---

## 3. Metodología de Desarrollo

La metodología de desarrollo adoptada es **iterativa e incremental, orientada a entregas con valor desde la primera iteración** y, de manera distintiva, **dirigida por medición contra criterios de éxito definidos a priori** (*gates*). Esta es la regla de oro operativa del proyecto, formalizada en su documento de instrucciones de trabajo (`CLAUDE.md`): *ninguna decisión técnica se da por hecha ni se declara exitosa sin un número contra un criterio definido de antemano*.

El ciclo de desarrollo de cada funcionalidad sigue un patrón disciplinado y reproducible de cuatro pasos:

1. **Medir primero.** Antes de implementar, se establece la línea base y el criterio de éxito numérico (el *gate*). Por ejemplo, el *wake-word* se evaluó contra el gate `recall ≥ 0,60 ∧ falsos positivos/hora ≤ 1,0` sobre un conjunto de prueba universal y diverso (no sobre la voz del operador, para preservar la universalidad multilingüe y multi-acento que exige el producto).

2. **Implementar de forma gateada.** Las funcionalidades nuevas se incorporan detrás de un *feature flag* en estado desactivado por defecto (*default-off*), de modo que no alteren el comportamiento estable mientras se validan.

3. **Validar en vivo.** Un cambio que afecta el comportamiento del agente no se considera terminado hasta ejecutarlo contra el modelo de lenguaje y el agente reales, con el mensaje que un usuario escribiría, verificando la cadena completa de herramientas y la respuesta final. Esta exigencia surgió de un fallo concreto: una corrección probada solo con pruebas unitarias *mockeadas* falló en producción porque el modelo, al ser no-determinista, eligió un camino de herramienta distinto al anticipado (corrigió `browser.open` pero el modelo enrutó hacia `browser.search`).

4. **Activar (*flip* a *default-on*).** Solo tras la validación en vivo se promueve la funcionalidad a comportamiento por defecto.

Este enfoque permitió liberar tempranamente un incremento funcional e ir agregando capacidades (*tool-calling*, *computer-use*, optimización de latencia, memoria de gustos, accesibilidad por cámara) en iteraciones sucesivas hasta consolidar el producto completo, cada una cerrada contra su propio *gate*. Se mantiene, además, una distinción explícita en todos los reportes entre **validación sintética** (pruebas con datos generados) y **prueba real** (con la voz y el equipo del usuario), en línea con la práctica de medición honesta del proyecto.

---

## 4. Propuesta de Alternativas de Solución y sus Factibilidades

### 4.1. Alternativas de arquitectura general

Antes de fijar la arquitectura definitiva se evaluaron varias alternativas, tanto a nivel de arquitectura general (nube versus local) como de selección de modelo y de cuantización. La comparación se sustentó en criterios medibles y no en preferencias.

**Tabla 3.2.** *Alternativas de arquitectura general.*

| Criterio | (A) Asistente en la nube (tipo Alexa/Siri) | (B) LLM grande local (Gemma 4 E4B / 26B) | (C) **Gemma 4 E2B-FT local (elegida)** |
|---|---|---|---|
| Privacidad | ✗ Audio enviado a servidores | ✓ Todo local | ✓ Todo local |
| Costo de operación | ✗ Suscripción / APIs pagas | ✓ Gratis | ✓ Gratis |
| Funciona sin internet | ✗ Requiere conexión | ✓ Offline | ✓ Offline |
| Cabe en 4 GB de VRAM | n/a | ✗ No entra (ver 4.2) | ✓ Entra (3,36 GB medido) |
| Calidad de *tool-calling* | ✓ Alta (modelos enormes) | ✓ ≈91 % (E4B, estimado) | ◐ ≈75 % (E2B, estimado; mitigado con *forced-retry* + FT) |
| Hardware requerido | Servidores remotos | GPU ≥ 6–8 GB | GPU 4 GB o *fallback* CPU |

*Fuente: Elaboración propia (2026), a partir de `ANALISIS_COMPETENCIA.md` y `vram_real_medida.csv`.*

La alternativa **(A)** se descartó porque viola las tres restricciones de producto nucleares: privacidad, costo cero y operación *offline*. La alternativa **(B)** se descartó por una razón física medida: no entra en el presupuesto de VRAM del hardware objetivo. Se adoptó la alternativa **(C)** por ser la única que satisface simultáneamente privacidad, gratuidad, operación local y la restricción de 4 GB, asumiendo conscientemente el *trade-off* de un *tool-calling* algo menor, mitigado en producción mediante el mecanismo de reintento forzado de herramienta (*forced-retry*) y el *fine-tuning* del modelo.

### 4.2. Alternativa de selección de modelo y cuantización (datos de VRAM medidos)

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

### 4.3. Otras decisiones técnicas respaldadas por medición

- **Whisper vs. Parakeet (STT):** ambos corren en CPU con cuantización int8 para no robar VRAM al LLM; Parakeet quedó como motor por defecto y Whisper como *fallback*.
- **Vulkan vs. CUDA (backend de inferencia):** el binario por defecto es CUDA y el sistema implementa además el *fallback* a CPU (perfil `cpu`, `-ngl 0`), que cubre por software la ejecución sin GPU dedicada; el empaquetado del *build* CPU/Vulkan extiende esta cobertura a gráficas integradas Intel/AMD, ampliando el público objetivo.
- **flash-attention desactivada por defecto:** con FA activa, los *prompts* superiores a ~10 K tokens disparan un *crash* CUDA (#22527); con FA desactivada el sistema se midió estable (37/37 turnos), a costa de un *prefill* ≈2× más lento, mitigado.

### 4.4. Factibilidad de la solución elegida

#### 4.4.1. Factibilidad técnica

La factibilidad técnica está **probada, no proyectada**: el sistema corre hoy en 4 GB de VRAM. El barrido de VRAM sobre los modelos **base** midió que E2B-Q4_K_M base consume 3 371 MiB (≈3,36 GB) —dato que decidió el pivote E4B → E2B—, mientras que el modelo *fine-tuneado* finalmente desplegado consume ≈2,07 GB de VRAM (medición del despliegue del FT v2, `dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`), con aún más margen para visión (≈1,2 GB) y contexto (12 288 tokens de piso). La inferencia se sirve con `llama.cpp`/`llama-server`, una solución madura y ampliamente adoptada en la comunidad para correr LLM cuantizados en hardware de consumo. Los componentes del *stack* están verificados:

- **LLM:** Gemma 4 E2B *fine-tuneado*, GGUF Q4_K_M, servido por `llama-server` (perfil `vram4`); con *fallback* al perfil `cpu` (`-ngl 0`, 0 VRAM) cuando la GPU está ocupada.
- **STT/TTS en CPU:** transcripción (Parakeet/Whisper int8) y síntesis de voz (Piper) corren en CPU y no tocan la VRAM.
- **Estabilidad bajo carga:** se resolvió el *crash* del render WebView2 al competir por la GPU (la UI renderiza por CPU) y el *crash* CUDA #22527 (*flash-attention* desactivada por defecto, estable 37/37).
- **Hardware objetivo:** GPU NVIDIA de 4 GB (GTX 1650 / RTX 3050 4 GB) con piso de 8 GB de RAM; el modo CPU, ya operativo en software, amplía el alcance a *laptops* sin gráfica dedicada, y el binario CPU/Vulkan extiende esa cobertura a gráficas integradas Intel/AMD.

La conclusión técnica es que el producto es viable y operativo en el hardware objetivo, y el modo CPU lo lleva además a "cualquier *laptop* razonable".

#### 4.4.2. Factibilidad económica

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

#### 4.4.3. Factibilidad social

La factibilidad social del proyecto se articula en tres ejes que constituyen su propuesta de valor diferencial:

- **Privacidad.** Todo el procesamiento ocurre en la máquina del usuario; no se transmite audio, pantalla ni acciones a ningún servidor. Esto alinea al producto con la legislación chilena vigente sobre datos personales (Ley N.º 21.719), protección de la vida privada (Ley N.º 19.628), delitos informáticos (Ley N.º 21.459) y ciberseguridad (Ley N.º 21.663): al no transmitir datos fuera del equipo, el sistema minimiza estructuralmente el riesgo regulatorio.
- **Accesibilidad e inclusión.** El asistente incorpora tres modos de accesibilidad —respaldados por las pautas WCAG 2.1/2.2— que atienden necesidades distintas: el modo `no_vidente` narra cada acción y lee la pantalla a pedido (todo por audio), y el modo `movilidad` ofrece control 100 % por voz para personas que no pueden usar teclado o ratón. Los *gates* de activación por voz se midieron en verde (10/10 activaciones, *recall hands-free* 100 %, siempre con 0 falsos positivos). El control por cámara y gestos (MediaPipe) complementa el acceso para movilidad reducida.
- **Democratización y universalidad.** Al correr en hardware modesto —una *laptop* típica de 4 GB de VRAM, o incluso sin gráfica dedicada en modo CPU—, el sistema democratiza el acceso a un asistente inteligente sin exigir GPU cara ni suscripción. Su carácter multilingüe y multi-acento, resuelto por *embeddings* y no por listas de palabras codificadas por idioma, lo hace utilizable por hablantes de cualquier lengua. El cómputo local evita, además, la huella ambiental de los *datacenters* en la nube.

---

## 5. Plan de Proyecto

### 5.1. Requisitos del sistema

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

### 5.2. Planificación (fases del desarrollo)

La planificación del proyecto se organizó en un horizonte de tres meses, estructurado en una **fase de inicio/análisis**, seguida de **tres iteraciones de desarrollo incremental** y una **fase de cierre**, en coherencia con la metodología iterativa-incremental adoptada. Cada iteración corresponde a un agrupamiento de los sprints reales del historial del proyecto y cierra contra *gates* medidos.

- **Fase 0 — Inicio y Análisis.** Definición del problema, análisis de competencia y de licencias, definición de objetivos SMART con sus criterios de éxito y establecimiento del método dirigido por medición. Entregable: marco del proyecto y backlog inicial.
- **Iteración 1 — Núcleo: LLM local en 4 GB + voz básica.** Selección de modelo respaldada por la medición de VRAM, STT/TTS en CPU y *wake-word*. *Gates*: VRAM ≤ 4 GB (cumplido); *wake-word* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872 en un *held-out* universal de 25 voces y 13 idiomas, por encima del umbral 0,60, con 1,00 en es/en/it/fr/pl/ru). Evidencia de liberación: el agente responde por voz *offline* y la palabra de activación "Baxy" opera de forma estable.
- **Iteración 2 — Tool-calling, router y acciones (computer-use).** Construcción del enrutador (encoder *fine-tuneado* + *abstain head*), *fine-tuning* del modelo E2B y cascada de *computer-use* (UIA → OCR → visión). *Gates*: router 0,9964 (*held-out* ES); 0 % herramientas inventadas en producción.
- **Iteración 3 — Robustez, latencia, memoria y accesibilidad.** Optimización de latencia (caché de prefijo), capa de memoria de gustos, interfaz web "Baxy field", control por cámara/gestos y *fallback* a CPU. *Gates*: latencia ≤ 5 s; suite de pruebas verde.
- **Fase de Cierre.** Consolidación del Backlog Maestro, estabilización de la suite (2 740 pruebas aprobadas, 0 fallidas) y documentación de la entrega final del producto.

La siguiente carta Gantt resume el cronograma sobre el horizonte de tres meses; las fechas agrupan los sprints reales del historial del repositorio y cada iteración cierra contra su *gate* medido. La representación gráfica (diagrama de Gantt y flujo gateado) se entrega como artefacto de planificación complementario (figuras "Metodología Gantt" y "flujo gateado" del documento de diagramas del proyecto).

**Tabla 3.6.** *Carta Gantt: fases, iteraciones e hitos de cierre.*

| Fase / Iteración | Período aproximado | Foco principal | Hito de cierre (gate) |
|---|---|---|---|
| Fase 0 — Inicio / Análisis | Mar 2026 (2 sem.) | Problema, competencia, licencias, objetivos SMART, método *measure-then-ship* | Marco del proyecto y *backlog* inicial |
| Iteración 1 — Núcleo | Mar–Abr 2026 (~3–4 sem.) | LLM local en 4 GB (E2B-Q4) + herramientas + enrutador + voz básica | VRAM ≤ 4 GB (cumplido); *wake-word* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (cumplido: recall 0,872) |
| Iteración 2 — Tool-calling / acciones | Abr–May 2026 (~3–4 sem.) | Router (encoder-FT + *abstain*) + computer-use (UIA/OCR/visión) + visión/cámara | Router 0,9964 (held-out ES); 0 % herramientas inventadas en producción |
| Iteración 3 — Robustez / latencia / memoria / accesibilidad | May–Jun 2026 (~3–4 sem.) | Fine-tuning E2B + optimización de latencia + memoria "Jarvis" + UI + fallback CPU | Latencia ≤ 5 s (p50 ≈ 1,22 s); suite verde |
| Fase de Cierre | Jun 2026 (2 sem.) | Backlog Maestro + suite 2 740/0 + entrega final del producto + informe final | Suite 2 740 aprobadas, 0 fallidas (2026-06-02) |

*Fuente: Elaboración propia (2026); fechas derivadas del historial del repositorio (sprints de 2026-05-26 a 2026-06-02 en la memoria del proyecto).*

---

## 6. Arquitectura y Diseño de Alto Nivel (Modelo 4+1 de Kruchten)

El diseño de alto nivel del sistema se documenta mediante una **adaptación del modelo de vistas arquitectónicas "4+1" propuesto por Kruchten (1995)**, el cual describe la arquitectura a través de vistas concurrentes y complementarias —en su forma canónica: Lógica, de Proceso, de Desarrollo y Física, más la vista de Escenarios (+1)—, cada una orientada a un grupo distinto de interesados. Dado que el producto se ejecuta sobre un **nodo único** (el equipo del usuario, sin componentes distribuidos ni concurrencia entre nodos), esta sección adapta el modelo y presenta cuatro vistas: **Lógica, Física, de Despliegue y de Escenarios**; la vista de **Proceso** se subsume en las vistas Lógica (el *hot-path* del turno) y de Despliegue (los procesos `llama-server`/`server`/UI), y la vista de **Desarrollo** se refleja en la organización en sub-paquetes descrita en la vista Lógica. Se reconoce explícitamente esta adaptación para no presentarla como el 4+1 estándar. La arquitectura aquí descrita no es de diseño teórico: fue **verificada leyendo el código y midiendo en vivo** durante una auditoría de tres sesiones (rondas 1–13), según consta en `documentacion/01_arquitectura/ARCHITECTURE.md`.

El norte rector de la arquitectura, ante cualquier conflicto de prioridades, es: (1) preservación de invariantes y aislamiento del *blast radius* (radio de daño); (2) fiabilidad —nunca caer el turno, perder una respuesta ni degradar en silencio—; y (3) latencia dentro del presupuesto tier-Alexa.

### 6.1. Diagrama de contexto (propuesta de solución macro)

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

### 6.2. Vista Lógica (paquetes del agente)

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

### 6.3. Vista Física (mapeo a hardware)

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

### 6.4. Vista de Despliegue (instalación y ejecución)

La Vista de Despliegue describe cómo se instala y arranca el sistema. El producto se despliega como un conjunto de **procesos locales coordinados**, sin servicios remotos:

1. **Proceso `llama-server`** (motor de inferencia): binario de `llama.cpp` que sirve el modelo GGUF por HTTP en el puerto local `:8080`. Se gestiona desde `LlamaServerManager`, que selecciona un **perfil de VRAM**: `vram4` (por defecto, CUDA, `-ngl` completo) o `cpu` (`-ngl 0`, 0 VRAM, visión desactivada) como *fallback*. El gestor garantiza el cierre del proceso en `atexit` (en Windows el hijo de `Popen` no muere con el padre y retendría ~3,4 GB de VRAM).
2. **Proceso de la UI** (`pywebview` + WebView2 renderizando React/Vite), que renderiza por CPU (`--disable-gpu`) para no competir por la GPU.
3. **Entornos virtuales (venvs) aislados**, por incompatibilidad de dependencias: `.venv` (Python 3.10, runtime del agente), `.venv_livekit` (Python 3.11, *training* del *wake-word*) y un venv Python 3.12 (*fine-tuning* con Unsloth). En tiempo de ejecución NO se importa el paquete `livekit`; los modelos ONNX se sirven con `onnxruntime` puro (verificado idéntico al oficial al 4.º decimal).

Configuración de estabilidad (perfil `vram4`, todo medido): `flash-attn` **OFF** por defecto, contexto **12 288** tokens y *throttle* de ONNX Runtime antes de cargar modelos en *loops* pesados. **Despliegue sin GPU:** operativo a nivel de software, ya que el LLM cae al perfil `cpu`; el binario `llama-server` CPU/Vulkan extiende esta cobertura a gráficas integradas Intel/AMD para ampliar el alcance de mercado.

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

### 6.5. Vista de Escenarios (casos de uso clave)

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

## 7. Herramientas de Apoyo a la Gestión

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

## 8. Plan de Monitoreo y Control

El monitoreo y control del avance se estructuró sobre tres dimensiones —**integridad técnica, valor de negocio y calidad**— y se materializó en instrumentos concretos y reproducibles, no en estimaciones subjetivas de porcentaje de avance.

**a) Gates medidos por subsistema.** Cada objetivo tiene asociado un criterio de éxito numérico que actúa como control de progreso. Los *gates* efectivamente medidos y cerrados incluyen: consumo de VRAM ≤ 4 GB (medido 3,36 GB para E2B-Q4); 0 % de herramientas inventadas en producción para el modelo *fine-tuneado*; precisión de *routing* de 0,9964 sobre un *held-out* en español; latencia por acción ≤ 4–5 s (medida en ~2,2 s tras la optimización de caché de prefijo). El *gate* de *wake-word* (`recall ≥ 0,60 ∧ fp/hr ≤ 1,0`) también se cerró: la palabra de activación "Baxy" alcanzó un recall de 0,872 sobre un *held-out* universal de 25 voces y 13 idiomas, por encima del umbral 0,60, con recall 1,00 en es/en/it/fr/pl/ru.

**b) Suite de pruebas automatizadas como control de calidad.** La suite creció a lo largo del desarrollo y debe permanecer verde como condición de avance. El estado de cierre reportado es de **2 740 pruebas aprobadas y 0 fallidas** (`BACKLOG_MAESTRO.md`, 2026), incluyendo pruebas de regresión por idioma y por dominio, y guardas estructurales (anti-*loop*, anti-alucinación, verificación de honestidad). El principio anti-regresión es explícito: ningún subgrupo fuerte debe degradarse al mejorar el promedio.

**c) Evaluaciones batch reproducibles.** El control incorpora evaluaciones a gran escala desde scripts versionados. Un ejemplo es el *replay* de los 1 071 mensajes únicos reales del usuario, re-ejecutados a través de la interfaz del agente con la ejecución física *mockeada*, que arrojó una latencia mediana (p50) de 2,5 s, cero errores y cero fugas de herramientas, permitiendo detectar y corregir incidencias sistémicas.

**d) Backlog maestro como tablero de control.** Se consolidó un único **Backlog Maestro** como fuente de verdad, en el que cada ítem se verifica contra el código real y se marca con un estado explícito. Esta verificación cruzada —tres auditores cotejando más de 50 ítems— constituye el reporte de progreso consolidado.

### 8.1. Gestión de riesgos

La gestión de riesgos se aborda como un **análisis a priori de la fase de planificación**:
antes de construir el sistema se identifican los riesgos previsibles del desafío y se define
para cada uno una **estrategia de mitigación preventiva**. Cada riesgo se cuantifica en una
escala de 1 a 5 de **probabilidad (P)** e **impacto (I)**; la **exposición (E = P × I)**
clasifica el riesgo en zonas Baja (1–6), Media (7–12) y Alta (15–25), y se reporta el
**riesgo residual** esperado tras la mitigación (criterio PMBOK / ISO 31000).

**Tabla 3.8.** *Matriz de riesgos del proyecto (evaluación a priori; extracto de los riesgos de mayor exposición).*

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

*Fuente: Elaboración propia (2026). Evaluación a priori de la planificación; la matriz completa de doce riesgos (R1–R12) con su descripción y metodología de escala consta en el documento de artefactos del portafolio.*

---

## 9. Evidencia del Producto Funcional

El proyecto **no se encuentra en fase de propuesta, sino que constituye un producto terminado y operativo** que se ejecuta hoy en hardware modesto. Esta sección documenta qué hace el sistema Baxy, sustentado en mediciones reales.

### 9.1. Qué hace Baxy hoy

- **Conversa por voz de extremo a extremo y de forma *offline*.** El usuario activa el asistente con una palabra clave (*wake-word*); su voz se transcribe en CPU; el modelo Gemma 4 E2B *fine-tuneado* genera la respuesta o decide invocar una herramienta; y la respuesta se sintetiza por voz (Piper, en CPU). Toda la cadena ocurre en la máquina del usuario, sin transmitir datos a la nube.
- **Controla el sistema operativo mediante más de sesenta herramientas de dominio** (67 esquemas únicos expuestos al modelo tras retirar `smart_home`)**.** Abre y opera aplicaciones, navega la web, gestiona mensajería (WhatsApp), instala y controla juegos (Steam), ajusta volumen y brillo, gestiona archivos y recordatorios, y ejecuta *computer-use* sobre cualquier aplicación mediante la cascada UIA → OCR → visión.
- **Opera dentro de 4 GB de VRAM.** El barrido de VRAM sobre los modelos **base** midió que E2B-Q4_K_M base consume 3 371 MiB (≈3,36 GB) de delta de VRAM (`documentacion/datos_crudos/vram_real_medida.csv`), dato que decidió el pivote E4B → E2B y deja margen para la visión residente (≈1,2 GB) y la caché KV; el modelo *fine-tuneado* finalmente desplegado consume 2,07 GB de VRAM efectiva (medición del despliegue del FT v2, `dataset_finetune/ESTADO_COMPLETO_2026-06-02.md`; el CSV cubre los modelos base, no el FT), con aún más holgura dentro de los 4 GB.
- **Responde con latencia tier-Alexa.** La latencia mediana por acción es ~2,2 s tras la optimización de caché de prefijo, holgadamente dentro del presupuesto de 4–5 s; las colas problemáticas (Spotify) se redujeron de 12,6 s a 5,6 s.
- **Garantiza honestidad estructural.** No declara haber ejecutado una acción si no puede verificarla contra el estado real del SO (registry, UIA, pycaw), mediante verificación tri-estado (`True`/`False`/`None`).
- **Ofrece modos de accesibilidad y control multilingüe.** Tres modos (`normal`, `no_vidente`, `movilidad`) y control por cámara/gestos (MediaPipe), con clasificación multilingüe por *embeddings* en lugar de listas de palabras por idioma.
- **Cuenta con una interfaz web propia ("Baxy field").** Renderizada por CPU (`--disable-gpu`) para no competir por la GPU, con retroalimentación visual del estado del asistente.

### 9.2. Evidencia de liberación por iteración

**Tabla 3.9.** *Evidencia de liberación del producto por iteración, contra su gate.*

| Iteración | Incremento liberado | Gate | Resultado medido |
|---|---|---|---|
| 1 — Núcleo | Agente responde por voz *offline* en 4 GB | VRAM ≤ 4 GB; router *holdout* | 3 371 MiB; router 0,9964 |
| 2 — Voz/percepción y acciones | *Wake-word* + STT/TTS + computer-use; casos de voz reales | *wake* `recall ≥ 0,60 ∧ fp/hr ≤ 1,0` (**cumplido**); FT | *wake* "Baxy" recall 0,872 en *held-out* universal (≥ 0,60), 1,00 en es/en/it/fr/pl/ru; 10/10 casos de voz PASS en vivo; 0 % herramientas inventadas; voz extremo a extremo operativa *offline* |
| 3 — Robustez/latencia/accesibilidad | Optimización, memoria de gustos, accesibilidad, *fallback* CPU | latencia ≤ 5 s; suite verde | p50 ~1,22 s; **2 740 tests verdes, 0 fallos** |

*Fuente: Elaboración propia (2026), a partir de `04_diseno_ejecucion_cierre.md`, `BACKLOG_MAESTRO.md` y `MEMORY.md`.*

### 9.3. Límites honestos del producto

En coherencia con el principio "medir, no celebrar", se declaran los límites reales del producto: el *tool-calling* del modelo de 2B se estima en ~75 % de exactitud de selección (frente al ~91 % del modelo E4B; estimación comparativa, no medición formal), *trade-off* aceptado por entrar en 4 GB; la visión se desactiva en modo CPU por costo; sobre GPU integradas Intel/AMD, la ejecución se cubre por el *fallback* a CPU operativo, mientras el binario CPU/Vulkan amplía esa cobertura; y el techo del *computer-use* está limitado por el estado del arte (benchmarks de referencia ~52,5 % en WindowsAgentArena). Estos límites se declaran con transparencia y no se ocultan.

---

## 10. Síntesis del Capítulo

El Capítulo III demuestra que el proyecto *Baxy* no es una propuesta teórica, sino un sistema construido, medido y operativo. Se seleccionó **Scrum** como metodología de gestión mediante una tabla ponderada (puntaje 2,85), coherente con la evidencia de sprints reales del repositorio, y una metodología de **desarrollo iterativo dirigido por medición** (*measure-then-ship*). Las **alternativas** de arquitectura y modelo se decidieron con datos de VRAM medidos (E2B-Q4_K_M es la única opción que entra en 4 GB), y las tres **factibilidades** —técnica (probada en hardware real), económica (costo de operación USD 0) y social (privacidad, accesibilidad, universalidad)— resultaron favorables. La **arquitectura** se documentó con las cuatro vistas del modelo 4+1 de Kruchten, verificadas contra el código. La **gestión** se apoyó en Git, un Backlog Maestro auditable y una suite de pruebas verde, y el **monitoreo** se ejerció mediante *gates* numéricos y una matriz de riesgos cuantificada (probabilidad × impacto) evaluada a priori en la planificación. Finalmente, la **evidencia del producto funcional** confirma que el sistema Baxy cumple sus criterios de éxito medibles: opera por voz, *offline*, dentro de 4 GB, con latencia tier-Alexa y honestidad estructural.

---

## Referencias

Kruchten, P. (1995). The 4+1 view model of architecture. *IEEE Software, 12*(6), 42–50. https://doi.org/10.1109/52.469759

Park, J. S., O'Brien, J. C., Cai, C. J., Morris, M. R., Liang, P., & Bernstein, M. S. (2023). *Generative agents: Interactive simulacra of human behavior*. arXiv. https://arxiv.org/abs/2304.03442

Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide: The definitive guide to Scrum: The rules of the game*. Scrum.org. https://scrumguides.org/

Villacura, E. (2026a). *BACKLOG_MAESTRO.md* [documento interno del proyecto Baxy]. Repositorio del proyecto.

Villacura, E. (2026b). *Baxy — Estado técnico y límites de hardware* [documento interno]. Repositorio del proyecto.

Villacura, E. (2026c). *vram_real_medida.csv* [conjunto de datos de mediciones]. Repositorio del proyecto.

Villacura, E. (2026d). *AUDITORIA_licencias.md* [auditoría de licencias de 511 paquetes, documento interno]. Repositorio del proyecto.

*Fuente: Elaboración propia (2026). Las cards de los modelos (Gemma 4, Whisper, LiveKit, Piper, MediaPipe), los benchmarks WindowsAgentArena/OSWorld y la legislación chilena citada se consolidan en la sección única **Referencias (general, APA 7)** del portafolio (y, equivalentemente, en la sección Referencias del Informe Final).*
