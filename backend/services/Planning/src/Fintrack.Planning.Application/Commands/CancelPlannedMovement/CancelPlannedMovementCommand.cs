namespace Fintrack.Planning.Application.Commands.CancelPlannedMovement;

public sealed record CancelPlannedMovementCommand(
    Guid Id)
    : ICommand<Result>;

