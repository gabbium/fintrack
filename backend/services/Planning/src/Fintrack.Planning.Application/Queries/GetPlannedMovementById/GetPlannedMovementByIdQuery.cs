using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Application.Queries.GetPlannedMovementById;

public sealed record GetPlannedMovementByIdQuery(
    Guid Id)
    : IQuery<Result<PlannedMovementDto>>;

