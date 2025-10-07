using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UseCases.CreatePlannedMovement;

public sealed record CreatePlannedMovementCommand(
    PlannedMovementKind Kind,
    decimal Amount,
    string? Description,
    DateTimeOffset DueOn)
    : ICommand<PlannedMovementDto>;
