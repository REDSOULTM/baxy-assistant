# Inventario de entregables (plan maestro de la tesis)

 "Tengo la estructura exacta del Informe Final (.docx), las VRAM medidas reales, competencia, arquitectura y lÃ­mites. Tengo todo lo necesario para producir el inventario consolidado y accionable. No necesito leer mÃ¡s: el material de las 7 clases ya estÃ¡ en el prompt, y el repo me dio los datos reales para llenar cada secciÃ³n.

Voy a entregar el inventario completo como mi respuesta final.

---

# INVENTARIO DE ENTREGABLES â Tesis "Carter / Gemma 4 Agent" (UNAB INSW410)

> **Fuente de verdad de datos reales** (para no inventar): repo en `C:\Users\emman\Desktop\ETC\Programacion\Probando Gemma 4`. Material clave ya disponible: `documentacion/00_producto/Gemma4_estado_y_limites_2026_05_29.md`, `documentacion/00_producto/ANALISIS_COMPETENCIA.md`, `documentacion/00_producto/AUDITORIA_licencias.md`, `documentacion/01_arquitectura/README.md` (+ `ARCHITECTURE.md`, `02_components`, `04_sequences`), `documentacion/datos_crudos/vram_real_medida.csv`, los 16 grÃ¡ficos PNG en `documentacion/graficos/`, y la memoria `MEMORY.md`. Estructura del Informe Final tomada literal del `.docx` temario.
> **Dato crÃ­tico de orientaciÃ³n**: hay DOS estructuras en juego. (1) El **Informe Final** (el `.docx` temario que pegaste) usa una taxonomÃ­a propia (IntroducciÃ³n â FundamentaciÃ³n â Alcance â Propuesta â Plan â DiseÃ±o Alto Nivel â EjecuciÃ³n 3 Iteraciones â Trabajos Futuros â Conclusiones). (2) El **Portafolio** de las 7 clases usa CAP I / CAP II / CAP III. **Son el mismo contenido reorganizado.** El inventario mapea cada pieza a AMBAS para que no se duplique trabajo. Genero todo una vez y lo coloco segÃºn el documento destino.

---

## A. RESUMEN EJECUTIVO DEL PLAN DE GENERACIÃN

Se generan **3 documentos maestros** + **artefactos embebidos** (diagramas, tablas, grÃ¡ficos):

1. **INFORME_FINAL.docx/.md** (documento principal, estructura del temario `.docx`) â el grueso del trabajo.
2. **PORTAFOLIO_CAP_I-II-III.docx/.md** (estructura UNAB de las clases; si el curso exige el formato portafolio ademÃ¡s del informe). En la prÃ¡ctica es un **reordenamiento** del Informe Final + PÃ¡ginas Preliminares.
3. **PRESENTACION_FINAL.pptx** (Clase 7, exposiciÃ³n oral 10 min).

**Buena noticia (medido en el repo):** ~70% del contenido tÃ©cnico ya existe escrito y medido en `documentacion/`. El trabajo NO es investigar de cero, es **redactar en formato acadÃ©mico UNAB + APA7** lo que ya estÃ¡ documentado con nÃºmeros reales.

---

## B. INVENTARIO POR ENTREGABLE

### BLOQUE 0 â PÃGINAS PRELIMINARES (Portafolio) / Portada (Informe)

| # | Entregable | SecciÃ³n destino | Contenido concreto (con quÃ© del proyecto se llena) | Formato | Prioridad |
|---|---|---|---|---|---|
| 0.1 | **Portada** | Preliminar (ambos) | TÃ­tulo en MAYÃSCULA, centrado, Arial 12, negrita, interlineado 1.5, **= objetivo general SIN verbo en infinitivo**. Ej: "ASISTENTE DE VOZ LOCAL Y PRIVADO PARA WINDOWS EN HARDWARE MODESTO (4 GB VRAM) MEDIANTE GEMMA 4 E2B FINE-TUNEADO". Autor: Emmanuel Villacura. Profesor guÃ­a: Nicolas Caselli. Santiago, Chile, 2026. | .docx | **P0** |
| 0.2 | **DeclaraciÃ³n de Originalidad y Uso de IA** | Preliminar | Tabla 3 columnas (Finalidad / DeclaraciÃ³n de uso / Herramienta+versiÃ³n). **Cargar HONESTO**: GeneraciÃ³n de cÃ³digo (Claude Code/Opus, Codex), RevisiÃ³n de redacciÃ³n, VerificaciÃ³n de datos. Ley NÂ°17.336. Firma. | .docx (tabla) | **P0** |
| 0.3 | Dedicatoria + Agradecimientos | Preliminar | Optativas, texto breve en cursiva. | .docx | P3 |
| 0.4 | **Tabla de Contenidos** | Preliminar | Auto-generada por Word desde estilos de tÃ­tulo. | .docx (TOC auto) | P1 (al final) |
| 0.5 | **Ãndice de Tablas + Ãndice de Figuras** | Preliminar | Auto. MÃ­nimo: Ishikawa, CANVAS, tabla homologaciÃ³n competencia, tabla objetivos+mÃ©tricas, matriz riesgos, las 4 vistas arquitectÃ³nicas, grÃ¡ficos VRAM/latencia. | .docx (auto) | P1 |
| 0.6 | **Resumen (250â300 palabras) + Palabras Clave (3)** | Preliminar | 3Âª persona, pasado, SIN refs, SIN resultados/conclusiones detallados. Cubrir: problema (asistentes de voz son cloud/privacidad-invasiva/pagos), objetivo (asistente local 4GB), metodologÃ­a (Scrum+...), resultados breves. Keywords: "asistente de voz local", "modelos de lenguaje en el borde (edge LLM)", "privacidad". | .docx | **P0** |
| 0.7 | **Abstract** | Preliminar | TraducciÃ³n literal al inglÃ©s del Resumen + "Key words". | .docx | P1 |

---

### BLOQUE 1 â INTRODUCCIÃN

| # | Entregable | SecciÃ³n Informe Final | SecciÃ³n Portafolio | Contenido concreto (datos del proyecto) | Formato | Prioridad |
|---|---|---|---|---|---|---|
| 1.1 | **IntroducciÃ³n** | `IntroducciÃ³n` | CAP I | PresentaciÃ³n macroâmicro: (macro) auge de asistentes de voz; (meso) son cloud, mandan audio a servidores, son pagos o requieren GPU grande; (micro) no hay asistente **local, privado, gratis, multilingÃ¼e que corra en 4 GB VRAM**. Importancia, objetivos a vuelo de pÃ¡jaro, mÃ©todos, y cierre describiendo cada capÃ­tulo. **SIN resultados ni conclusiones.** Fuente: `ANALISIS_COMPETENCIA.md` (ningÃºn competidor corre 4GB local con voz). | .md â .docx | **P0** |

---

### BLOQUE 2 â FUNDAMENTACIÃN DEL PROBLEMA (= CAP I/II Portafolio)

| # | Entregable | SecciÃ³n Informe Final | Portafolio | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|---|---|
| 2.1 | **Contexto** | `FundamentaciÃ³n > Contexto` | CAP I/II | Industria de asistentes (Alexa, Siri, Google). Datos citados (fuente+aÃ±o, APA): cuota de mercado de voz, tendencia edge-AI, preocupaciÃ³n por privacidad. **Requiere 3â5 fuentes web reales** (buscar y citar). | .md | **P0** |
| 2.2 | **SituaciÃ³n Actual** | `FundamentaciÃ³n > SituaciÃ³n Actual` | CAP II "Planteamiento" | El estado hoy: dependencia de la nube, latencia de red, costo de suscripciÃ³n, datos de voz en servidores ajenos, no funciona offline. Narrativa del proceso actual (7 pasos Clase 2). Demostrar **"destrucciÃ³n de valor"** (privacidad perdida, costo, dependencia). | .md | **P0** |
| 2.3 | **TÃ©cnica de IdentificaciÃ³n de la ProblemÃ¡tica** | `FundamentaciÃ³n > TÃ©cnica IdentificaciÃ³n` | CAP II (OBLIGATORIO) | **DIAGRAMA DE ISHIKAWA** (la tÃ©cnica #1, obligatoria). Cabeza = "No existe asistente de voz local, privado y gratis usable en hardware modesto". Espinas (las 6M adaptadas): **TecnologÃ­a** (LLMs grandes no caben en 4GB; visiÃ³n pesa 1.2GB), **Costo** (APIs cloud pagas, GPUs caras), **Privacidad** (audio a la nube), **Conectividad** (requiere internet), **Idioma** (competidores monolingÃ¼es/inglÃ©s-cÃ©ntrico), **MÃ©todo** (tool-calling difÃ­cil en modelos chicos ~75%). | **Diagrama** (draw.io/Mermaid â PNG) + texto | **P0** |
| 2.4 | **DescripciÃ³n de causas** | `FundamentaciÃ³n > DescripciÃ³n de causas` | CAP II | Por cada causa del Ishikawa: en quÃ© consiste / quÃ© efecto produce / cÃ³mo incide. + factores internos/externos + secuencia causal + datos/evidencia + causas principales vs secundarias (4 elementos Clase 3). Anclar en datos reales: VRAM medida (`vram_real_medida.csv`: E2B-Q4=3371 MiB delta entra; E4B-Q4=5087 MiB NO entra), tool-calling 75% vs 91%. | .md (tabla causa-efecto-incidencia) | **P0** |
| 2.5 | *(opcional)* **Ãrbol de Oportunidad** | FundamentaciÃ³n (alternativa/complemento) | CAP II | La tÃ©cnica #2. RaÃ­ces (causas) â tronco (oportunidad: "asistente edge privado") â copa (efectos positivos: privacidad, offline, gratis, accesibilidad). Ãtil para enmarcar como OPORTUNIDAD ademÃ¡s de problema. | Diagrama | P2 |

---

### BLOQUE 3 â ALCANCE DEL PROYECTO (= CAP II Portafolio)

| # | Entregable | SecciÃ³n Informe Final | Portafolio | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|---|---|
| 3.1 | **Objetivo General** | `Alcance > Objetivo General` | CAP II | Estructura TI = **negocio + herramienta tecnolÃ³gica**, verbo infinitivo, formato Verbo+Variable+Unidad+Contexto. Ej: "Desarrollar un asistente de voz local, privado y multilingÃ¼e que opere Ã­ntegramente en hardware modesto (â¤4 GB VRAM) mediante un modelo Gemma 4 E2B fine-tuneado, para usuarios de PC Windows sin depender de servicios en la nube." | .md | **P0** |
| 3.2 | **Objetivos EspecÃ­ficos (â¤4, SMART)** | `Alcance > Objetivos EspecÃ­ficos` | CAP II | **MÃ¡ximo 4**, verbo infinitivo, SMART. Propuestos (anclados en lo real): **(1)** Ejecutar el LLM+visiÃ³n+voz dentro de 4 GB VRAM. **(2)** Lograr tool-calling fiable (selecciÃ³n correcta de acciÃ³n) en un modelo de 2B. **(3)** Mantener latencia tier-Alexa por turno de voz. **(4)** Garantizar operaciÃ³n local/privada y honestidad estructural (no alucinar acciones). | .md | **P0** |
| 3.3 | **MÃ©tricas de Objetivos** â­ artefacto clave | `Alcance > MÃ©tricas` | CAP II (Tabla 3) | **TABLA "JustificaciÃ³n de objetivos"**: Objetivo especÃ­fico \| SituaciÃ³n actual \| Resultado esperado \| MÃ©trica \| **Criterio de Ã©xito (numÃ©rico)**. Una fila por objetivo. Criterios reales del repo: **VRAM â¤4 GB** (medido 3.36 GB E2B-Q4); **tool-calling: 0% tools inventadas en prod** (GATE pasado, memoria FT E2B v2) y ~75% accuracy; **latencia â¤4â5 s** (medido ~2.2 s/acciÃ³n); **wake-word recall â¥0.60 â§ fp/hr â¤1.0**. | **Tabla** .docx | **P0** |
| 3.4 | **Alternativas de SoluciÃ³n** | `Alcance > Alternativas de SoluciÃ³n` | CAP III (Propuesta de alternativas + factibilidades) | 3+ alternativas comparadas: (A) Asistente cloud (Alexa-like) â descartado por privacidad/costo; (B) LLM grande local (E4B/26B) â descartado, no entra en 4GB (datos `vram_real_medida.csv`); (C) **Gemma 4 E2B-FT local** â elegida. MÃ¡s alternativas internas medidas: Qwen3-4B vs Gemma (grÃ¡fico `04_gemma_vs_qwen_patrones.png`), Whisper vs Parakeet, Vulkan vs CUDA (`12_vulkan_vs_cuda.png`). | Tabla comparativa | **P1** |
| 3.5 | **Alcances y Limitaciones** | `Alcance > Alcances y Limitaciones` | CAP II | **LÃ­mites** (enmarcan problemÃ¡tica): SO Windows, usuario individual de escritorio, espaÃ±ol+multilingÃ¼e. **Alcances** (enmarcan soluciÃ³n): geogrÃ¡fico (cualquier mÃ¡quina Windows), temporal (MVP funcional), datos (todo local, 0 a la nube), **temÃ¡tica** (asistencia por voz + computer-use). **Limitaciones reales y honestas**: tool-calling 75% (no 91% del modelo grande); sin visiÃ³n en modo CPU; requiere 4 GB VRAM o fallback CPU lento (~12-20 tok/s); SOTA computer-use techo ~52% (WindowsAgentArena). | .md (2 sub-bloques) | **P0** |
| 3.6 | **Factibilidad TÃ©cnica** | `Alcance > Factibilidades > TÃ©cnicas` | CAP I/II (viabilidad) | Probada: corre HOY en 4 GB (medido). Stack OSS (`AUDITORIA_licencias.md`). Hardware target: GTX 1650/RTX 3050 4GB o fallback CPU. | .md | **P1** |
| 3.7 | **Factibilidad EconÃ³mica** | `Alcance > Factibilidades > EconÃ³micas` | CAP I/II | **Costo $0 de operaciÃ³n**: todo OSS/gratis, sin APIs pagas, sin suscripciÃ³n cloud, corre en hardware que el usuario ya tiene. Comparar vs costo recurrente de Alexa+/cloud APIs. Vendibilidad: stack permisivo VENDIBLE salvo 1 bloqueante (piper-tts GPL â fix subprocess). Fuente: `AUDITORIA_licencias.md`. | .md + tabla costos | **P1** |
| 3.8 | **Factibilidad Social / Cultural / Legal / Ambiental** | `Alcance > Factibilidades > Sociales` | CAP I/II | **Social**: privacidad (datos no salen del equipo), accesibilidad (control por voz/gestos cÃ¡mara para movilidad reducida, ver `05_vision_camara`), democratizaciÃ³n (corre en PC modesta), multilingÃ¼e/multi-acento (universal). **Legal**: Leyes chilenas â **21.719** (ProtecciÃ³n Datos Personales), **19.628** (vida privada), **21.459** (delitos informÃ¡ticos), **21.663** (Ciberseguridad); favorable porque NO transmite datos. **Ambiental**: menor huella vs datacenters cloud (cÃ³mputo local en hardware existente). | .md | **P1** |

---

### BLOQUE 4 â PROPUESTA DE SOLUCIÃN

| # | Entregable | SecciÃ³n Informe Final | Portafolio | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|---|---|
| 4.1 | **Propuesta de SoluciÃ³n (nivel macro)** | `Propuesta de SoluciÃ³n` | CAP II/III | DescripciÃ³n macro del asistente: pipeline vozâSTTâLLM(router+tools)âTTS, todo local. Modelo de la soluciÃ³n (front UI web + back llama-server). NO detalle de implementaciÃ³n. Fuente: `01_arquitectura/ARCHITECTURE.md`, `00_producto/computer_use_capability_layers.md`. | .md + diagrama esquema | **P0** |
| 4.2 | **Diagrama de Contexto** | `Propuesta > Diagrama de Contexto` | (Informe) | Diagrama de contexto (C4 nivel 1 / DFD nivel 0): el sistema Carter en el centro; actores externos = Usuario (voz/gestos), SO Windows (apps, registry, UIA), perifÃ©ricos (micrÃ³fono, cÃ¡mara, parlante). Sin nube. Fuente: `01_arquitectura/01_delta_system.md`. | **Diagrama** (draw.io/Mermaid) | **P0** |
| 4.3 | **CANVAS (Modelo de Negocio)** | (va en FundamentaciÃ³n/Alcance) | CAP II (obligatorio) | Plantilla 9 bloques numerada (Clase 3). Segmentos: usuarios privacy-conscious, hardware modesto, accesibilidad. Propuesta de valor: voz local/privada/gratis. Recursos clave: Gemma 4 OSS, MiniLM, llama.cpp. Costes: $0 operaciÃ³n (desarrollo = tiempo). Socios: comunidad OSS (Google/Gemma, llama.cpp, MediaPipe). | **Tabla CANVAS** | **P1** |

---

### BLOQUE 5 â PLAN DE PROYECTO (= CAP III Portafolio)

| # | Entregable | SecciÃ³n Informe Final | Portafolio | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|---|---|
| 5.1 | **MetodologÃ­a de GestiÃ³n** | `Plan > MetodologÃ­a de GestiÃ³n` | CAP III (obligatorio, justificada) | **Elegir vÃ­a tabla comparativa ponderada** (Clase 5, NO por opiniÃ³n). Candidatas: Scrum, Kanban, XP, Cascada (+ PMBOK). Escala pesos sumando 1 + semÃ¡foro verde/naranjo/rojo. Dado que el desarrollo real fue **iterativo por sprints** (la memoria estÃ¡ llena de "Sprint 7", "Sprint 1 R1-R8", sprints de router), **Scrum/iterativo gana**. Destacar la elegida. | **Tabla ponderada** | **P0** |
| 5.2 | **MetodologÃ­a de Desarrollo** | `Plan > MetodologÃ­a de Desarrollo` | CAP III | MetodologÃ­a tÃ©cnica: desarrollo dirigido por **mediciÃ³n/gates** (regla de oro del proyecto: nada se da por hecho sin un nÃºmero contra un gate). Cada feature: medir â implementar gateado (flag default-off) â validar en vivo â flippear. Citar CLAUDE.md como mÃ©todo. | .md | **P0** |
| 5.3 | **Plan de Monitoreo y Control** | `Plan > Plan de Monitoreo y Control` | CAP III | Marco Clase 6: 3 dimensiones (Integridad tÃ©cnica / Negocio / Calidad). **Check List del Progreso** (8 Ã­tems). **Reporte de Progreso** (5 componentes). Aterrizar en lo real: suite de tests verde como control de calidad (742â2190 tests), gates por subsistema, evals batch. | .md + plantilla checklist | **P1** |
| 5.4 | **Plan de GestiÃ³n de Riesgos + Detalle de Riesgos** â­ | `Plan > Plan de gestiÃ³n de Riesgos` | CAP III | **MATRIZ DE RIESGOS** 5 columnas (Riesgo \| DescripciÃ³n \| Probabilidad \| Impacto \| Estrategia de MitigaciÃ³n). Riesgos REALES del repo (oro puro): crash CUDA #22527 (FA-off); ONNX congela PC (throttle); VoxCPM segfault Ada (Piper); modelo no entra en VRAM (E4BâE2B pivote); tool-calling alucina (forced-retry + FT); piper-tts GPL bloquea venta (subprocess); STT en GPU robarÃ­a VRAM (CPU int8). | **Matriz** (tabla) | **P0** |
| 5.5 | **MÃ©todos y Materiales** | `Plan > MÃ©todos y Materiales` | CAP III | Stack/herramientas: Gemma 4 E2B-FT GGUF Q4_K_M, llama.cpp/llama-server, Whisper int8 CPU / Parakeet, LiveKit wake-word, Piper TTS, MediaPipe (cÃ¡mara), sentence-transformers MiniLM (router), React+pywebview (UI), Unsloth (fine-tune). Hardware: GPU 4GB / fallback CPU. Fuente: `documentacion/README.md` datos rÃ¡pidos. | Tabla materiales | **P1** |
| 5.6 | **PlanificaciÃ³n (Carta Gantt, 3 meses)** â­ | `Plan > PlanificaciÃ³n Proyecto` | CAP III (obligatorio, 3 meses) | **Carta Gantt en Excel/cronograma** horizonte **3 meses**, etapas de gestiÃ³n + desarrollo. Mapear a las 3 iteraciones reales. Reflejar sprints del historial git/memoria. | **.xlsx Gantt** (o imagen) | **P1** |

---

### BLOQUE 6 â DISEÃO DE ALTO NIVEL (4+1 Vistas de Kruchten)

| # | Entregable | SecciÃ³n Informe Final | Portafolio | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|---|---|
| 6.1 | **Vista LÃ³gica** | `DiseÃ±o Alto Nivel > Vista LÃ³gica` | CAP III | Componentes y su relaciÃ³n: agente (`agent.py`), router (`routing/planner.py`), tools (`domain_tools/`), voz, visiÃ³n, memoria/Jarvis, safety. Diagrama de componentes/clases. Fuente: `01_arquitectura/02_delta_components.md`, `03_delta_classes.md`, `ARCHITECTURE.md`. | **Diagrama UML** componentes | **P1** |
| 6.2 | **Vista FÃ­sica** | `DiseÃ±o Alto Nivel > Vista FÃ­sica` | CAP III | Mapeo a hardware: GPU (LLM+visiÃ³n 3.36GB), CPU (STT+TTS+UI render), RAM (KV+mmap), perifÃ©ricos. Diagrama de nodos. Fuente: `Gemma4_estado_y_limites` tabla de lÃ­mites. | **Diagrama** fÃ­sico | **P1** |
| 6.3 | **Vista de Despliegue** | `DiseÃ±o Alto Nivel > Vista de despliegue` | CAP III | CÃ³mo se instala/corre en la mÃ¡quina del usuario: proceso llama-server (CUDA/Vulkan/CPU), proceso UI (pywebview), venvs (3.10 runtime / 3.11 livekit / 3.12 finetune). Diagrama de despliegue. Fuente: `06_delta_dependencies.md`, CLAUDE.md (venvs). | **Diagrama** despliegue | **P1** |
| 6.4 | **Vista de Escenarios (Casos de Uso)** | `DiseÃ±o Alto Nivel > Vista Escenarios` | CAP III | Casos de uso clave: "abrÃ­ Spotify y ponÃ© rock", "mandale un WhatsApp a X", "instalÃ¡ DOOM", "subÃ­ el volumen", control por gestos. Diagramas de caso de uso + secuencia (la cascada UIAâOCRâvisiÃ³n). Fuente: `01_arquitectura/04_sequences/`, `MEMORY.md` (casos reales validados). | **Diagramas** UML CU + secuencia | **P1** |

---

### BLOQUE 7 â EJECUCIÃN DEL PROYECTO (3 Iteraciones) â­ EL BLOQUE MÃS PESADO

> Cada iteraciÃ³n (sprint) lleva **12 sub-secciones idÃ©nticas** (del `.docx`): DuraciÃ³n y Meta sprint / Funcionalidades / Historias de usuario / Puntos de historia comprometidos / DefiniciÃ³n sprint backlog / DiseÃ±o Detallado / Riesgos Gatillados / EjecuciÃ³n de Pruebas / Evidencia de Monitoreo y Control / Evidencia LiberaciÃ³n MVP / Problemas Abiertos. **Esto se llena con los sprints REALES del repo** (git log + `MEMORY.md` + `sprint_prompts/`).

| # | Entregable | SecciÃ³n | Contenido concreto (mapeo a sprints reales) | Formato | Prioridad |
|---|---|---|---|---|---|
| 7.1 | **IteraciÃ³n 1** | `EjecuciÃ³n > IteraciÃ³n 1` | **NÃºcleo: LLM local en 4GB + voz bÃ¡sica.** Historias: "como usuario quiero hablarle al PC sin internet". Funcionalidades: selecciÃ³n de modelo (Gemma E2B sobre E4B/26B, datos VRAM), STT/TTS en CPU, wake-word. Pruebas: gate VRAM â¤4GB, wake recallâ¥0.60. Evidencia MVP: el agente responde por voz offline. Riesgos gatillados: CUDA #22527, ONNX freeze. Fuente: `06_vram_estabilidad`, `03_voz_stt`, `MEMORY.md`. | .md + capturas/grÃ¡ficos | **P1** |
| 7.2 | **IteraciÃ³n 2** | `EjecuciÃ³n > IteraciÃ³n 2` | **Tool-calling + router + acciones (computer-use).** Historias: "quiero que ejecute apps/acciones por voz". Funcionalidades: router (encoder FT + abstain head), fine-tune E2B (0% tools inventadas), computer-use cascada UIAâOCRâvisiÃ³n, WhatsApp/Steam/Spotify. Pruebas: gate router 0.9964, FT 0% inventadas. Riesgos: tool-calling 75%, destinatario equivocado WhatsApp. Fuente: `02_router`, `04_computer_use`, `09_finetune`, `MEMORY.md` (muchas entradas). | .md + grÃ¡ficos | **P1** |
| 7.3 | **IteraciÃ³n 3** | `EjecuciÃ³n > IteraciÃ³n 3` | **Robustez, latencia, memoria/Jarvis, UI, accesibilidad.** Historias: "quiero que sea rÃ¡pido, recuerde mis gustos y sea usable manos-libres". Funcionalidades: optimizaciÃ³n latencia (prefix-cache ~1s/acciÃ³n, 2.2s), capa Jarvis (observador ambiente + perfil gustos), UI web Carter field, control por cÃ¡mara/gestos, fallback CPU. Pruebas: latencia â¤5s, suite 2190 verde. Fuente: `07_latencia`, `08_memoria_jarvis`, `05_vision_camara`. | .md + grÃ¡ficos | **P1** |

**Para las 3 iteraciones se reutilizan los 16 grÃ¡ficos PNG ya generados** (`documentacion/graficos/`): VRAM por modelo, latencia por categorÃ­a, progresiÃ³n v1âv14, Vulkan vs CUDA, causas raÃ­z de fails, etc. â van como "Evidencia" y figuras.

---

### BLOQUE 8 â CIERRE

| # | Entregable | SecciÃ³n | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|---|
| 8.1 | **Trabajos Futuros** | `Trabajos Futuros` | (= "Recomendaciones / mejoras versiÃ³n 2.0" de Clase 6). Del roadmap real: soporte sin GPU (binario CPU/Vulkan + auto-default), cliente MCP (B9), error-type recovery, memory-guided tool-picking, code-exec sandbox, E4B cuando haya >4GB. Fuente: `ANALISIS_COMPETENCIA.md` roadmap + `Gemma4_estado_y_limites` pendientes + `_backlog/BACKLOG_MAESTRO.md`. | .md | **P2** |
| 8.2 | **Conclusiones** | `Conclusiones` | **REGLA Clase 6: 1 conclusiÃ³n POR objetivo especÃ­fico (â¤4)** + 1 conclusiÃ³n acadÃ©mica/lecciones aprendidas. Cada conclusiÃ³n cierra contra su mÃ©trica/criterio de Ã©xito (cumplido/parcial). LecciÃ³n aprendida: el mÃ©todo medir-antes-de-celebrar. | .md | **P2** |
| 8.3 | **BibliografÃ­a (APA 7)** | `BibliografÃ­a` | Lista alfabÃ©tica APA v7. Fuentes: cards de modelos (Gemma, Whisper, LiveKit, Piper, MediaPipe), papers (Generative Agents para Jarvis, SMART), benchmarks (WindowsAgentArena, OSWorld), leyes chilenas, las fuentes web del contexto. El README dice "40+ fuentes" â ya hay precedente. **Cada capÃ­tulo ademÃ¡s cierra con su propia BibliografÃ­a** (regla Portafolio). | .docx | **P1** |
| 8.4 | **Anexos** | `Anexos` | Capturas del MVP funcionando, repositorio git, tablas extensas de mediciones (`datos_crudos/*.json`), cÃ³digo de scripts de mediciÃ³n, grÃ¡ficos en alta resoluciÃ³n. | .docx / adjuntos | P3 |

---

### BLOQUE 9 â PRESENTACIÃN ORAL (Clase 7, examen)

| # | Entregable | Contenido concreto | Formato | Prioridad |
|---|---|---|---|---|
| 9.1 | **PRESENTACION_FINAL.pptx** | Mazo **10 min**, plantilla **PPT UNAB**, tipografÃ­a Ãºnica (Arial/Calibri/Verdana) 18â24pt, lÃ¡minas numeradas. Estructura obligatoria (10 secciones): Planteamiento/ContextualizaciÃ³n â Problema/Oportunidad â Objetivo General â Objetivos EspecÃ­ficos con mÃ©tricas (tabla Objetivo\|R.Esperado\|MÃ©trica\|C.Ãxito) â Alcance/Limitaciones (4 dimensiones) â Propuesta macro (diagrama flujo + "esto es Carter" demo) â MetodologÃ­a/Materiales (tabla fases-herramientas-actividades) â Planes de gestiÃ³n/riesgos â Prototipo (la UI/demo en vivo) â Conclusiones/Recomendaciones (tabla objetivoâC.Ã©xito con checks). Marco teÃ³rico â¥3 autores/fuentes. Cronograma en Excel. Datos citados (fuente+aÃ±o). | **.pptx** | **P0** (es el examen, "debe aprobar") |

---

## C. TÃCNICAS ESPECÃFICAS QUE EL CURSO EXIGE APLICAR (y cÃ³mo usarlas)

> **AclaraciÃ³n de oro repetida en TODOS los PDFs**: el curso **NO pide "5 porquÃ©s" ni genÃ©ricamente "Ã¡rbol de problemas"**. Las tÃ©cnicas nombradas son las de abajo. No inventar otras.

1. **Diagrama de Ishikawa (espina de pescado)** â **OBLIGATORIO** (sello explÃ­cito Clase 3). TÃ©cnica de anÃ¡lisis causa-efecto del problema. CategorÃ­as = las "6M" (MÃ©todo, MÃ¡quina/TecnologÃ­a, Mano de obra, Medio ambiente, Materiales, MediciÃ³n), adaptadas al caso. Por cada causa describir: en quÃ© consiste / quÃ© efecto produce / cÃ³mo incide. â **Bloque 2.3**.
2. **Ãrbol de Oportunidad** â alternativa/complemento al Ishikawa (raÃ­ces=causas, tronco=oportunidad, copa=efectos). â **Bloque 2.5**.
3. **DescripciÃ³n de causas** (4 elementos): factores internos/externos, secuencia causal, datos/evidencias, principales vs secundarias. â **Bloque 2.4**.
4. **Mapa de EmpatÃ­a** (PIENSA/SIENTE, ESCUCHA, VE, DICE/HACE + frustra/motiva) â herramienta de Clase 3 para entender al usuario. Opcional pero suma.
5. **Modelo CANVAS** (9 bloques numerados 1â9) â **exigido en CAP II**. â **Bloque 4.3**.
6. **Objetivos SMART** + fÃ³rmula **Verbo + Variable + Unidad de anÃ¡lisis + Contexto**, verbos SIEMPRE en infinitivo. â **Bloque 3.1â3.2**.
7. **Tabla "JustificaciÃ³n de objetivos"**: Objetivo \| SituaciÃ³n actual \| Resultado esperado \| MÃ©trica \| **Criterio de Ã©xito (numÃ©rico obligatorio: %, tiempo, etc.)**. â **Bloque 3.3**.
8. **Tabla de HomologaciÃ³n / comparativa de soluciones** (filas=caracterÃ­sticas, columnas=competidores + "SoluciÃ³n Propuesta", celdas â/â) â para justificar el aporte frente al estado del arte. Llenar con los 10 competidores reales de `ANALISIS_COMPETENCIA.md`. â **Bloque 3.4**.
9. **SelecciÃ³n de MetodologÃ­a por tabla comparativa ponderada** (Clase 5): factores + pesos sumando 1.0 + escala 1-3 o semÃ¡foro **verde/naranjo/rojo**, promediar/ponderar, **destacar la elegida**. NO elegir por preferencia. â **Bloque 5.1**.
10. **Carta GANTT** (3 meses, en Excel) â etapas gestiÃ³n + desarrollo. â **Bloque 5.6**.
11. **Matriz de Riesgos** (5 columnas: Riesgo \| DescripciÃ³n \| Probabilidad \| Impacto \| Estrategia de MitigaciÃ³n). â **Bloque 5.4**.
12. **Vistas 4+1 de Kruchten** (LÃ³gica / FÃ­sica / Despliegue / Escenarios) para el DiseÃ±o de Alto Nivel del Informe Final. â **Bloque 6**.
13. **Plan de Monitoreo**: Check List del Progreso (8 Ã­tems) + Reporte de Progreso (5 componentes) + 3 dimensiones de control (tÃ©cnica/negocio/calidad). â **Bloque 5.3**.

---

## D. FORMATO Y REQUISITOS TRANSVERSALES (aplican a TODO el informe)

- **PÃ¡gina**: tamaÃ±o carta; **Arial o Times New Roman 12**; leyendas tablas/figuras tamaÃ±o 9; mÃ¡rgenes 2,5 cm (inferior 3 cm); **interlineado 1,5**; texto justificado.
- **PaginaciÃ³n**: romanos minÃºscula (preliminar) + arÃ¡bigos (texto), Ã¡ngulo inferior derecho. Cada capÃ­tulo en pÃ¡gina nueva. TÃ­tulos MAYÃSCULA+NEGRITA.
- **Figuras/tablas**: numeraciÃ³n correlativa independiente entre sÃ­, nÃºmero SOBRE el recuadro, **fuente con AÃO** en margen inferior ("Fuente: ElaboraciÃ³n propia (2026)").
- **APA 7** obligatorio en citas y referencias; bibliografÃ­a alfabÃ©tica; comunicaciones personales solo en texto.
- **Nomenclatura archivo PDF Ãºnico**: `Villacura_E_Asistente_de_voz_local_2026.pdf` (Apellido_Inicial_5palabrasTÃ­tulo_AÃ±o, Ã±ân, sin espacios).
- **Conteos duros**: Resumen 250â300 palabras + 3 keywords; â¤4 objetivos especÃ­ficos; Gantt 3 meses; 1 conclusiÃ³n por objetivo; exposiciÃ³n 10 min.

---

## E. ORDEN DE GENERACIÃN RECOMENDADO (ruta crÃ­tica)

**P0 (primero, son la columna vertebral y dependencias de todo lo demÃ¡s):**
1. Objetivo general + 4 especÃ­ficos SMART (3.1, 3.2) â **todo cuelga de aquÃ­**.
2. Tabla de mÃ©tricas/criterios de Ã©xito (3.3).
3. Ishikawa + descripciÃ³n de causas (2.3, 2.4).
4. IntroducciÃ³n + Contexto + SituaciÃ³n Actual (1.1, 2.1, 2.2).
5. Alcances/Limitaciones (3.5).
6. Propuesta + Diagrama de Contexto (4.1, 4.2).
7. MetodologÃ­a gestiÃ³n+desarrollo (5.1, 5.2) + Matriz de riesgos (5.4).
8. Portada + DeclaraciÃ³n IA + Resumen (0.1, 0.2, 0.6).
9. **PPTX presentaciÃ³n (9.1)** â es el examen.

**P1 (cuerpo y evidencia):** factibilidades (3.6â3.8), CANVAS (4.3), alternativas+homologaciÃ³n (3.4), Gantt (5.6), mÃ©todos/materiales (5.5), monitoreo (5.3), 4 vistas de diseÃ±o (6.1â6.4), 3 iteraciones (7.1â7.3), bibliografÃ­a (8.3), abstract+Ã­ndices (0.4â0.7).

**P2/P3 (cierre y pulido):** Trabajos Futuros (8.1), Conclusiones (8.2), Ã¡rbol de oportunidad (2.5), mapa empatÃ­a, anexos (8.4), dedicatoria/agradecimientos (0.3).

---

## F. ARTEFACTOS GRÃFICOS A PRODUCIR (lista cerrada)

Diagramas nuevos a dibujar (draw.io / Mermaid / PlantUML â exportar PNG): **Ishikawa** (2.3), **Ãrbol de oportunidad** (2.5), **Diagrama de Contexto** (4.2), **Esquema soluciÃ³n macro** (4.1), **CANVAS** (4.3), **4 vistas** LÃ³gica/FÃ­sica/Despliegue/Escenarios+secuencia (6.1â6.4). Tablas: **JustificaciÃ³n objetivos** (3.3), **HomologaciÃ³n competencia** (3.4), **Comparativa metodologÃ­as ponderada** (5.1), **Matriz riesgos** (5.4), **CANVAS** (4.3). **Reutilizar tal cual** los 16 PNG de `documentacion/graficos/` como evidencia en las iteraciones y factibilidades. Carta **Gantt** en Excel (5.6).

**Rutas reales para llenar contenido sin inventar:** `documentacion/00_producto/{Gemma4_estado_y_limites_2026_05_29.md, ANALISIS_COMPETENCIA.md, AUDITORIA_licencias.md}`, `documentacion/01_arquitectura/{ARCHITECTURE.md, README.md, 01_delta_system.md, 02_delta_components.md, 03_delta_classes.md, 04_sequences/, 06_delta_dependencies.md}`, `documentacion/datos_crudos/vram_real_medida.csv` (+ los `.json`), `documentacion/graficos/*.png`, `documentacion/_backlog/BACKLOG_MAESTRO.md`, `MEMORY.md`, y el git log para los sprints de las 3 iteraciones."
  }
