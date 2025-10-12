using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Application.Queries.ListPlannedMovements;

internal sealed class ListPlannedMovementsQueryHandler(
    IListPlannedMovementsQueryService listPlannedMovementsQueryService)
    : IQueryHandler<ListPlannedMovementsQuery, Result<PaginatedList<PlannedMovementDto>>>
{
    public async Task<Result<PaginatedList<PlannedMovementDto>>> HandleAsync(
        ListPlannedMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await listPlannedMovementsQueryService.ListAsync(query, cancellationToken);
    }
}
