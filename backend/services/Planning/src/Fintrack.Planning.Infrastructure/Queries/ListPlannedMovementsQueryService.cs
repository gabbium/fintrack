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
            queryable = queryable.Where(plannedMovement => query.Kind.Contains(plannedMovement.Kind));
        }

        if (query.Status is { Count: > 0 })
        {
            queryable = queryable.Where(plannedMovement => query.Status.Contains(plannedMovement.Status));
        }

        if (query.MinDueOn is not null)
        {
            queryable = queryable.Where(plannedMovement => plannedMovement.DueOn >= query.MinDueOn.Value);
        }

        if (query.MaxDueOn is not null)
        {
            queryable = queryable.Where(plannedMovement => plannedMovement.DueOn <= query.MaxDueOn.Value);
        }

        var totalItems = await queryable.CountAsync(cancellationToken);

        var normalizedOrder = query.Order?.Trim().ToLowerInvariant();

        queryable = normalizedOrder switch
        {
            "dueon desc" => queryable
                .OrderByDescending(plannedMovement => plannedMovement.DueOn)
                .ThenBy(plannedMovement => plannedMovement.Id),
            "dueon asc" => queryable
                .OrderBy(plannedMovement => plannedMovement.DueOn)
                .ThenBy(plannedMovement => plannedMovement.Id),
            "amount desc" => queryable
                .OrderByDescending(plannedMovement => plannedMovement.Amount)
                .ThenBy(plannedMovement => plannedMovement.Id),
            "amount asc" => queryable
                .OrderBy(plannedMovement => plannedMovement.Amount)
                .ThenBy(plannedMovement => plannedMovement.Id),
            _ => queryable
                .OrderByDescending(plannedMovement => plannedMovement.DueOn)
                .ThenBy(plannedMovement => plannedMovement.Id),
        };

        var plannedMovements = await queryable
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(plannedMovement => PlannedMovementDto.FromDomain(plannedMovement))
            .ToListAsync(cancellationToken);

        return new PaginatedList<PlannedMovementDto>(plannedMovements, totalItems, query.PageNumber, query.PageSize);
    }
}
