namespace Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

public interface IMovementRepository
{
    Task AddAsync(Movement movement, CancellationToken cancellationToken = default);
}

