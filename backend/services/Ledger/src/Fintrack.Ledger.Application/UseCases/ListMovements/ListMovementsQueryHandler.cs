using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Application.UseCases.ListMovements;

internal sealed class ListMovementsQueryHandler(
    IListMovementsQueryService listMovementsQueryService)
    : IQueryHandler<ListMovementsQuery, PaginatedList<MovementDto>>
{
    public async Task<Result<PaginatedList<MovementDto>>> HandleAsync(
        ListMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        return await listMovementsQueryService.ListAsync(query, cancellationToken);
    }
}
