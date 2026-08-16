# Diagramas en Mermaid — Tesis Baxy

> Versión en **Mermaid** de los diagramas que en `INFORME_FINAL_COMPLETO.md` aparecen
> como arte ASCII. Mermaid se renderiza nativamente en GitHub, GitLab, VS Code
> (extensión Markdown Preview Mermaid), Obsidian, Typora y muchos editores Markdown.
> Para incrustarlos en el `.docx`: renderizar en <https://mermaid.live>, exportar a
> PNG/SVG y pegar en el lugar de cada figura.
>
> Fuente de cada diagrama: `_tesis_curso/entregables/INFORME_FINAL_COMPLETO.md`
> (bloques 01 introducción/fundamentación, 03 propuesta/plan, 04 diseño/ejecución).
> Elaboración propia (2026).

---

## Figura 1. Diagrama de Ishikawa (causa-efecto) de la problemática

Mermaid no posee un tipo *fishbone* nativo, por lo que el diagrama de Ishikawa se
representa con un **grafo de causas → problema** que conserva la estructura de espina
de pescado: el problema central (la "cabeza del pescado") a la derecha y las seis
categorías de causas (adaptación de las "6M") confluyendo hacia él. Se incluye además
una **versión en `mindmap`** equivalente para editores que prefieran ese formato.

### 1a. Versión grafo causa → efecto (recomendada)

```mermaid
flowchart LR
    %% Categorias de causas (6M) -> Problema central (cabeza del pescado)
    subgraph TEC["TECNOLOGIA / MAQUINA"]
        T1["LLMs grandes no caben en 4 GB de VRAM"]
        T2["Encoder de vision (mmproj) pesa ~1,2 GB"]
        T3["Hardware de usuario modesto (GPU 4 GB o sin GPU)"]
    end
    subgraph COS["COSTO"]
        C1["APIs cloud de pago"]
        C2["GPUs de alta gama caras"]
        C3["Suscripciones recurrentes"]
    end
    subgraph PRI["PRIVACIDAD"]
        P1["Audio enviado a la nube"]
        P2["Datos en servidores ajenos"]
        P3["Usuario pierde control de su informacion"]
    end
    subgraph CON["CONECTIVIDAD"]
        N1["Requiere internet permanente"]
        N2["Latencia de red por turno"]
        N3["Sin operacion offline"]
    end
    subgraph IDI["IDIOMA / USUARIO"]
        I1["Competidores monolingues o anglocentricos"]
        I2["Hardcodeo de idioma (p. ej. chino)"]
        I3["Excluye otros idiomas y acentos"]
    end
    subgraph MET["METODO"]
        M1["Tool-calling dificil en modelos chicos (~75% vs ~91%, estimado)"]
        M2["Modelos pueden 'alucinar' acciones"]
        M3["Sin medicion/gates se celebra sin verificar"]
    end

    TEC --> PROB
    COS --> PROB
    PRI --> PROB
    CON --> PROB
    IDI --> PROB
    MET --> PROB

    PROB["PROBLEMA:<br/>No existe un asistente de voz local, privado y gratuito<br/>usable en hardware modesto (&le; 4 GB VRAM)"]

    classDef cabeza fill:#b3001b,stroke:#5a000d,color:#ffffff,stroke-width:2px;
    classDef cat fill:#e8edf2,stroke:#34495e,color:#1c2833;
    class PROB cabeza;
    class TEC,COS,PRI,CON,IDI,MET cat;
```

### 1b. Versión `mindmap` equivalente

```mermaid
mindmap
  root((No existe asistente de voz<br/>local, privado y gratis<br/>en &le;4 GB VRAM))
    Tecnologia / Maquina
      LLMs grandes no caben en 4 GB
      Encoder de vision ~1,2 GB
      Hardware de usuario modesto
    Costo
      APIs cloud de pago
      GPUs de alta gama caras
      Suscripciones recurrentes
    Privacidad
      Audio enviado a la nube
      Datos en servidores ajenos
      Perdida de control del usuario
    Conectividad
      Requiere internet permanente
      Latencia de red por turno
      Sin operacion offline
    Idioma / Usuario
      Competidores monolingues
      Hardcodeo de idioma
      Excluye idiomas y acentos
    Metodo
      Tool-calling dificil en 2B (~75% vs ~91%, estimado)
      Riesgo de alucinar acciones
      Sin gates se celebra sin verificar
```

*Figura 1. Diagrama de Ishikawa: seis categorías de causas (6M) que originan la
problemática central. Fuente: Elaboración propia (2026), a partir de
`ANALISIS_COMPETENCIA.md`, `Gemma4_estado_y_limites_2026_05_29.md` y
`vram_real_medida.csv`.*

---

## Figura 2. Diagrama de Contexto (DFD nivel 0 / C4 nivel 1)

Sitúa al sistema **Baxy** en el centro y representa sus interacciones con los
actores y entidades externas. La característica determinante es la **ausencia total
de la nube**: ningún flujo cruza el perímetro del equipo del usuario hacia servicios
remotos.

```mermaid
flowchart TB
    subgraph PERIMETRO["PERIMETRO DEL EQUIPO DEL USUARIO — todo local, sin nube"]
        direction TB
        USUARIO(["USUARIO<br/>(operador)"])
        PERIF["PERIFERICOS<br/>Microfono · Camara<br/>Parlante · Pantalla"]
        BAXY["BAXY (Baxy)<br/>STT (CPU) &rarr; LLM Gemma 4 E2B-FT (GPU 4GB / CPU)<br/>&rarr; Router + ~67 Tools &rarr; Verificadores &rarr; TTS (CPU)"]
        SO["SISTEMA OPERATIVO WINDOWS<br/>Aplicaciones · Registry · Interfaz UIA/accesib.<br/>Audio (pycaw) / Brillo · Navegador / Mensajeria"]

        USUARIO -- "voz / wake-word" --> BAXY
        USUARIO -- "gestos (camara)" --> BAXY
        BAXY -- "respuesta hablada (TTS) + UI visual" --> USUARIO
        USUARIO -. usa .- PERIF

        BAXY -- "control (acciones)" --> SO
        SO -- "estado (verificacion)" --> BAXY
    end

    NUBE["NUBE / SERVICIOS REMOTOS"]
    PERIMETRO x-. "SIN CONEXION · SIN ENVIO DE DATOS" .-x NUBE

    classDef sistema fill:#1f6feb,stroke:#0a2e6b,color:#ffffff,stroke-width:2px;
    classDef actor fill:#fff3cd,stroke:#7a5b00,color:#1c2833;
    classDef ext fill:#e8edf2,stroke:#34495e,color:#1c2833;
    classDef prohibido fill:#f5c6c6,stroke:#b3001b,color:#5a000d,stroke-dasharray: 5 5;
    class BAXY sistema;
    class USUARIO actor;
    class SO,PERIF ext;
    class NUBE prohibido;
```

*Figura 2. Diagrama de Contexto: el Usuario interactúa con Baxy por voz/gestos y
recibe TTS + UI; Baxy actúa sobre el SO Windows y lee su estado para verificar. El
perímetro del equipo no es atravesado por ningún flujo hacia la nube. Fuente:
Elaboración propia (2026).*

---

## Figura 3. Vista Lógica (paquetes del agente y hot-path del turno)

Descomposición funcional del sistema en paquetes y el flujo del *hot-path* (camino de
ejecución de un comando del usuario), según el modelo de vistas "4+1" de
Kruchten (1995).

```mermaid
flowchart TB
    SURF["SUPERFICIES DE ENTRADA<br/>launcher · server (FastAPI) · CLI · MCP"]

    subgraph VOICE["voice (canal de audio siempre activo)"]
        direction TB
        AV["audio &rarr; vad &rarr; wake &rarr; pipeline (STT)"]
        TTS["TTS (Piper, CPU)"]
    end

    subgraph CORE["agent_core (orquestacion del turno)"]
        direction TB
        RC["run_content<br/>(_decide_turn / _execute_turn / _finalize_turn)"]
        RT["routing (encoder-FT + abstain head + Tool2Vec, cap 5)"]
        LLM["LLM Gemma 4 E2B-FT"]
        TOOLS["tools (~67) / computer_use (UIA &rarr; OCR &rarr; vision)"]
        VER["verificadores (tri-estado confirmed)"]
        RC --> RT --> LLM --> TOOLS --> VER
    end

    PERS["PERSISTENCIA<br/>state / experience / memoria-Jarvis"]
    DIAG["DIAGNOSTICOS<br/>tracing / telemetry"]
    PARL(["Parlantes"])

    SURF --> VOICE
    SURF --> CORE
    AV -- "comando (texto)" --> RC
    TTS --> PARL
    VER --> TTS
    CORE --> PERS
    CORE --> DIAG

    classDef inp fill:#fff3cd,stroke:#7a5b00,color:#1c2833;
    classDef pkg fill:#e8edf2,stroke:#34495e,color:#1c2833;
    classDef core fill:#1f6feb,stroke:#0a2e6b,color:#ffffff;
    class SURF inp;
    class PERS,DIAG,VOICE pkg;
    class RC,RT,LLM,TOOLS,VER core;
```

*Figura 3. Vista Lógica: paquetes del agente y flujo del hot-path. Fuente:
Elaboración propia (2026), a partir de `ARCHITECTURE.md`.*

---

## Figura 4. Vista de Despliegue (procesos y entornos en la máquina del usuario)

Cómo se instala y arranca el sistema como un conjunto de **procesos locales
coordinados**, sin servicios remotos. Tres procesos (UI, server, llama-server) y tres
*venvs* aislados por incompatibilidad de dependencias.

```mermaid
flowchart TB
    subgraph MAQ["Maquina Windows del usuario (nodo unico)"]
        direction TB
        UI["&laquo;process&raquo; UI<br/>pywebview / WebView2 (--disable-gpu)<br/>React / Vite"]
        SRV["&laquo;process&raquo; server.py<br/>FastAPI / uvicorn &rarr; Gemma4Agent (runtime)"]
        LS["&laquo;process&raquo; llama-server (llama.cpp)<br/>perfil {vram4 | cpu}"]
        GGUF[("GGUF Gemma 4 E2B-Q4<br/>flash-attn OFF · ctx 12288")]
        VENVS["venvs aislados:<br/>.venv (3.10 runtime) ·<br/>.venv_livekit (3.11 wake-word) ·<br/>venv 3.12 (fine-tuning)"]

        UI -- "HTTP / IPC" --> SRV
        SRV -- "HTTP :8080" --> LS
        LS --> GGUF
        SRV -. usa .- VENVS
    end

    NUBE["nube"]
    MAQ x-. "sin nodo en la nube — 0 transmision de datos" .-x NUBE

    classDef proc fill:#1f6feb,stroke:#0a2e6b,color:#ffffff;
    classDef store fill:#d5f5e3,stroke:#1e8449,color:#0b3d23;
    classDef env fill:#e8edf2,stroke:#34495e,color:#1c2833;
    classDef prohibido fill:#f5c6c6,stroke:#b3001b,color:#5a000d,stroke-dasharray: 5 5;
    class UI,SRV,LS proc;
    class GGUF store;
    class VENVS env;
    class NUBE prohibido;
```

*Figura 4. Vista de Despliegue: procesos y entornos. Fuente: Elaboración propia
(2026).*

---

## Figura 5. Vista Física (mapeo a recursos de hardware del nodo único)

Asignación de los componentes lógicos a los recursos de hardware de **un único nodo:
el computador del usuario**. Datos de VRAM medidos en `vram_real_medida.csv`.

```mermaid
flowchart TB
    subgraph PC["PC DEL USUARIO (nodo unico) — sin nodo en la nube, 0 transmision"]
        direction LR
        subgraph GPU["GPU 4 GB VRAM"]
            G1["Gemma 4 E2B-Q4 (3,36 GB)"]
            G2["+ vision mmproj (~1,2 GB, residente)"]
            G3["+ cache KV"]
        end
        subgraph CPU["CPU (0 VRAM)"]
            CP1["STT int8 · TTS Piper"]
            CP2["enrutador MiniLM · UI render"]
        end
        subgraph RAM["RAM (&ge; 8 GB)"]
            R1["modelo mmap + KV residente"]
        end
        PERIF["Perifericos:<br/>microfono · camara · parlante"]
    end

    classDef gpu fill:#1f6feb,stroke:#0a2e6b,color:#ffffff;
    classDef cpu fill:#27ae60,stroke:#145a32,color:#ffffff;
    classDef ram fill:#e67e22,stroke:#7e3d0b,color:#ffffff;
    classDef per fill:#e8edf2,stroke:#34495e,color:#1c2833;
    class G1,G2,G3 gpu;
    class CP1,CP2 cpu;
    class R1 ram;
    class PERIF per;
```

*Figura 5. Vista Física: reparto de recursos en el nodo único. E2B-Q4 (3 371 MiB)
entra en 4 GB; E4B-Q4 (5 087 MiB) NO entra. Fuente: Elaboración propia (2026), datos
de `vram_real_medida.csv`.*

---

## Figura 6. Vista de Escenarios (los tres casos de uso clave)

Concreta las vistas anteriores mediante casos de uso reales, **validados en vivo**
contra el agente y el LLM.

```mermaid
flowchart TB
    U(["Usuario (voz / gesto)"]) --> WAKE["wake"] --> STT["STT"] --> RUN["run_content"]

    RUN --> S1["Escenario 1: comando simple<br/>audio.set_volume"]
    RUN --> S2["Escenario 2: cadena / mision<br/>computer_use(goal)"]
    RUN --> S3["Escenario 3: accesibilidad<br/>vision_input &rarr; cu"]

    S1 --> VER["verificador (tri-estado)"]
    S2 --> VER
    S3 --> VER

    VER --> TTS["TTS Piper"] --> RESP(["respuesta hablada"])

    classDef io fill:#fff3cd,stroke:#7a5b00,color:#1c2833;
    classDef step fill:#e8edf2,stroke:#34495e,color:#1c2833;
    classDef esc fill:#1f6feb,stroke:#0a2e6b,color:#ffffff;
    classDef ver fill:#b3001b,stroke:#5a000d,color:#ffffff;
    class U,RESP io;
    class WAKE,STT,RUN,TTS step;
    class S1,S2,S3 esc;
    class VER ver;
```

*Figura 6. Vista de Escenarios: los tres casos de uso clave. Fuente: Elaboración
propia (2026), casos validados en `MEMORY.md`.*

---

## Figura 7. Metodología — fases y tres iteraciones (cronograma Gantt)

La planificación se organizó en un horizonte de tres meses: una **fase de
inicio/análisis**, **tres iteraciones de desarrollo incremental** y una **fase de
cierre**, en coherencia con la metodología iterativa-incremental (Scrum + Kanban).
Cada iteración cierra contra *gates* medidos.

```mermaid
gantt
    title Metodologia iterativa-incremental — Baxy
    dateFormat YYYY-MM-DD
    axisFormat %b
    todayMarker off

    section Fase 0 — Inicio/Analisis
    Problema, competencia, licencias, objetivos SMART, metodo measure-then-ship :done, f0, 2026-03-01, 14d

    section Iteracion 1 — Nucleo
    LLM local en 4 GB (E2B-Q4) + ~67 tools + enrutador :done, i1a, after f0, 20d
    STT/TTS en CPU + wake-word                          :done, i1b, after f0, 24d
    Gate: VRAM <=4 GB ; wake recall>=0,60 & fp/hr<=1,0  :milestone, m1, after i1b, 0d

    section Iteracion 2 — Tool-calling / acciones
    Router (encoder-FT + abstain) + computer-use (UIA/OCR/vision) :done, i2a, after m1, 20d
    Voz/vision/camara (vision_input) + robustez                  :done, i2b, after m1, 24d
    Gate: router 0,9964 ; 0% tools inventadas (prod)             :milestone, m2, after i2b, 0d

    section Iteracion 3 — Robustez/latencia/memoria/accesibilidad
    Fine-tuning E2B + optimizacion latencia (~2,2 s) :done, i3a, after m2, 20d
    Memoria Jarvis + UI Baxy field + fallback CPU   :done, i3b, after m2, 24d
    Gate: latencia <=5 s ; suite verde                :milestone, m3, after i3b, 0d

    section Fase de Cierre
    Backlog Maestro + suite 2740/0 + trabajos futuros + informe :active, fc, after m3, 14d
```

*Figura 7. Cronograma de las fases y tres iteraciones del desarrollo, con los gates
medidos al cierre de cada iteración. Las duraciones son ilustrativas del horizonte de
tres meses; las iteraciones agrupan los sprints reales del historial del repositorio.
Fuente: Elaboración propia (2026).*

---

## Figura 8. Metodología — flujo de las tres iteraciones y sus gates

Vista alternativa (grafo) que enfatiza el encadenamiento de iteraciones y el carácter
**gateado** del avance: ninguna iteración se da por cerrada sin pasar su gate medido.

```mermaid
flowchart LR
    F0["Fase 0<br/>Inicio / Analisis<br/>(problema, competencia,<br/>objetivos SMART, gates)"]
    I1["Iteracion 1<br/>Nucleo: LLM 4 GB +<br/>tools + router + voz basica"]
    G1{{"Gate 1<br/>VRAM <=4 GB ·<br/>wake recall>=0,60 & fp/hr<=1,0"}}
    I2["Iteracion 2<br/>Tool-calling, router y<br/>acciones (computer-use) + vision"]
    G2{{"Gate 2<br/>router 0,9964 ·<br/>0% tools inventadas (prod)"}}
    I3["Iteracion 3<br/>Robustez, latencia,<br/>memoria y accesibilidad"]
    G3{{"Gate 3<br/>latencia <=5 s ·<br/>suite verde"}}
    FC["Fase de Cierre<br/>Backlog Maestro · suite 2740/0 ·<br/>trabajos futuros · informe final"]

    F0 --> I1 --> G1 --> I2 --> G2 --> I3 --> G3 --> FC

    classDef fase fill:#e8edf2,stroke:#34495e,color:#1c2833;
    classDef iter fill:#1f6feb,stroke:#0a2e6b,color:#ffffff;
    classDef gate fill:#d5f5e3,stroke:#1e8449,color:#0b3d23;
    class F0,FC fase;
    class I1,I2,I3 iter;
    class G1,G2,G3 gate;
```

*Figura 8. Flujo gateado de las tres iteraciones: el avance solo procede al pasar el
gate medido de cada iteración. Fuente: Elaboración propia (2026).*

---

### Notas de uso

- **Renderizado:** pegar cada bloque ```` ```mermaid ```` en
  <https://mermaid.live> o previsualizar en VS Code / GitHub. Para el `.docx`,
  exportar a PNG/SVG y reemplazar el arte ASCII de cada figura.
- **Compatibilidad:** los tipos usados (`flowchart`, `mindmap`, `gantt`) están en
  Mermaid v9+. `mindmap` requiere v9.3+; si el visor es antiguo, usar la versión 1a
  (grafo) del Ishikawa.
- **Diagramas incluidos (8):** (1) Ishikawa — grafo + mindmap; (2) Contexto;
  (3) Vista Lógica; (4) Vista de Despliegue; (5) Vista Física; (6) Vista de
  Escenarios; (7) Metodología Gantt; (8) Metodología flujo gateado.
