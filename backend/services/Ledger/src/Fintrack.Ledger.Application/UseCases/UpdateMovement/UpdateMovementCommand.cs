using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UseCases.UpdateMovement;

public sealed record UpdateMovementCommand(
    Guid Id,
    MovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset OccurredOn)
    : ICommand<MovementDto>;
