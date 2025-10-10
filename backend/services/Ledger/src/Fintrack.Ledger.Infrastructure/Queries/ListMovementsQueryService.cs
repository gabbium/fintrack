using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.UseCases.ListMovements;

namespace Fintrack.Ledger.Infrastructure.Queries;

internal sealed class ListMovementsQueryService(LedgerDbContext dbContext) : IListMovementsQueryService
{
    public async Task<PaginatedList<MovementDto>> ListAsync(
        ListMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryable = dbContext.Movements.AsNoTracking();

        if (query.Kind is { Count: > 0 })
        {
            queryable = queryable.Where(mov => query.Kind.Contains(mov.Kind));
        }

        if (query.MinOccurredOn is not null)
        {
            queryable = queryable.Where(mov => mov.OccurredOn >= query.MinOccurredOn.Value);
        }

        if (query.MaxOccurredOn is not null)
        {
            queryable = queryable.Where(mov => mov.OccurredOn <= query.MaxOccurredOn.Value);
        }

        var normalizedOrder = query.Order?.Trim().ToLowerInvariant();

        queryable = normalizedOrder switch
        {
            "occurredon desc" => queryable
                .OrderByDescending(mov => mov.OccurredOn)
                .ThenBy(mov => mov.Id),
            "occurredon asc" => queryable
                .OrderBy(mov => mov.OccurredOn)
                .ThenBy(mov => mov.Id),
            "amount desc" => queryable
                .OrderByDescending(mov => mov.Amount)
                .ThenBy(mov => mov.Id),
            "amount asc" => queryable
                .OrderBy(mov => mov.Amount)
                .ThenBy(mov => mov.Id),
            _ => queryable
                .OrderByDescending(mov => mov.OccurredOn)
                .ThenBy(mov => mov.Id),
        };

        return await queryable.ToPaginatedListAsync(
            query.PageNumber,
            query.PageSize,
            movement => MovementDto.FromDomain(movement),
            cancellationToken);
    }
}
