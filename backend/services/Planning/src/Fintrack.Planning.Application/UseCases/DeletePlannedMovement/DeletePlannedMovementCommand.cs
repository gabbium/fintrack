namespace Fintrack.Planning.Application.UseCases.DeletePlannedMovement;

public sealed record DeletePlannedMovementCommand(
    Guid Id)
    : ICommand<Result>;
