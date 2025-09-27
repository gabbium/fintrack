using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure.Data;

public class LedgerDbContext(
    DbContextOptions<LedgerDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Movement> Movements => Set<Movement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ledger");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Movement>();

        base.OnModelCreating(modelBuilder);
    }
}

