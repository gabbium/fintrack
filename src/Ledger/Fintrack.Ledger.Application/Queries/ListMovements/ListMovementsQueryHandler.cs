using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Application.Queries.ListMovements;

internal sealed class ListMovementsQueryHandler(
    IListMovementsQueryService listMovementsQueryService)
    : IQueryHandler<ListMovementsQuery, PaginatedList<MovementDto>>
{
    public async Task<Result<PaginatedList<MovementDto>>> HandleAsync(
        ListMovementsQuery request,
        CancellationToken cancellationToken = default)
    {
        return await listMovementsQueryService.ListAsync(request, cancellationToken);
    }
}
