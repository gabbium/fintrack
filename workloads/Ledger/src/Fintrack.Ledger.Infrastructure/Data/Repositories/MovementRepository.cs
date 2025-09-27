using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Data.Repositories;

internal sealed class MovementRepository(LedgerDbContext context) : IMovementRepository
{
    public async Task AddAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        await context.Movements.AddAsync(movement, cancellationToken);
    }
}

