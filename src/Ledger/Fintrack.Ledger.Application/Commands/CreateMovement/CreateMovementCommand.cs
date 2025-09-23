using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.Enums;

namespace Fintrack.Ledger.Application.Commands.CreateMovement;

public sealed record CreateMovementCommand(
    MovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset OccurredOn)
    : ICommand<MovementDto>;
