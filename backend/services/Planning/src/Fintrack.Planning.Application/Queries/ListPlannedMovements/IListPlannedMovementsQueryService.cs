using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Application.Queries.ListPlannedMovements;

public interface IListPlannedMovementsQueryService
{
    Task<PaginatedList<PlannedMovementDto>> ListAsync(
        ListPlannedMovementsQuery query,
        CancellationToken cancellationToken = default);
}
