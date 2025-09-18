using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.Ledger.Infrastructure.Data;

public class LedgerDbContext(DbContextOptions<LedgerDbContext> options) : DbContext(options), ILedgerUnitOfWork
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ledger");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

