using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.Commands.CreatePlannedMovement;

public sealed record CreatePlannedMovementCommand(
    PlannedMovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset DueOn)
    : ICommand<Result<PlannedMovementDto>>;
