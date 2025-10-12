using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.Commands.UpdateMovement;

public sealed record UpdateMovementCommand(
    Guid Id,
    MovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset OccurredOn)
    : ICommand<Result<MovementDto>>;
