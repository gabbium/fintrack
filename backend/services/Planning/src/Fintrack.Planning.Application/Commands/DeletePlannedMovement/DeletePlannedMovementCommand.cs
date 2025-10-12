namespace Fintrack.Planning.Application.Commands.DeletePlannedMovement;

public sealed record DeletePlannedMovementCommand(
    Guid Id)
    : ICommand<Result>;
