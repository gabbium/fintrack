using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Repositories;

public sealed class MovementRepository(LedgerDbContext dbContext) : IMovementRepository
{
    public async Task<Movement?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await dbContext.Movements
            .AsNoTracking()
            .FirstOrDefaultAsync(movement => movement.Id == id, cancellationToken);
    }

    public async Task AddAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        await dbContext.Movements.AddAsync(movement, cancellationToken);
    }

    public Task UpdateAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        dbContext.Movements.Update(movement);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(Movement movement, CancellationToken cancellationToken = default)
    {
        dbContext.Movements.Remove(movement);
        return Task.CompletedTask;
    }
}
