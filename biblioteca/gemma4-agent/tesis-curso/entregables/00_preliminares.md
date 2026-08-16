<div align="center">

# UNIVERSIDAD ANDRÉS BELLO

## FACULTAD DE INGENIERÍA

### INGENIERÍA CIVIL INFORMÁTICA

<br><br><br>

# ASISTENTE DE VOZ DE ESCRITORIO LOCAL Y PRIVADO PARA WINDOWS EN HARDWARE MODESTO (≤ 4 GB DE VRAM) MEDIANTE UN MODELO GEMMA 4 E2B FINE-TUNEADO

<br><br>

*Proyecto de Título para optar al título de Ingeniero Civil en Informática*

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
> numeran en romanos minúsculos (i, ii, iii, …) y el cuerpo del informe en arábigos
> (1, 2, 3, …), en la esquina inferior derecha. Cada capítulo inicia en página nueva.
> Tamaño carta, fuente Arial o Times New Roman 12, interlineado 1,5, márgenes de 2,5 cm
> (inferior 3 cm) y texto justificado.

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
