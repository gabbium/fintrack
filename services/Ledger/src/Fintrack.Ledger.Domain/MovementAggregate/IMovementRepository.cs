namespace Fintrack.Ledger.Domain.MovementAggregate;

public interface IMovementRepository : IRepository<Movement>
{
    Task<Movement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Movement movement, CancellationToken cancellationToken = default);
    Task UpdateAsync(Movement movement, CancellationToken cancellationToken = default);
    Task RemoveAsync(Movement movement, CancellationToken cancellationToken = default);
}
