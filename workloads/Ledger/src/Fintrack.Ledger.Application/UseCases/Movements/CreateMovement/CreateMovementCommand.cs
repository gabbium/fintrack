using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.Movements;

namespace Fintrack.Ledger.Application.UseCases.Movements.CreateMovement;

public sealed record CreateMovementCommand(
    MovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset OccurredOn)
    : ICommand<MovementDto>;
