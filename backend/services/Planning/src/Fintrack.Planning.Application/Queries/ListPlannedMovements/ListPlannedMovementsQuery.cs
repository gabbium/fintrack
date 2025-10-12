using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.Queries.ListPlannedMovements;

public sealed record ListPlannedMovementsQuery(
    int PageNumber,
    int PageSize,
    string? Order,
    List<PlannedMovementKind>? Kind,
    List<PlannedMovementStatus>? Status,
    DateTimeOffset? MinDueOn,
    DateTimeOffset? MaxDueOn)
    : IQuery<Result<PaginatedList<PlannedMovementDto>>>;
