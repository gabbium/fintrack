using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.Queries.ListMovements;

namespace Fintrack.Ledger.Infrastructure.Queries;

internal sealed class ListMovementsQueryService(LedgerDbContext context) : IListMovementsQueryService
{
    public async Task<PaginatedList<MovementDto>> ListAsync(
        ListMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryable = context.Movements
            .AsNoTracking()
            .AsQueryable();

        if (query.Kinds.Count > 0)
        {
            queryable = queryable.Where(movement => query.Kinds.Contains(movement.Kind));
        }

        if (query.MinOccurredOn is not null)
        {
            queryable = queryable.Where(x => x.OccurredOn >= query.MinOccurredOn.Value);
        }

        if (query.MaxOccurredOn is not null)
        {
            queryable = queryable.Where(x => x.OccurredOn <= query.MaxOccurredOn.Value);
        }

        var totalItems = await queryable.CountAsync(cancellationToken);

        var movements = await queryable
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(movement => MovementDto.FromDomain(movement))
            .ToListAsync(cancellationToken);

        return new PaginatedList<MovementDto>(movements, totalItems, query.PageNumber, query.PageSize);
    }
}
