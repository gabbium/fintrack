namespace Fintrack.Planning.Domain.PlannedMovementAggregate;

public interface IPlannedMovementRepository : IRepository<PlannedMovement>
{
    Task<PlannedMovement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default);
    Task UpdateAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default);
    Task RemoveAsync(PlannedMovement plannedMovement, CancellationToken cancellationToken = default);
}
