using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Repositories;

public sealed class MovementRepository(LedgerDbContext context) : IMovementRepository
{
    public async Task AddAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        await context.Movements.AddAsync(movement, cancellationToken);
    }
}

