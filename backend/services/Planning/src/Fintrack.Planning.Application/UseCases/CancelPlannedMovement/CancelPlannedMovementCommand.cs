namespace Fintrack.Planning.Application.UseCases.CancelPlannedMovement;

public sealed record CancelPlannedMovementCommand(
    Guid Id)
    : ICommand<Result>;

