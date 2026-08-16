# Entregables — Tesis Seminario de Título (Baxy)

Generados a partir del proyecto real (este repo) + el material del curso (Chat WhatsApp
+ 7 PDFs + temario del Informe Final). Curso: **UNAB INSW410 Portafolio de Proyectos**.
Estudiante: Emmanuel Villacura Arancibia. Profesor guía: Nicolás Caselli. Año: 2026.

Todo dato técnico proviene de archivos **medidos** del repo (VRAM real, latencia,
tool-calling, suite de tests, auditoría de licencias), no inventado. Donde algo no estaba
100 % medido, se marcó explícitamente (honestidad). Las cifras y fuentes load-bearing del
bloque introductorio se verificaron contra la fuente original el 2026-06-03.

---

## 1. Documentos maestros (lo que se entrega y se defiende)

| Archivo | Qué es |
|---|---|
| **PORTAFOLIO_COMPLETO.md** | **El portafolio completo en un solo documento**: une las páginas preliminares (portada, declaración de uso de IA, resumen, abstract), la Tabla de Contenidos y los Índices de Tablas/Figuras, y los tres capítulos acumulativos **CAP I + CAP II + CAP III** con numeración UNAB coherente (1.x, 2.x, 3.x). Es la entrega del Portafolio de las tres sumativas. (~19.300 palabras.) |
| **INFORME_FINAL_COMPLETO.md** | El Informe Final en markdown, con la taxonomía del temario `.docx` UNAB (Introducción · Fundamentación · Alcance · Propuesta · Plan · Diseño Alto Nivel · Ejecución 3 iteraciones · Trabajos Futuros · Conclusiones · Bibliografía). Fuente del `.docx`. (~15.200 palabras.) |
| **INFORME_FINAL.docx** | El Informe Final en formato UNAB (carta, Arial 12, márgenes 2,5 cm, interlineado 1,5, justificado, estilos Heading 1/2/3 para TOC, tablas reales, arte ASCII en Courier). **Listo para revisar y entregar.** |
| **PRESENTACION_FINAL.pptx** | Presentación oral de la defensa (~10 min, 15 slides = 14 de contenido + 1 apéndice "No proyectar" con datos defensivos). Cada slide tiene nota del orador en el panel de notas. **Es el examen.** |

> **Portafolio vs. Informe Final:** son el **mismo contenido reorganizado**. El curso pide el
> *Portafolio* en 3 capítulos acumulativos (CAP I/II/III) → usar `PORTAFOLIO_COMPLETO.md`.
> El temario `.docx` usa su propia taxonomía → usar `INFORME_FINAL_COMPLETO.md` / `INFORME_FINAL.docx`.

---

## 2. Capítulos del Portafolio (fuente editable, ya unidos en PORTAFOLIO_COMPLETO.md)

| Archivo | Contenido |
|---|---|
| `portafolio_CAP_I.md` | **CAP I — Problema/Oportunidad y Planteamiento.** Descripción del problema, situación actual (7 pasos), **Diagrama de Ishikawa** (6M) + descripción de causas (en qué consiste/qué efecto/cómo incide; principal vs. secundaria; interno/externo), fundamentación, viabilidad en 5 dimensiones (legal/ambiental/económica/tecnológica/cultural) y estado del arte con tabla de homologación de 10+ competidores. (~5.960 palabras.) |
| `portafolio_CAP_II.md` | **CAP II — Objetivos, Métricas, Alcance, Normativas y Plan.** Objetivo general + 6 objetivos específicos SMART, tabla de justificación de objetivos con criterios de éxito y resultados medidos, alcances/limitaciones + homologación, normativas chilenas (21.719/19.628/21.459/21.663/17.336) + auditoría de licencias (511 paquetes), y plan inicial (Scrum ponderado, monitoreo, matriz de 12 riesgos, fases). (~4.770 palabras.) |
| `portafolio_CAP_III.md` | **CAP III — Metodología, Alternativas y Factibilidades, Plan, Arquitectura, Gestión, Monitoreo y Prototipo.** Scrum por tabla ponderada (2,85) + desarrollo gateado; alternativas A/B/C con VRAM medida + 3 factibilidades; requisitos RF/RNF; arquitectura 4+1 de Kruchten (4 vistas + diagrama de contexto); herramientas de gestión; monitoreo por gates + matriz de riesgos; evidencia del prototipo/MVP. (~6.930 palabras.) |

---

## 3. Páginas preliminares y bloques del Informe Final (fuente editable)

| Archivo | Contenido |
|---|---|
| `00_preliminares.md` | Portada, Declaración de Originalidad y Uso de IA, Resumen (≈300 palabras) + palabras clave, y Abstract. |
| `01_introduccion_fundamentacion.md` | Bloque 1 del Informe: introducción y fundamentación del problema. |
| `02_alcance_objetivos_factibilidad.md` | Bloque 2 del Informe: alcance, objetivos, factibilidades. |
| `03_propuesta_plan_proyecto.md` | Bloque 3 del Informe: propuesta de solución y plan de proyecto. |
| `04_diseno_ejecucion_cierre.md` | Bloque 4 del Informe: diseño de alto nivel, ejecución (3 iteraciones) y cierre. |
| `05_presentacion_oral.md` | Fuente de la presentación (14 slides + apéndice Q&A, bullets + notas del orador + marcas DEMO/CAPTURA). |

---

## 4. Guion de defensa oral

| Archivo | Contenido |
|---|---|
| `guion_defensa_oral.md` | **Guion de defensa oral (~10 min)** alineado a `PRESENTACION_FINAL.pptx` (15 slides). Por slide: texto exacto hablado (~40 s) + indicación escénica + transición. Incluye un **banco de 14 respuestas preparadas al jurado** (local vs. cloud, cómo entra en 4 GB, precisión del 2B, vendibilidad/licencias, honestidad tri-estado, Scrum ponderado, latencia, multilingüe, testeo en vivo, flash-attn OFF, sin NVIDIA/CPU-Vulkan, seguridad, mejora prioritaria) + apéndice de datos defensivos rápidos. (~5.570 palabras.) |

---

## 5. Artefactos de apoyo (para anexar, renderizar o defender)

| Archivo | Contenido |
|---|---|
| `artefactos_sueltos.md` | **4 artefactos standalone listos para anexar:** (1) tabla comparativa de competencia (Baxy vs. Alexa/Siri/Google + 4 categorías de agentes, 9 criterios); (2) análisis legal (leyes chilenas + auditoría de 511 paquetes, 385 OK / 8 GPL / 5 NC / 0 AGPL, único bloqueante piper-tts GPL + veredicto vendible); (3) matriz de 12 riesgos R1–R12 (prob./impacto/exposición/mitigación/estado); (4) dos técnicas de análisis del problema (Ishikawa 6M + Árbol de problemas con priorización Pareto). (~5.140 palabras.) |
| `diagramas_mermaid.md` | **8 figuras / 9 bloques Mermaid** que reemplazan el arte ASCII del Informe: Ishikawa (grafo + mindmap), diagrama de contexto (DFD-0/C4 "sin nube"), las 4 vistas 4+1 (Lógica/Despliegue/Física/Escenarios), y la metodología (Gantt + flujo gateado). Validados 9/9 con `mermaid.parse` v11. **Para la entrega: renderizar en mermaid.live → PNG/SVG y pegar en el .docx.** |
| `bibliografia_verificada.md` | Verificación web (2026-06-03) de todas las cifras load-bearing del bloque introductorio, con el estado de cada fuente (confirmada / corregida) y la bibliografía APA 7 limpia. Documenta las correcciones de autoría y de cifras aplicadas a CAP I. |
| `_revision_adversarial.md` | Revisión adversarial (rol jurado) cotejando los entregables contra el código y los datos reales del repo. Lista 7 hallazgos críticos + 10 menores, con lo verificado OK que no debe tocarse. Documento de trabajo interno (no se entrega al jurado). |

---

## 6. Scripts de conversión (re-ejecutables)

- `_tesis_curso/_to_docx.py` — markdown → `.docx` formato UNAB.
- `_tesis_curso/_to_pptx.py` — markdown → `.pptx`.

Re-ejecutables si se editan los `.md` fuente.

---

## ⚠️ Qué REVISAR antes de entregar (no se pudo automatizar)

1. **Capturas / evidencia visual:** el informe marca dónde van capturas de pantalla
   (demo de voz, la GUI funcionando). Agregarlas desde el MVP real.
2. **Diagramas:** renderizar los de `diagramas_mermaid.md` en mermaid.live (PNG/SVG) y
   reemplazar el arte ASCII en el `.docx` para una entrega pulida. El informe ya remite a
   cada figura por número.
3. **Entradas APA marcadas `[VERIFICAR]`:** la sección Referencias del informe está
   consolidada; las fuentes load-bearing fueron verificadas, pero las entradas con
   `[VERIFICAR]` (cards de modelos, WCAG, las 5 leyes chilenas) deben reconfirmarse contra
   la fuente primaria antes de imprimir. Ver `bibliografia_verificada.md`.
4. **Tabla de Contenidos del .docx:** abrir en Word → Referencias → Insertar Tabla de
   Contenidos (los títulos ya son estilos H1/H2/H3 jerárquicos, se auto-genera).
5. **Datos personales / firma:** completar la Declaración de Originalidad con la firma.
6. **Lectura final:** es tu tesis — leerla, ajustar el tono a tu voz y validar que cada
   afirmación represente lo que querés defender.

---

## Inventario rápido (todos los archivos de esta carpeta)

- **Documentos maestros:** `PORTAFOLIO_COMPLETO.md`, `INFORME_FINAL_COMPLETO.md`, `INFORME_FINAL.docx`, `PRESENTACION_FINAL.pptx`
- **Capítulos del Portafolio:** `portafolio_CAP_I.md`, `portafolio_CAP_II.md`, `portafolio_CAP_III.md`
- **Preliminares + bloques del Informe:** `00_preliminares.md`, `01_introduccion_fundamentacion.md`, `02_alcance_objetivos_factibilidad.md`, `03_propuesta_plan_proyecto.md`, `04_diseno_ejecucion_cierre.md`, `05_presentacion_oral.md`
- **Guion de defensa:** `guion_defensa_oral.md`
- **Artefactos de apoyo:** `artefactos_sueltos.md`, `diagramas_mermaid.md`, `bibliografia_verificada.md`, `_revision_adversarial.md`
- **README:** este archivo.

> Esta carpeta NO se versiona en git (trabajo académico personal, gitignored).
