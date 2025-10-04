namespace Fintrack.Ledger.Application.UseCases.DeleteMovement;

public sealed record DeleteMovementCommand(
    Guid Id)
    : ICommand;
