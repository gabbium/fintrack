using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Domain.Movements;

namespace Fintrack.Ledger.Infrastructure.Data;

public class LedgerDbContext(
    DbContextOptions<LedgerDbContext> options,
    IUser user)
    : DbContext(options), IUnitOfWork
{
    private readonly Guid _userId = user.UserId;

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

