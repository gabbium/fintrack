namespace Fintrack.Ledger.Application.Commands.DeleteMovement;

public sealed record DeleteMovementCommand(
    Guid Id)
    : ICommand;
