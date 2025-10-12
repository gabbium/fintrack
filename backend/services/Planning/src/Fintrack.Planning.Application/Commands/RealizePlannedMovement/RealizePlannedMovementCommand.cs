namespace Fintrack.Planning.Application.Commands.RealizePlannedMovement;

public sealed record RealizePlannedMovementCommand(
    Guid Id)
    : ICommand<Result>;

