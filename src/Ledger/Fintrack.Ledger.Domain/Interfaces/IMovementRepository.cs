using Fintrack.Ledger.Domain.Entities;

namespace Fintrack.Ledger.Domain.Interfaces;

public interface IMovementRepository
{
    Task<Movement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task AddAsync(Movement movement, CancellationToken cancellationToken = default);
    Task UpdateAsync(Movement movement, CancellationToken cancellationToken = default);
    Task RemoveAsync(Movement movement, CancellationToken cancellationToken = default);
}
