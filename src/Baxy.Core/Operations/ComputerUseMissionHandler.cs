using Baxy.Kernel.Operations;

namespace Baxy.Core.Operations;

/// <summary>
/// CU1959 (plan post-goal Fase 4, D21): el bucle de computer use —mirar,
/// elegir un paso, ejecutarlo, volver a mirar— lo conduce el shell, que es
/// quien tiene a la mente delante. El core sólo ejecuta las primitivas de
/// cada paso con su postlectura. Invocado directamente, sin ese bucle, no
/// puede decidir nada y lo dice antes de tocar la pantalla.
/// </summary>
internal sealed class ComputerUseMissionHandler : IOperationHandler
{
    public OperationDefinition Definition { get; } =
        ProductCatalog.CreateDefinition("mission.computer_use");

    public ValueTask<OperationOutcome> ExecuteAsync(
        OperationInvocation invocation,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return ValueTask.FromResult(OperationOutcome.Failure(
            "computer_use_loop_not_hosted",
            causeCode: "computer_use_requires_mind"));
    }
}
