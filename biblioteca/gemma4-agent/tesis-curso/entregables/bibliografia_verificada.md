# Bibliografía verificada — Informe Final (Baxy)

> Verificación de las fuentes y cifras *load-bearing* del bloque
> `01_introduccion_fundamentacion.md`. Cada fuente fue buscada en la web y
> contrastada con la página original (WebSearch + WebFetch). Se reporta,
> para cada cifra, si está **CONFIRMADA**, **AJUSTADA** o **NO CONFIRMADA**.
> Honestidad ante todo: no se inventó ninguna fuente. Donde la cita del
> informe estaba mal, se indica y se propone la corrección real.
>
> Fecha de verificación / acceso: **2026-06-03**.

---

## RESUMEN EJECUTIVO (qué hay que corregir en el informe)

| # | Cifra / cita en el informe | Veredicto | Acción requerida |
|---|---|---|---|
| 1 | Mercado USD 9.163 mil millones (2025) | **CONFIRMADA (con nota de errata de la fuente)** | Mantener 9.163; la fuente imprime "USD 9,163 **billion**", errata evidente por "million". Conservar la lectura 9.163 **mil millones**. |
| 2 | Proyección USD 59.900 millones (2033) | **CONFIRMADA** | Ninguna. |
| 3 | CAGR ≈ 26,8 % (2025–2033) | **CONFIRMADA** | Ninguna. |
| 4 | +8.400 millones de dispositivos habilitados para voz | **CONFIRMADA** | Ninguna. |
| 5 | "más del 67 % de los usuarios de smartphone usan voz al menos 1×/mes" | **NO CONFIRMADA** | **Corregir.** Ese 67 % NO está en la fuente Astute Analytica. Reemplazar por una cifra real de la misma fuente o quitar. |
| 6 | Privacidad: "cerca del 60 % preocupado" | **CONFIRMADA (= 58 %)** | Opcional: 31 % + 27 % = 58 %. "Cerca del 60 %" es admisible; preciso sería "58 %". |
| 7 | 49 % desconoce la escucha continua | **CONFIRMADA** | Ninguna. |
| 8 | 68 % nunca tomó medidas de privacidad | **CONFIRMADA** | Ninguna. |
| 9 | 77 % usaría más con mejor privacidad | **CONFIRMADA** | Ninguna. |
| 10 | Cita "Singh et al. (2024)" → arXiv 2410.11845 | **NO CONFIRMADA (autores incorrectos)** | **Corregir autores.** El paper 2410.11845 es de **Zheng, Y. et al.**, NO de "Singh, S., Sarkar, S., …". Esos autores parecen inventados. |
| 11 | Cita "Zheng et al. (2025)" → TST 10.26599/TST.2025.9010166 | **AJUSTADA (autores y año incorrectos)** | El artículo TST es de **Cai, G. et al. (2026)**, vol. 31(3). El apellido "Zheng" corresponde en realidad al *otro* paper (2410.11845). Hay un cruce de citas. |
| 12 | "Gemini Nano (Android) y Apple Intelligence ejecutan modelos localmente" (atribuido a Singh et al., 2024) | **NO CONFIRMADA en esa fuente** | El paper 2410.11845 NO nombra Gemini Nano ni Apple Intelligence (cita Gemma y OpenELM). Agregar una fuente primaria (Android Developers) para esos productos. |

**Lo más urgente:** (a) el **67 %** (ítem 5) no tiene respaldo en la fuente
citada; (b) los **autores de las dos citas académicas están cruzados/mal**
(ítems 10–11): hay un "Singh et al." que no corresponde a ningún paper real.

---

## 1. Astute Analytica — mercado de asistentes de voz

**Cómo aparece en el informe (Astute Analytica, 2026):**
mercado USD 9.163 mil millones (2025) → USD 59.900 millones (2033); CAGR
≈ 26,8 %; +8.400 millones de dispositivos; "más del 67 % de los usuarios de
smartphone usan comandos de voz al menos 1×/mes".

**Fuente real (verificada):** comunicado de prensa de Astute Analytica vía
GlobeNewswire, *"Voice Assistant Market to Reach US$ 59.9 Billion by 2033…"*,
10 de febrero de 2026; respaldado por la página de reporte de Astute
Analytica (`astuteanalytica.com/industry-report/voice-assistant-market`).

**Verificación cifra por cifra:**

- **USD 9.163 mil millones (2025) — CONFIRMADA, con errata de la fuente.**
  La fuente imprime literalmente *"Global market value reached USD 9,163
  billion in 2025"*. Eso es una **errata tipográfica de la fuente**: el valor
  2024 es USD 7.08 mil millones y el de 2033 es USD 59.9 mil millones, así que
  "9.163 **billion**" (>59.9) es imposible en la curva. El número correcto es
  USD 9.163 **mil millones** (= 9,163 **million**). El informe usa la lectura
  correcta. **Recomendación:** mantener "9.163 mil millones" y, si se desea,
  agregar nota "(la fuente imprime 'billion' por errata; el valor coherente con
  su propia serie 2024–2033 es ~9,16 mil millones)".

- **USD 59.900 millones (2033) — CONFIRMADA.** Literal en el título y cuerpo:
  *"to Reach US$ 59.9 Billion by 2033"*.

- **CAGR ≈ 26,8 % — CONFIRMADA.** Literal: *"growing at a CAGR of 26.80%
  from 2025 to 2033"*.

- **+8.400 millones de dispositivos — CONFIRMADA.** Literal: *"over 8.4
  billion enabled devices worldwide"*.

- **"más del 67 % de smartphones usan voz 1×/mes" — NO CONFIRMADA.**
  Esta cifra **no aparece** en la fuente Astute Analytica. Lo que la fuente sí
  dice es: *"125 million smartphone owners achieved 88.1% monthly penetration"*
  (penetración mensual del 88,1 % sobre 125 M de propietarios, dato de EE. UU.)
  y *"60% of owners reporting weekly usage—up from 45% in 2024"* (uso semanal).
  El 67 % no coincide con ninguno de estos. Tampoco se encontró el "67 %
  mensual" en búsquedas generales (las cifras cercanas reales son: ~61,9 %
  millennials/mes; 66 % interacción semanal — fuentes distintas).
  **Recomendación (elegir una):**
  1. Reemplazar por una cifra **de la misma fuente**, p. ej.: *"en EE. UU., la
     penetración mensual del asistente de voz entre propietarios de smartphone
     alcanzó el 88,1 % (Astute Analytica, 2026)"* — pero aclarando que es dato
     de EE. UU., no global.
  2. O usar el uso semanal: *"el 60 % de los usuarios recurre a la búsqueda por
     voz semanalmente, frente al 45 % en 2024 (Astute Analytica, 2026)"*.
  3. O bien **eliminar** la oración del 67 % para no sostener una cifra sin
     respaldo.
  Marcar mientras tanto en el informe: **[VERIFICAR: 67 % no confirmado en la
  fuente citada]**.

- **"cerca del 60 % preocupado por su privacidad" — ver fuente 2 (Secure Data
  Recovery), no Astute Analytica.** Nota: el informe atribuye esta frase a
  Astute Analytica en una parte ("Astute Analytica, 2026") y a Secure Data
  Recovery en otra. La cifra de privacidad pertenece a **Secure Data Recovery
  Services**, no a Astute Analytica. **Recomendación:** unificar la atribución
  del 60 % a Secure Data Recovery Services (2024).

---

## 2. Secure Data Recovery Services — encuesta de privacidad

**Cómo aparece en el informe (Secure Data Recovery Services, 2024):**
encuesta a >1.000 personas en EE. UU.; ~60 % preocupado al menos
ocasionalmente; 49 % desconoce la escucha continua; 68 % nunca tomó medidas;
77 % usaría más con mayor privacidad/transparencia.

**Fuente real (verificada):** Secure Data Recovery Services, *"Listening In:
Privacy Concerns of Voice Assistants"*, publicado el 5 de agosto de 2024
(actualizado 17 de febrero de 2026).
`https://www.securedatarecovery.com/blog/smart-device-privacy-concerns`

**Verificación cifra por cifra:**

- **>1.000 personas en EE. UU. — CONFIRMADA.** *"Over 1,000 Americans."*
- **~60 % preocupado — CONFIRMADA como 58 %.** La fuente da *"31% …
  privacy concerns … 27% … occasional concerns"*; 31 % + 27 % = **58 %**.
  "Cerca del 60 %" es una aproximación aceptable; lo más preciso sería decir
  **"alrededor del 58 % (31 % siempre + 27 % ocasionalmente)"**.
- **49 % desconoce la escucha continua — CONFIRMADA.** Literal: *"49% …
  did not know that voice assistants continuously listen for wake words"*.
- **68 % nunca tomó medidas — CONFIRMADA.** Literal: *"68% … have never
  taken any measures to improve the privacy"*.
- **77 % usaría más con mejor privacidad — CONFIRMADA.** Literal: *"77% …
  would be more likely to use voice assistants with enhanced privacy features
  and greater transparency"*.

**Veredicto:** fuente sólida y bien citada. Único ajuste fino sugerido: 58 %
en vez de "cerca del 60 %" si se quiere exactitud.

---

## 3. arXiv 2410.11845 — review de edge LLMs (cita "Singh et al." en el informe)

**Cómo aparece en el informe:** *Singh, S., Sarkar, S., Das, A., Saha, S., &
Saha, S. (2024). A review on edge large language models: Design, execution, and
applications. arXiv. https://arxiv.org/html/2410.11845v2*

**Fuente real (verificada):** el paper **existe** y el título es correcto, pero
**los autores citados son INCORRECTOS**. Los autores reales de arXiv 2410.11845
son:
**Zheng, Y., Chen, Y., Qian, B., Shi, X., Shu, Y., & Chen, J.** (2024;
revisado feb. 2025). *A Review on Edge Large Language Models: Design, Execution,
and Applications.* arXiv:2410.11845.

- **Autores "Singh, S., Sarkar, S., Das, A., Saha, S., & Saha, S." — NO
  CONFIRMADOS.** No existe ningún paper de edge LLMs con esos autores; parecen
  **inventados/erróneos**. El primer autor real es **Yue Zheng**.
- **Contenido (privacidad on-device) — CONFIRMADO.** El paper sí respalda la
  idea: *"processing sensitive data on-device eliminates risks associated with
  cloud transmission"* y *"local inference ensures faster responses and
  functionality without internet connectivity"*. Sostiene la afirmación general
  de privacidad/latencia/conectividad del informe.
- **Gemini Nano (Android) / Apple Intelligence — NO en esta fuente.** El paper
  **no nombra** Gemini Nano ni Apple Intelligence; menciona Gemma (Google) y
  OpenELM (Apple) como ejemplos de LLMs eficientes para borde, no esos
  productos de consumo. La oración del informe que atribuye "Gemini Nano… y
  Apple Intelligence… (Singh et al., 2024)" **no está respaldada por esta
  cita**. Ver fuente 5 para un respaldo real de ese claim.

**Acción:** corregir los autores a **Zheng et al. (2024)** y reasignar la
afirmación de Gemini Nano/Apple Intelligence a una fuente primaria.

---

## 4. Tsinghua Science and Technology — survey de inferencia edge (cita "Zheng et al." en el informe)

**Cómo aparece en el informe:** *Zheng, Z., Wang, Y., Huang, Y., Song, S.,
Yang, M., Tang, B., Xiong, F., & Li, Z. (2025). Efficient inference for edge
large language models: A survey. Tsinghua Science and Technology.
https://www.sciopen.com/article/10.26599/TST.2025.9010166*

**Fuente real (verificada):** el artículo **existe**, el título y la revista
son correctos, pero **autores y año están mal**. Los datos reales son:
**Cai, G., Tian, R., Yang, L., Jia, Y., Li, L., & Wang, J.** (2026).
*Efficient Inference for Edge Large Language Models: A Survey.* **Tsinghua
Science and Technology, 31(3).** DOI: 10.26599/TST.2025.9010166.

- **Autores "Zheng, Z., Wang, Y., …" — NO CONFIRMADOS.** Los autores reales
  son **Cai, G. et al.** (primer autor: Guanyu Cai). Nota: el apellido "Zheng"
  que usó el informe corresponde en realidad al *otro* paper (2410.11845,
  fuente 3) → hubo un **cruce de citas** entre las dos referencias académicas.
- **Año 2025 → 2026.** El artículo aparece en el vol. 31, nº 3 (junio 2026).
  El DOI conserva "2025" por convención de la revista, pero el año de
  publicación es 2026.
- **Contenido — CONFIRMADO.** Respalda el claim del informe: el cloud-deployment
  de LLMs *"introduc[e] challenges related to cost, latency, privacy, and
  network reliability"* y el on-device está limitado por *"the severe resource
  constraints of edge hardware"*. Pertinente y correcto.

**Acción:** corregir autores a **Cai, G. et al.** y año a **2026**.

---

## 5. (NUEVA, recomendada) Fuente primaria para Gemini Nano / Apple Intelligence on-device

Para sostener la oración *"Fabricantes como Google (con Gemini Nano en Android)
y Apple (con Apple Intelligence) ya ejecutan modelos localmente"*, que **no**
está respaldada por arXiv 2410.11845, se recomienda agregar una fuente primaria
real:

- **Android Developers. (s. f.).** *Gemini Nano.* Google.
  `https://developer.android.com/ai/gemini-nano` (acceso: 2026-06-03).
  Documentación oficial: Gemini Nano corre on-device vía AICore, con beneficio
  explícito de privacidad por procesamiento local.

(Para Apple Intelligence, si se quiere cita directa, usar la página oficial de
Apple sobre Apple Intelligence on-device; no se fetcheó la primaria de Apple en
esta verificación — marcar **[VERIFICAR: agregar URL oficial de Apple
Intelligence]** si se incluye el nombre del producto.)

---

## BIBLIOGRAFÍA APA 7 — versión limpia y verificada

> Lista lista para pegar en el informe (sección Bibliografía), con autores y
> años CORREGIDOS. Las entradas marcadas con ✅ están verificadas contra la
> página original el 2026-06-03.

Android Developers. (s. f.). *Gemini Nano*. Google. Recuperado el 3 de junio de
2026, de https://developer.android.com/ai/gemini-nano ✅ *(fuente nueva
recomendada para el claim Gemini Nano on-device)*

Astute Analytica. (2026, 10 de febrero). *Voice assistant market to reach US$
59.9 billion by 2033 driven by mass consumer adoption, enterprise voice AI, and
smart device proliferation*. GlobeNewswire. Recuperado el 3 de junio de 2026,
de https://www.globenewswire.com/news-release/2026/02/10/3235286/0/en/Voice-Assistant-Market-to-Reach-US-59-9-Billion-by-2033-Driven-by-Mass-Consumer-Adoption-Enterprise-Voice-AI-and-Smart-Device-Proliferation-Astute-Analytica.html
✅ *(market size, CAGR y dispositivos CONFIRMADOS; el "67 %" NO proviene de
aquí)*

Cai, G., Tian, R., Yang, L., Jia, Y., Li, L., & Wang, J. (2026). Efficient
inference for edge large language models: A survey. *Tsinghua Science and
Technology, 31*(3). https://doi.org/10.26599/TST.2025.9010166 ✅ *(reemplaza la
cita errónea "Zheng et al., 2025")*

Secure Data Recovery Services. (2024, 5 de agosto). *Listening in: Privacy
concerns of voice assistants*. Recuperado el 3 de junio de 2026, de
https://www.securedatarecovery.com/blog/smart-device-privacy-concerns ✅
*(49 %, 68 %, 77 % y ~58 % CONFIRMADOS)*

Zheng, Y., Chen, Y., Qian, B., Shi, X., Shu, Y., & Chen, J. (2024). *A review on
edge large language models: Design, execution, and applications* [Preprint].
arXiv. https://arxiv.org/abs/2410.11845 ✅ *(reemplaza la cita errónea "Singh et
al., 2024"; respalda el beneficio de privacidad on-device, pero NO nombra Gemini
Nano/Apple Intelligence)*

---

## CAMBIOS CONCRETOS A APLICAR EN `01_introduccion_fundamentacion.md`

1. **Cita en texto** (líneas ~105, 108): cambiar **"(Singh et al., 2024;
   Zheng et al., 2025)"** por **"(Zheng et al., 2024; Cai et al., 2026)"**.
2. **Oración Gemini Nano/Apple Intelligence** (línea ~106-108): cambiar la cita
   de "(Singh et al., 2024)" a **"(Android Developers, s. f.)"** para el nombre
   del producto, o reformular para no atribuir esos productos al review de edge
   LLMs.
3. **Cifra 67 %** (líneas ~91-93): **[VERIFICAR: no confirmada]**. Reemplazar
   por la penetración mensual real de la fuente ("88,1 % en EE. UU. sobre 125 M
   de propietarios de smartphone") aclarando que es dato de EE. UU., o por el
   "60 % de uso semanal (vs. 45 % en 2024)", o eliminar la oración.
4. **Atribución del ~60 % de privacidad** (líneas ~91-93 vs. 111-119): unificar
   a **Secure Data Recovery Services (2024)**; opcionalmente precisar **58 %**.
5. **Lista de Referencias** (líneas ~341-355): reemplazar las dos entradas
   académicas erróneas (Singh / Zheng) por **Zheng, Y. et al. (2024)** y
   **Cai, G. et al. (2026)**, y agregar **Android Developers (s. f.)** si se
   conserva la mención a Gemini Nano.

---

## NOTA DE HONESTIDAD METODOLÓGICA

- Todas las cifras de mercado y de privacidad fueron contrastadas contra la
  página original mediante WebFetch el **2026-06-03**.
- El "USD 9,163 billion" de Astute Analytica es una **errata de la propia
  fuente** (incoherente con su serie 2024→2033); el informe usa la lectura
  correcta (9.163 mil millones). Esto se documenta, no se oculta.
- El **67 %** no pudo confirmarse en ninguna fuente: se marca como NO
  CONFIRMADO y se ofrecen alternativas reales y verificadas.
- Las dos citas académicas tenían **autores incorrectos** (posible cruce al
  redactar); se corrigieron con los autores reales tomados de arXiv y de
  Sciopen/Tsinghua. No se inventó ninguna fuente nueva: la única adición
  (Android Developers) es documentación oficial real para sostener un claim que
  la cita original no respaldaba.
