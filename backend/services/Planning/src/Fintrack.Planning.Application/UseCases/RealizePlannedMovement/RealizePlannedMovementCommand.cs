namespace Fintrack.Planning.Application.UseCases.RealizePlannedMovement;

public sealed record RealizePlannedMovementCommand(
    Guid Id)
    : ICommand<Result>;

