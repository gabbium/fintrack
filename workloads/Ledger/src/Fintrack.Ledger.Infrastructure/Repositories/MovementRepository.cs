using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Repositories;

public sealed class MovementRepository(LedgerDbContext context) : IMovementRepository
{
    public async Task<Movement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Movements
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task AddAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        await context.Movements.AddAsync(movement, cancellationToken);
    }

    public Task UpdateAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        context.Movements.Update(movement);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        context.Movements.Remove(movement);
        return Task.CompletedTask;
    }
}

