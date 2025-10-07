using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Application.UseCases.GetPlannedMovementById;

public sealed record GetPlannedMovementByIdQuery(
    Guid Id)
    : IQuery<PlannedMovementDto>;

