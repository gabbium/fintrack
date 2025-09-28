using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure;

public class LedgerDbContext(
    DbContextOptions<LedgerDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Movement> Movements => Set<Movement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ledger");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
