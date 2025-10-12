using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Infrastructure.Repositories;

public sealed class PlannedMovementRepository(PlanningDbContext context) : IPlannedMovementRepository
{
    public async Task<PlannedMovement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.PlannedMovements
            .AsNoTracking()
            .FirstOrDefaultAsync(plannedMovement => plannedMovement.Id == id, cancellationToken);
    }

    public async Task AddAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default)
    {
        await context.PlannedMovements.AddAsync(plannedMovement, cancellationToken);
    }

    public Task UpdateAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default)
    {
        context.PlannedMovements.Update(plannedMovement);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default)
    {
        context.PlannedMovements.Remove(plannedMovement);
        return Task.CompletedTask;
    }
}
