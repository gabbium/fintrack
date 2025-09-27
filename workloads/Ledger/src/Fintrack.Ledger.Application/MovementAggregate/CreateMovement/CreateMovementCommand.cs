using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.MovementAggregate.CreateMovement;

public sealed record CreateMovementCommand(
    MovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset OccurredOn)
    : ICommand<MovementDto>;
