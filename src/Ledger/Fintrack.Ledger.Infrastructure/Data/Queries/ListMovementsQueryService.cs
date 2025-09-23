using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.Queries.ListMovements;

namespace Fintrack.Ledger.Infrastructure.Data.Queries;

internal sealed class ListMovementsQueryService(LedgerDbContext context) : IListMovementsQueryService
{
    public async Task<PaginatedList<MovementDto>> ListAsync(
        ListMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryable = context.Movements
            .AsNoTracking()
            .AsQueryable();

        var totalItems = await queryable.CountAsync(cancellationToken);

        var movements = await queryable
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(m => MovementDto.FromDomain(m))
            .ToListAsync(cancellationToken);

        return new PaginatedList<MovementDto>(movements, totalItems, query.PageNumber, query.PageSize);
    }
}
