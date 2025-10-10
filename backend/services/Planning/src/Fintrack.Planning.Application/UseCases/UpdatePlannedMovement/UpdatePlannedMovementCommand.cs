using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.UpdatePlannedMovement;

public sealed record UpdatePlannedMovementCommand(
    Guid Id,
    PlannedMovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset DueOn)
    : ICommand<Result<PlannedMovementDto>>;
