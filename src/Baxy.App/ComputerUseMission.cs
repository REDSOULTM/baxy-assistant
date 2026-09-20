using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using Baxy.Contracts;
using Baxy.Kernel.Operations;

namespace Baxy.App;

/// <summary>
/// CU1959 (plan post-goal Fase 4, D21): el motor general de computer use.
/// Un objetivo dentro de cualquier aplicación se cumple mirando la ventana en
/// primer plano (controles UIA y texto OCR), pidiéndole a la mente UN paso del
/// repertorio, ejecutándolo por el core con su postlectura y volviendo a
/// mirar, hasta que la comprobación de éxito se ve o el presupuesto se agota.
/// No sabe de ninguna aplicación: todo lo que ve viene de la pantalla y todo
/// lo que hace son primitivas del catálogo con el riesgo de cada una.
/// </summary>
internal static class ComputerUseMission
{
    internal const string OperationName = "mission.computer_use";
    internal const int DefaultBudgetSteps = 12;
    private const int MaximumBudgetSteps = 12;
    // Cada vuelta cuesta una lectura (UIA + OCR), una decisión del modelo y
    // una primitiva con postlectura; doce vueltas caben en este tiempo.
    private static readonly TimeSpan TimeBudget = TimeSpan.FromSeconds(150);
    private static readonly TimeSpan ViewTimeout = TimeSpan.FromSeconds(30);
    private static readonly TimeSpan ActionTimeout = TimeSpan.FromSeconds(45);

    // Repertorio cerrado: lo que una persona hace con el teclado y el ratón
    // sobre lo que ve, más abrir la aplicación. Nada por aplicación.
    internal static readonly IReadOnlySet<string> Primitives = new HashSet<string>(StringComparer.Ordinal)
    {
        "app.open",
        "input.key.press",
        "input.scroll",
        "input.text.type",
        "input.visible.click",
    };

    internal sealed record Outcome(bool Succeeded, string? ErrorCode, JsonObject Observed)
    {
        internal string ToFacts()
        {
            using JsonDocument document = JsonDocument.Parse(Observed.ToJsonString());
            OperationOutcome outcome = Succeeded
                ? OperationOutcome.Success(document.RootElement.Clone())
                : OperationOutcome.Failure(ErrorCode ?? "computer_use_failed", document.RootElement.Clone());
            return OperationVisibleFacts.FromOutcome(OperationName, outcome);
        }
    }

    internal static async Task<Outcome> RunAsync(
        CoreProcessClient core,
        MindSidecarClient mind,
        RetryableOperationRegistry registry,
        Func<RetryableOperationRegistry, PreparedOperation, bool> markResolved,
        Action<string> setStatus,
        string objective,
        JsonObject arguments,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(core);
        ArgumentNullException.ThrowIfNull(mind);
        ArgumentNullException.ThrowIfNull(registry);
        ArgumentNullException.ThrowIfNull(arguments);
        string goal = (string?)arguments["goal"] ?? objective;
        string? successCheck = arguments["successCheck"] is JsonValue check
            && check.TryGetValue(out string? checkText)
            && !string.IsNullOrWhiteSpace(checkText)
                ? checkText.Trim()
                : null;
        int budget = arguments["budgetSteps"] is JsonValue budgetValue
            && budgetValue.TryGetValue(out int requestedBudget)
                ? Math.Clamp(requestedBudget, 1, MaximumBudgetSteps)
                : DefaultBudgetSteps;
        var steps = new JsonArray();
        var history = new JsonArray();
        var stopwatch = Stopwatch.StartNew();
        string traceId = ShellTraceSink.TurnId;
        string? lastWindow = null;
        string? lastSignature = null;
        int unchangedViews = 0;
        string? errorCode = null;
        bool reached = false;
        string? evidence = null;
        ShellTraceSink.Record(ShellTraceScopes.Turn, traceId, "computer_use.start",
            $"budget.{budget}");

        for (int step = 1; step <= budget; step++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (stopwatch.Elapsed > TimeBudget)
            {
                errorCode = "computer_use_time_exhausted";
                break;
            }

            setStatus("Mirando la pantalla");
            JsonObject? view = await LookAsync(core, registry, markResolved, cancellationToken)
                .ConfigureAwait(true);
            if (view is null)
            {
                errorCode = "computer_use_view_unavailable";
                break;
            }

            lastWindow = (string?)view["window"];
            string signature = ViewSignature(view);
            unchangedViews = string.Equals(signature, lastSignature, StringComparison.Ordinal)
                ? unchangedViews + 1
                : 0;
            lastSignature = signature;
            // Dos actos seguidos sin que la pantalla cambie: el bucle para y
            // dice lo que ve, en vez de dar vueltas (diseño §3).
            if (unchangedViews >= 2 && steps.Count > 0)
            {
                errorCode = "computer_use_surface_unchanged";
                break;
            }

            ShellTraceSink.Record(ShellTraceScopes.Turn, traceId, "computer_use.view",
                $"step.{step}.controls.{ControlCount(view)}.text.{TextCount(view)}");
            setStatus($"Paso {step}: decidiendo");
            MindComputerUseStep? decision = await mind.DecideComputerUseStepAsync(
                objective, goal, successCheck, view, history, cancellationToken)
                .ConfigureAwait(true);
            if (decision is null)
            {
                errorCode = "computer_use_decision_unavailable";
                break;
            }

            ShellTraceSink.Record(ShellTraceScopes.Turn, traceId, "computer_use.decision",
                $"step.{step}.{ShellTrace.SanitizeLabel(decision.Operation)}");
            if (decision.Operation == "done")
            {
                reached = true;
                evidence = (string?)decision.Arguments["evidence"] ?? decision.Reason;
                break;
            }

            if (decision.Operation == "none" || !Primitives.Contains(decision.Operation))
            {
                errorCode = "computer_use_no_step_visible";
                evidence = decision.Reason;
                break;
            }

            setStatus($"Paso {step}: {DescribeStep(decision)}");
            JsonObject record = await ActAsync(
                core, registry, markResolved, decision, cancellationToken).ConfigureAwait(true);
            record["step"] = step;
            steps.Add(record);
            history.Add(record.DeepClone());
            ShellTraceSink.Record(ShellTraceScopes.Turn, traceId, "computer_use.step",
                $"step.{step}.{ShellTrace.SanitizeLabel(decision.Operation)}.ok.{(bool?)record["ok"] == true}");
            // Un acto que no pudo hacerse deja la pantalla como estaba: si el
            // siguiente tampoco puede, la lectura repetida lo cortará arriba.
            await Task.Delay(400, cancellationToken).ConfigureAwait(true);
        }

        if (!reached && errorCode is null)
        {
            errorCode = "computer_use_budget_exhausted";
        }

        var observed = new JsonObject
        {
            ["goal"] = goal,
            ["successCheck"] = successCheck,
            ["reached"] = reached,
            ["stepCount"] = steps.Count,
            ["steps"] = steps,
            ["window"] = lastWindow,
            ["elapsedSeconds"] = (int)stopwatch.Elapsed.TotalSeconds,
            ["authority"] = "shell_loop_over_uia_ocr_postread",
        };
        if (evidence is not null)
        {
            observed["evidence"] = evidence;
        }

        ShellTraceSink.Record(ShellTraceScopes.Turn, traceId, "computer_use.end",
            $"reached.{reached}.steps.{steps.Count}.{ShellTrace.SanitizeLabel(errorCode ?? "ok")}");
        return new Outcome(reached, reached ? null : errorCode, observed);
    }

    private static async Task<JsonObject?> LookAsync(
        CoreProcessClient core,
        RetryableOperationRegistry registry,
        Func<RetryableOperationRegistry, PreparedOperation, bool> markResolved,
        CancellationToken cancellationToken)
    {
        var routed = new RoutedOperation(
            "input.visible.controls",
            new JsonObject { ["includeText"] = true, ["limit"] = 60 });
        PreparedOperation prepared = registry.GetOrAdd(routed);
        try
        {
            OperationResponse response = await core.SendOperationAsync(
                prepared, ViewTimeout, cancellationToken).ConfigureAwait(false);
            if (response.Status != OperationStatuses.Completed
                || !response.Verified
                || response.Result is not { ValueKind: JsonValueKind.Object } result)
            {
                return null;
            }

            return JsonNode.Parse(result.GetRawText()) as JsonObject;
        }
        finally
        {
            _ = markResolved(registry, prepared);
        }
    }

    private static async Task<JsonObject> ActAsync(
        CoreProcessClient core,
        RetryableOperationRegistry registry,
        Func<RetryableOperationRegistry, PreparedOperation, bool> markResolved,
        MindComputerUseStep decision,
        CancellationToken cancellationToken)
    {
        var record = new JsonObject
        {
            ["operation"] = decision.Operation,
        };
        foreach (string key in new[] { "label", "text", "key", "direction", "amount", "applicationName" })
        {
            if (decision.Arguments[key] is JsonNode value)
            {
                record[key] = value.DeepClone();
            }
        }

        JsonObject arguments = decision.Arguments.DeepClone() as JsonObject ?? new JsonObject();
        if (!MindPlanBoundary.ArgumentsSatisfyExactSchema(decision.Operation, arguments))
        {
            record["ok"] = false;
            record["error"] = "computer_use_step_arguments_invalid";
            return record;
        }

        PreparedOperation prepared = registry.GetOrAdd(new RoutedOperation(decision.Operation, arguments));
        try
        {
            OperationResponse response = await core.SendOperationAsync(
                prepared, ActionTimeout, cancellationToken).ConfigureAwait(false);
            bool ok = response.Status == OperationStatuses.Completed && response.Verified;
            record["ok"] = ok;
            if (!ok)
            {
                // Un paso que pide confirmación no cabe en el bucle: el
                // repertorio sólo tiene primitivas que en modo normal no
                // preguntan; si una lo hiciera, se registra y el bucle sigue.
                record["error"] = response.ErrorCode
                    ?? (response.Status == OperationStatuses.Pending
                        ? "computer_use_step_pending"
                        : "computer_use_step_failed");
            }

            if (response.Result is { ValueKind: JsonValueKind.Object } result)
            {
                foreach (string key in new[] { "cascadeStage", "surfaceChanged", "absentOrDisabled", "selected", "processId", "name" })
                {
                    if (result.TryGetProperty(key, out JsonElement value)
                        && value.ValueKind is JsonValueKind.String or JsonValueKind.True
                            or JsonValueKind.False or JsonValueKind.Number)
                    {
                        record[key] = JsonNode.Parse(value.GetRawText());
                    }
                }
            }

            return record;
        }
        finally
        {
            _ = markResolved(registry, prepared);
        }
    }

    private static string DescribeStep(MindComputerUseStep decision) => decision.Operation switch
    {
        "input.visible.click" => $"clic en «{(string?)decision.Arguments["label"]}»",
        "input.text.type" => "escribiendo",
        "input.key.press" => $"tecla {(string?)decision.Arguments["key"]}",
        "input.scroll" => "desplazando",
        "app.open" => $"abriendo {(string?)decision.Arguments["applicationName"]}",
        _ => decision.Operation,
    };

    private static string ViewSignature(JsonObject view)
    {
        var parts = new List<string> { (string?)view["window"] ?? string.Empty };
        if (view["controls"] is JsonArray controls)
        {
            foreach (JsonNode? control in controls)
            {
                if (control is JsonObject item)
                {
                    parts.Add($"{(string?)item["kind"]}|{(string?)item["name"]}|{(string?)item["state"]}");
                }
            }
        }

        if (view["text"] is JsonArray text)
        {
            foreach (JsonNode? line in text)
            {
                parts.Add((string?)line ?? string.Empty);
            }
        }

        return string.Join('\n', parts);
    }

    private static int ControlCount(JsonObject view) =>
        view["controls"] is JsonArray controls ? controls.Count : 0;

    private static int TextCount(JsonObject view) =>
        view["text"] is JsonArray text ? text.Count : 0;
}
