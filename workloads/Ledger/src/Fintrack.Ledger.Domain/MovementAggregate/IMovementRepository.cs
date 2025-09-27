namespace Fintrack.Ledger.Domain.MovementAggregate;

public interface IMovementRepository
{
    Task AddAsync(Movement movement, CancellationToken cancellationToken = default);
}

