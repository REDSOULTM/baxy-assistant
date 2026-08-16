# Revisión adversarial del INFORME_FINAL_COMPLETO.md

> Rol asumido: profesor jurado (Nicolás Caselli) evaluando una tesis de Ingeniería
> en Ingeniería Civil Informática. Lectura crítica buscando incoherencias internas,
> sobre-promesas, secciones débiles del temario UNAB y errores de formato académico.
> Contrastado contra `documentacion/00_producto/`, `documentacion/_backlog/BACKLOG_MAESTRO.md`,
> `documentacion/01_arquitectura/ARCHITECTURE.md`, `documentacion/datos_crudos/vram_real_medida.csv`
> y el código real del repo.
>
> Fecha: 2026-06-03. Documento revisado: `_tesis_curso/entregables/INFORME_FINAL_COMPLETO.md`
> (1674 líneas) + cotejo con `05_presentacion_oral.md` y `00_preliminares.md`.

---

## RESUMEN EJECUTIVO

El informe es **sólido, honesto y bien fundamentado en datos medidos** — la práctica
de "medir, no celebrar" se nota y eleva mucho la calidad frente a una tesis típica.
La mayoría de las cifras (VRAM 3.371 MiB, contexto 12.288, tabla de competencia,
licencias 511 paquetes, gates de accesibilidad, 2.740 tests) se **verificaron contra
el repo y son correctas**. No hay fraude ni invención.

Sin embargo, hay un puñado de **incoherencias numéricas reales** (sobre todo en
latencia y en VRAM del modelo desplegado vs. el base), **dos sobre-promesas puntuales**
y —lo más serio— **citas académicas con autores fabricados/cruzados y una cifra de
mercado sin respaldo** (detectado por la verificación web en `bibliografia_verificada.md`).
A ello se suman **vacíos del temario** (sin Bibliografía consolidada, citas sin entrada
APA, Carta Gantt ausente) y detalles de forma. Los 7 CRÍTICOS deben corregirse antes de
entregar: son exactamente el tipo de cosa que el jurado usa para preguntas incómodas, y
el de las citas roza la integridad académica.

---

## CRÍTICO (incoherencia / sobre-promesa — corregir SÍ o SÍ)

### C1. Latencia: tres cifras distintas presentadas sin distinguir bien qué mide cada una
**Dónde:** §3.3 (tabla OE3: "≈2,2 s por acción"); §5.3.a ("~2,2 s"); §5.5 Iteración 3
("p50 global medido ~1,22 s"); §7.3 Evidencia ("p50 global medido ~1,22 s"); §9
Conclusión 3 ("la latencia mediana por turno se redujo a ~1,22–2,2 s"); §5.3.c (replay:
"latencia mediana (p50) de 2,5 s"). El PPT (Slide 10) lo agrava: "~2,2 s/acción (p50
global ~1,22 s)".
**Problema:** se mezclan **tres números** (1,22 s, 2,2 s, 2,5 s) como si fueran lo
mismo. En realidad: 2,2 s = latencia por *acción* (tool-call, doc de estado);
1,22 s = p50 *global* del turno (BACKLOG); 2,5 s = p50 del replay de 1.071 mensajes.
Son métricas distintas pero el informe las usa intercambiablemente, y la Conclusión 3
("~1,22–2,2 s") parece un rango inventado. **Un jurado preguntará "¿en qué quedamos,
1,22 o 2,2?"** y la respuesta actual suena a maquillaje.
**Qué cambiar:** definir UNA vez, en §3.3 o §5.3, qué es cada métrica con su fuente, y
usarlas consistentemente: *"p50 global por turno ≈1,22 s; latencia por acción con
tool-call ≈2,2 s; p50 sobre el replay de 1.071 mensajes reales ≈2,5 s"*. En la
Conclusión 3 no poner un rango "1,22–2,2"; decir "p50 por turno ~1,22 s; las acciones
con herramienta ~2,2 s, ambas dentro del presupuesto de 4–5 s".

### C2. VRAM del modelo DESPLEGADO: 3,36 GB vs 2,07 GB — contradicción visible en la Conclusión 1
**Dónde:** Conclusión 1 (§9): *"Gemma 4 E2B-Q4_K_M ocupa 3,36 GB (3,37 GB con el modelo
fine-tuneado en 2,07 GB de VRAM efectiva)"*. También §7.3 ("E2B-FT Q4 ocupa 3,2 GB en
disco y **2,07 GB de VRAM**") y §3.6/§6.2 que usan 3,36 GB para el modelo en uso.
**Problema:** la frase de la Conclusión 1 es **literalmente contradictoria**: dice
"3,36 GB" y "2,07 GB de VRAM efectiva" del modelo fine-tuneado en la misma oración, y
mete un "3,37 GB" que no aparece en ninguna medición. El lector no sabe cuánto consume
el producto REAL que se entrega (el FT). El 3.371 MiB del CSV es la **medición del E2B
base** (`vram_real_medida.csv`), mientras que el modelo realmente desplegado es el FT,
que la memoria del proyecto mide en **2,07 GB**. El informe usa 3,36 GB como "lo
medido" en toda la factibilidad y vistas, pero el artefacto desplegado consume menos.
**Qué cambiar:** separar explícitamente: (a) "el barrido de VRAM se hizo sobre los
modelos **base** de la familia Gemma 4 → E2B-Q4_K_M base = 3.371 MiB (3,36 GB); este
fue el dato que decidió el pivote E4B→E2B"; (b) "el modelo **fine-tuneado** finalmente
desplegado consume 2,07 GB de VRAM, con aún más holgura dentro de 4 GB". Borrar el
"(3,37 GB ...)" de la Conclusión 1, que es la oración más confusa del informe.

### C3. Sobre-promesa: voz/wake-word presentado como cerrado, pero PRODUCTION_READY.md lo tiene TODO en rojo
**Dónde:** §7.2 ("Gate de wake-word: recall ≥ 0.60 ∧ fp/hr ≤ 1.0 ... corregido"),
§5.5 Iteración 1 ("Gates: ... wake-word recall ≥ 0,60 ∧ fp/hr ≤ 1,0"), §5.3.a ("y el
ya citado gate de wake-word"), Slide 6 (pipeline de voz como capacidad lograda).
**Problema:** el documento de aceptación oficial del propio repo,
`documentacion/00_producto/PRODUCTION_READY.md`, marca los **5 thresholds de voz en
❌**: wake recall 25 % (no 70 %), FP rate sin medir, STT WER ~0,57, STT latency sin
medición válida, y voice end-to-end p95 **sin medición**. Es decir, el subsistema de
**voz extremo a extremo NO está medido contra su gate**. El informe es honesto a medias:
en §3.3 (nota metodológica) sí admite que *"la medición sobre el audio de referencia
definitivo figura como pendiente"*, pero el resto del texto trata el wake-word y la voz
como entregados/gateados-en-verde. **Riesgo alto:** si el jurado pide la demo de voz y
falla, o pregunta por el recall real del wake-word, el "recall ≥0,60 cumplido" se cae.
**Qué cambiar:** alinear todo el informe con la honestidad de §3.3. En §7.1/§7.2 y §5.5
decir explícitamente que el gate de wake-word **está definido pero su medición final
sobre el audio de referencia v2 está pendiente** (estado real: PRODUCTION_READY = rojo);
presentar la voz como "implementada y funcional en uso, con la aceptación cuantitativa
formal pendiente del audio de referencia". No afirmar "recall ≥0,60 cumplido". Esto es
lo MÁS importante de toda la revisión: es la única afirmación que el repo **contradice
de frente**.

### C4. "62 herramientas" vs. el conteo real (61 tras eliminar smart_home; el schema tiene 67 nombres)
**Dónde:** Resumen, Abstract, §1, §3.5, §4.1, §6.1.e, §7.1, varias slides ("~62
herramientas" / "approximately sixty-two tools").
**Problema:** el `tools_pkg/tool_schemas.py` real tiene **67 nombres de tool** de
nivel superior; y la propia BACKLOG_MAESTRO documenta que al **eliminar `smart_home`**
quedaron **"tools(61)"**. O sea: 61 expuestas (sin contar internas como verify/safety/
session/mcp/subagent/skill_load) o 67 si se cuentan todas. El número "62" no coincide
con ninguno de los dos conteos verificables hoy. El hedge "aproximadamente" lo salva
parcialmente, pero un jurado que abra el código verá 61 o 67, no 62.
**Qué cambiar:** o bien decir "más de 60 herramientas" (vago pero seguro), o fijar el
número real verificado HOY (contar las expuestas al modelo y citar `tool_schemas.py`).
Recomendado: "alrededor de 60 herramientas de dominio (61 expuestas al modelo tras
retirar `smart_home`)". Mantener consistencia entre informe, abstract y PPT.

### C5. Inconsistencia informe ↔ presentación: 6 objetivos vs. 5 objetivos
**Dónde:** Informe §3.2 lista **6 objetivos específicos** (OE1–OE6) y §9 da "6/6";
PPT Slide 4 lista **5 objetivos específicos** (fusiona honestidad dentro del #4) pero
Slide 10 y Slide 14 vuelven a hablar de **OE1–OE6 / "6/6 objetivos"**.
**Problema:** la presentación se contradice a sí misma (5 en slide 4, 6 en slides 10 y
14) y con el informe. Si el jurado proyecta el slide 4 y luego oye "6/6 cumplidos", la
inconsistencia es visible en vivo.
**Qué cambiar:** dejar **6 objetivos** en todos lados (informe ya tiene 6; el PPT debe
listar los 6 en el slide 4, separando honestidad/privacidad como el informe). Editar
`05_presentacion_oral.md` Slide 4 y regenerar el .pptx.

### C6b. CITAS ACADÉMICAS CON AUTORES FABRICADOS / INCORRECTOS + cifra del 67 % sin respaldo
**Dónde:** §1 e §2 (Contexto) y la sección Referencias del bloque de Introducción.
**Problema (integridad académica — el más grave junto con C3):** una verificación web
independiente (ver `bibliografia_verificada.md`, 2026-06-03) encontró que:
- La cita **"Singh, S., Sarkar, S., Das, A., Saha, S., & Saha, S. (2024)"** para el
  arXiv 2410.11845 tiene **autores INVENTADOS**: el paper real es de **Zheng, Y., Chen,
  Y., Qian, B., Shi, X., Shu, Y., & Chen, J. (2024)**. No existe ningún review de edge
  LLMs con esos autores "Singh et al.".
- La cita **"Zheng, Z., Wang, Y., … (2025)"** para el TST 10.26599/TST.2025.9010166
  tiene **autores y año incorrectos**: el artículo real es de **Cai, G. et al. (2026),
  vol. 31(3)**. Hubo un **cruce de citas** (el apellido "Zheng" pertenece al OTRO paper).
- La afirmación *"Gemini Nano y Apple Intelligence ejecutan modelos localmente (Singh et
  al., 2024)"* **no está respaldada por esa fuente** (el paper no nombra esos productos).
- La cifra *"más del 67 % de los usuarios de smartphone usan voz al menos 1×/mes (Astute
  Analytica, 2026)"* **NO aparece en la fuente citada** (la fuente da 88,1 % de
  penetración mensual en EE. UU. y 60 % de uso semanal, no un 67 % global).
- Menor relacionado: el ~60 % de privacidad se atribuye en un punto a Astute Analytica y
  en otro a Secure Data Recovery; el dato es de **Secure Data Recovery (2024)** (= 58 %).
**Por qué es CRÍTICO y no menor:** citar autores que no escribieron el paper es,
formalmente, **fabricación de referencias** — exactamente lo que un jurado penaliza con
dureza en una tesis, y contradice la propia declaración de uso de IA ("las fuentes fueron
verificadas por el autor"). Si el jurado abre uno de esos arXiv, los autores no coinciden.
**Qué cambiar:** aplicar los 5 cambios concretos ya listados en `bibliografia_verificada.md`
(corregir Singh→Zheng et al. 2024; Zheng→Cai et al. 2026; reasignar Gemini Nano/Apple a
fuente primaria o quitar; corregir/eliminar el 67 %; unificar atribución del 58 % a Secure
Data Recovery). La bibliografía limpia ya está redactada en ese archivo — solo falta
aplicarla al cuerpo y a Referencias.

### C6. "2.07 GB de VRAM" no está en el set de datos crudos citado; procede solo de MEMORY
**Dónde:** §7.3 y §9 Conclusión 1 ("2,07 GB de VRAM").
**Problema:** todas las cifras de VRAM del informe se anclan a `vram_real_medida.csv`,
pero ese CSV **no contiene la medición del modelo fine-tuneado** (solo modelos base).
El 2,07 GB viene de la memoria del proyecto (MEMORY: "E2B-FT Q4 ... VRAM 2.07GB"), no
del CSV citado como fuente. Si el jurado pide ver el dato en `vram_real_medida.csv`, no
está ahí.
**Qué cambiar:** citar la fuente correcta para el 2,07 GB (la medición del FT v2,
`dataset_finetune/ESTADO_COMPLETO_2026-06-02.md` o el log de despliegue del FT), no el
CSV de modelos base. O bien generar/registrar el dato del FT en `datos_crudos/` para
que la trazabilidad cierre.

---

## MENOR (estilo, forma académica, completitud del temario)

### M1. Bibliografía NO consolidada — el temario UNAB pide una sección única de Referencias
**Dónde:** el índice promete "Referencias (APA 7)" al cierre, pero las referencias
están **fragmentadas en 3 bloques** (fin del Cap. 2, fin de Cap. 4–5, nota del Cap. 9)
y una nota explícita admite que *"se recomienda fusionarlas"*. Para una entrega final,
dejar la fusión "como recomendación pendiente" es débil.
**Qué cambiar:** crear UNA sección **Referencias** alfabética al final, en APA 7, con
TODAS las fuentes. Es trabajo mecánico pero el jurado lo nota de inmediato.

### M2. Citas en el cuerpo sin entrada bibliográfica completa (Kruchten, Park et al., benchmarks, leyes, model cards)
**(Relacionado con C6b, pero esto es solo el faltante de ENTRADAS, no errores de autoría.)**
**Dónde:** se cita "Kruchten (1995)", "Park et al. (2023)", "WindowsAgentArena/OSWorld",
"UFO2 + GPT-4o ≈28 %", las leyes chilenas (21.719/19.628/21.459/21.663/17.336) y las
"cards de modelos (Gemma 4, Whisper, LiveKit, Piper, MediaPipe)" **en el texto**, pero
**ninguna tiene entrada APA completa** en las secciones de referencias actuales (solo
una nota dice que "se consolidan en la Bibliografía del Informe Final", que no existe).
**Qué cambiar:** agregar las entradas APA reales de: Kruchten (1995, IEEE Software),
Park et al. (2023, "Generative Agents", arXiv), el paper/leaderboard de WindowsAgentArena
y de UFO2, las leyes chilenas (con su cuerpo legal), y las model cards. **Verificar
autores/años de cada una antes de pegarlas** (ver C6b: dos citas ya tenían autores mal).
Sin esto, hay citas "huérfanas" — falta de respaldo formal aunque las fuentes existan.

### M3. Falta Carta Gantt / cronograma temporal explícito en el Plan de Proyecto
**Dónde:** §5.5 "Planificación" describe Fase 0 + 3 iteraciones + Cierre en "tres
meses", pero **no hay Carta Gantt ni cronograma con fechas/duraciones**. El temario de
"Plan de Proyecto" de un Seminario de Título normalmente exige planificación temporal
visible (Gantt o tabla de hitos con fechas).
**Qué cambiar:** agregar una tabla/figura de cronograma (aunque sea aproximada, con las
fechas reales del historial git: la memoria tiene fechas de 2026-05-26 a 2026-06-02 por
sprint). Ata bien con la afirmación de "horizonte de tres meses".

### M4. Modelo "4+1 de Kruchten" mal mapeado: se presentan 4 vistas pero se omite la de Desarrollo
**Dónde:** §6 dice usar "el modelo 4+1 de Kruchten" y presenta Lógica, Física,
Despliegue y Escenarios.
**Problema técnico/académico:** el 4+1 canónico tiene **Lógica, Proceso, Física,
Desarrollo (+1 Escenarios)**. El informe sustituye "Proceso" y "Desarrollo" por
"Física" y "Despliegue", lo cual es una adaptación legítima pero **no es el 4+1
estándar**, y un jurado que conozca Kruchten lo notará. Además §6 (intro) dice "vistas
... —Lógica, Física (Procesos/Despliegue) y de Escenarios—" mezclando los nombres.
**Qué cambiar:** o bien (a) declarar explícitamente "adaptación del 4+1 de Kruchten:
las vistas de Proceso y Desarrollo se condensan en Física y Despliegue dado el nodo
único", o (b) renombrar para acercarse al canon (Lógica, Proceso, Física/Despliegue,
Escenarios). Reconocer la adaptación blinda contra la objeción.

### M5. "Capítulos 4 y 5" en el cuerpo arrancan con numeración propia (## 4. / ## 5.) pero el Cap. 3 viene de otro archivo con "# 3."
**Dónde:** el documento consolida 5 archivos fuente. Cap. 3 abre como "# 3. ALCANCE",
Cap. 4–5 abren como "## 4." y "## 5." (un nivel distinto), y los Cap. 6–9 como "## 6."
etc. Hay **inconsistencia de nivel de encabezado** (# vs ##) entre bloques pegados.
**Qué cambiar:** homogeneizar niveles de Heading para que el TOC auto-generado del
.docx salga limpio y jerárquico. (Cap. = Heading 1; subsecciones = Heading 2/3.)

### M6. Restos de marcas de trabajo interno visibles en el documento "final"
**Dónde:** nota de cabecera "revisar fuentes APA marcadas `[CITAR]`"; comentarios HTML
`<!-- ===== FUENTE: 0X_*.md ===== -->`; marcas `[DEMO]`/`[CAPTURA]` (en el PPT);
referencias a archivos internos del repo (`ANALISIS_COMPETENCIA.md`, `MEMORY.md`,
`vram_real_medida.csv`, `agent.py:3515`) citadas como si fueran fuentes académicas.
**Problema:** citar **documentos internos del repo y rutas de código** como fuentes
("documentación interna del proyecto, `ANALISIS_COMPETENCIA.md`, 2026") es aceptable
como evidencia propia, pero **abusar de ello** da aspecto de borrador. Y los comentarios
HTML / marcas de andamiaje no deben quedar en el PDF final.
**Qué cambiar:** (a) eliminar comentarios HTML de andamiaje y la nota `[CITAR]` de
cabecera; (b) mover las referencias a archivos del repo a notas al pie o a un "Anexo:
Evidencia y trazabilidad" en vez de intercalarlas como citas; (c) la cita "Villacura,
E. (2026a) BACKLOG_MAESTRO" en APA es correcta — usar ese estilo para los demás
internos, no la ruta cruda.

### M7. Tono: mayormente 3ra persona correcta, pero hay deslices de registro coloquial
**Dónde:** los **ejemplos de comandos** usan voseo/coloquial ("subí el volumen", "abrí
Spotify y poné rock", "instalá DOOM", "escribí X en la app Y", "actuá vos"). Es
correcto citarlos como input del usuario, pero conviene que estén siempre **entre
comillas como ejemplos**, nunca en la prosa expositiva. La prosa en sí está bien en 3ra
persona. El PPT, en cambio, usa "yo" en las notas del orador, lo cual es apropiado para
un guion oral.
**Qué cambiar:** verificar que todo voseo aparezca entrecomillado como ejemplo de
comando del usuario, no en la redacción. (Está casi siempre bien; revisar §6.4 y §7.)

### M8. "≈75 % de accuracy" presentado como dato medido sin fuente de la medición exacta
**Dónde:** OE2 (§3.3), §3.4.1, §3.5.2, R4 (§5.4), §7.1, Conclusión 2: "~75 % (E2B) vs
~91 % (E4B)".
**Problema:** el 75 %/91 % aparece como número duro en toda la tesis y en el Ishikawa
("~75 % vs ~91 %"), pero su **procedencia no está anclada** a un archivo de medición
como sí lo está la VRAM. El `Gemma4_estado_y_limites` lo cita pero como afirmación, no
con el harness/dataset que lo midió. Riesgo: el jurado pregunta "¿sobre qué set se midió
ese 75 %?" y no hay un CSV equivalente al de VRAM.
**Qué cambiar:** o anclar el 75 %/91 % a su evaluación reproducible (qué dataset, qué
script, cuántos casos), o suavizarlo a "estimado" si no hay una medición tan formal como
la de VRAM. Coherencia con el principio "medir, no celebrar".

### M9. Afirmación "21 combinaciones" — verificada OK, pero conviene blindarla
**Dónde:** Conclusión 1 y PPT Slide 10: "medir 21 combinaciones de modelo/cuantización".
**Estado:** **CORRECTO** — `vram_real_medida.csv` tiene exactamente 21 filas. Se deja
constancia de que este dato sí cierra; no requiere cambio, solo no tocarlo.

### M10. Resumen ligeramente por encima de lo habitual y palabras clave escasas
**Dónde:** Resumen = 298 palabras; Abstract = 231; palabras clave = 3.
**Problema menor:** muchos formatos UNAB piden resumen ≤250–300 palabras (298 está al
límite) y **al menos 4–5 palabras clave**. Tres es pocas.
**Qué cambiar:** opcional, recortar el resumen a ~250 y subir a 5 palabras clave (p. ej.
añadir "Gemma 4 / edge LLM", "tool-calling", "computer-use", "accesibilidad").

---

## VERIFICACIONES QUE SÍ CIERRAN (no cambiar — defendibles)

Para que el estudiante sepa dónde está firme:

- **VRAM E2B-Q4_K_M = 3.371 MiB** y E4B-Q4_K_M = 5.087 MiB, E4B-UD-IQ2_M = 4.057 MiB,
  26B/31B > 12.000 MiB → **exacto** contra `vram_real_medida.csv`.
- **Contexto 12.288 tokens** → **exacto**: `infra/profiles.py:185` (vram4) usa 12288
  con comentario "no 16384, ahorra ~168 MB KV". (El 16384 del repo es del perfil
  `standby`, no del runtime; no hay incoherencia 12288/16384 en el informe.)
- **2.740 tests, 0 fallos** → coincide con BACKLOG_MAESTRO (cierre 2026-06-02).
- **Router held-out ES 0,9964** → coincide con BACKLOG y MEMORY.
- **511 paquetes / único bloqueante GPL = piper-tts** → exacto contra `AUDITORIA_licencias.md`.
- **Gates accesibilidad G1 10/10 0 FP, G2 recall 100 % 0 FP** → exacto contra
  `modos_accesibilidad.md` (con la limitación honesta de "verificación física pendiente",
  que el informe sí declara).
- **Tabla de competencia (10 competidores, Mark-XXXIX = Gemini cloud pago)** → exacto
  contra `ANALISIS_COMPETENCIA.md`.
- **27 bugs en los bordes, auditoría rondas 1–13** → exacto contra `ARCHITECTURE.md`.
- **run_content 2193→22 LOC, flash-attn OFF 37/37, MAX_SELECTED_TOOLS=5, forced-retry
  cap 128, smart_home eliminado** → todos respaldados por MEMORY/BACKLOG.
- **0 % herramientas inventadas en producción (con array de tools)** y la nota de que sin
  el array da 47 % (artefacto) → exacto contra MEMORY (FT E2B v2).

---

## ORDEN DE ATAQUE RECOMENDADO

1. **C6b** (citas con autores fabricados + 67 % sin respaldo) — integridad académica; la
   corrección ya está redactada en `bibliografia_verificada.md`, solo falta aplicarla.
2. **C3** (voz/wake-word vs PRODUCTION_READY rojo) — el único choque frontal con el código.
3. **C2 + C6** (VRAM 3,36 base vs 2,07 del FT) — la oración de la Conclusión 1 es la más floja.
4. **C1** (latencia 1,22/2,2/2,5) — definir cada métrica una vez.
5. **C5** (5 vs 6 objetivos informe↔PPT) — edición rápida, pero visible en vivo.
6. **C4** (conteo de tools) — fijar número o usar "más de 60".
7. **M1 + M2** (Bibliografía consolidada + entradas APA huérfanas, verificando autores) —
   trabajo mecánico, alto impacto en la nota de forma.
8. **M3** (Carta Gantt) y **M4** (4+1 de Kruchten) — completitud del temario.
9. Resto de MENORES (M5–M10) — pulido final antes de imprimir.

> Nota: este informe ya tiene dos entregables hermanos de apoyo —
> `bibliografia_verificada.md` (resuelve C6b/M2) y `diagramas_mermaid.md` (reemplaza el
> arte ASCII por figuras renderizables, atacando parte de M6/legibilidad). Conviene
> integrarlos al INFORME_FINAL antes de la conversión final a .docx.
