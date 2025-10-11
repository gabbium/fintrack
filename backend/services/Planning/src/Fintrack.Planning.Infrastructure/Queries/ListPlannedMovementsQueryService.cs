using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Application.UseCases.ListPlannedMovements;

namespace Fintrack.Planning.Infrastructure.Queries;

internal sealed class ListPlannedMovementsQueryService(PlanningDbContext dbContext) : IListPlannedMovementsQueryService
{
    public async Task<PaginatedList<PlannedMovementDto>> ListAsync(
        ListPlannedMovementsQuery query,
        CancellationToken cancellationToken = default)
    {
        var queryable = dbContext.PlannedMovements.AsNoTracking();

        if (query.Kind is { Count: > 0 })
        {
            queryable = queryable.Where(pm => query.Kind.Contains(pm.Kind));
        }

        if (query.Status is { Count: > 0 })
        {
            queryable = queryable.Where(pm => query.Status.Contains(pm.Status));
        }

        if (query.MinDueOn is not null)
        {
            queryable = queryable.Where(pm => pm.DueOn >= query.MinDueOn.Value);
        }

        if (query.MaxDueOn is not null)
        {
            queryable = queryable.Where(pm => pm.DueOn <= query.MaxDueOn.Value);
        }

        var normalizedOrder = query.Order?.Trim().ToLowerInvariant();

        queryable = normalizedOrder switch
        {
            "dueon desc" => queryable
                .OrderByDescending(pm => pm.DueOn)
                .ThenBy(pm => pm.Id),
            "dueon asc" => queryable
                .OrderBy(pm => pm.DueOn)
                .ThenBy(pm => pm.Id),
            "amount desc" => queryable
                .OrderByDescending(pm => pm.Amount)
                .ThenBy(pm => pm.Id),
            "amount asc" => queryable
                .OrderBy(pm => pm.Amount)
                .ThenBy(pm => pm.Id),
            _ => queryable
                .OrderByDescending(pm => pm.DueOn)
                .ThenBy(pm => pm.Id),
        };

        return await queryable.ToPaginatedListAsync(
            query.PageNumber,
            query.PageSize,
            pm => PlannedMovementDto.FromDomain(pm),
            cancellationToken);
    }
}
