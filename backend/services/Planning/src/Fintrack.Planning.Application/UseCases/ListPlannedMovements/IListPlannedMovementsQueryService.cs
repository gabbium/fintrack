using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Application.UseCases.ListPlannedMovements;

public interface IListPlannedMovementsQueryService
{
    Task<PaginatedList<PlannedMovementDto>> ListAsync(
        ListPlannedMovementsQuery query,
        CancellationToken cancellationToken = default);
}
