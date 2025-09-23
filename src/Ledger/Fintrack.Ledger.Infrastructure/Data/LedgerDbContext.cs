using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Domain.Entities;

namespace Fintrack.Ledger.Infrastructure.Data;

public class LedgerDbContext(
    DbContextOptions<LedgerDbContext> options,
    IUser user)
    : DbContext(options), ILedgerUnitOfWork
{
    private readonly Guid _userId = user.Id;

    public DbSet<Movement> Movements => Set<Movement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ledger");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Movement>()
            .HasQueryFilter(m => m.UserId == _userId);

        base.OnModelCreating(modelBuilder);
    }
}

