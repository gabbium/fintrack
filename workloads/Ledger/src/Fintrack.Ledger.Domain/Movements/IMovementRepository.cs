namespace Fintrack.Ledger.Domain.Movements;

public interface IMovementRepository
{
    Task AddAsync(Movement movement, CancellationToken cancellationToken = default);
}

