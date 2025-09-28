using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Application.Queries.ListMovements;

public interface IListMovementsQueryService
{
    Task<PaginatedList<MovementDto>> ListAsync(
        ListMovementsQuery query,
        CancellationToken cancellationToken = default);
}
