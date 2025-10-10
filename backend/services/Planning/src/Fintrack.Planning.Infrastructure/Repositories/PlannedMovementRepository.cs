using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Infrastructure.Repositories;

public sealed class PlannedMovementRepository(PlanningDbContext dbContext) : IPlannedMovementRepository
{
    public async Task<PlannedMovement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.PlannedMovements
            .AsNoTracking()
            .FirstOrDefaultAsync(pm => pm.Id == id, cancellationToken);
    }

    public async Task AddAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default)
    {
        await dbContext.PlannedMovements.AddAsync(plannedMovement, cancellationToken);
    }

    public Task UpdateAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default)
    {
        dbContext.PlannedMovements.Update(plannedMovement);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default)
    {
        dbContext.PlannedMovements.Remove(plannedMovement);
        return Task.CompletedTask;
    }
}
