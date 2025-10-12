using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Application.Queries.ListMovements;

namespace Fintrack.Ledger.Infrastructure.Queries;

internal sealed class ListMovementsQueryService(LedgerDbContext context) : IListMovementsQueryService
{
    public async Task<PaginatedList<MovementDto>> ListAsync(
        ListMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryable = context.Movements.AsNoTracking();

        if (query.Kind is { Count: > 0 })
            queryable = queryable.Where(movement => query.Kind.Contains(movement.Kind));

        if (query.MinOccurredOn is not null)
            queryable = queryable.Where(movement => movement.OccurredOn >= query.MinOccurredOn.Value);

        if (query.MaxOccurredOn is not null)
            queryable = queryable.Where(movement => movement.OccurredOn <= query.MaxOccurredOn.Value);

        var normalizedOrder = query.Order?.Trim().ToLowerInvariant();

        queryable = normalizedOrder switch
        {
            "occurredon desc" => queryable
                .OrderByDescending(movement => movement.OccurredOn)
                .ThenBy(movement => movement.Id),
            "occurredon asc" => queryable
                .OrderBy(movement => movement.OccurredOn)
                .ThenBy(movement => movement.Id),
            "amount desc" => queryable
                .OrderByDescending(movement => movement.Amount)
                .ThenBy(movement => movement.Id),
            "amount asc" => queryable
                .OrderBy(movement => movement.Amount)
                .ThenBy(movement => movement.Id),
            _ => queryable
                .OrderByDescending(movement => movement.OccurredOn)
                .ThenBy(movement => movement.Id),
        };

        return await queryable.ToPaginatedListAsync(
            query.PageNumber,
            query.PageSize,
            movement => MovementDto.FromAggregate(movement),
            cancellationToken);
    }
}
