using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Infrastructure;

public sealed class LedgerDbContext(
    DbContextOptions<LedgerDbContext> options,
    IIdentityService identityService)
    : DbContext(options), IUnitOfWork
{
    private readonly Guid _userId = identityService.GetUserIdentity();

    public DbSet<Movement> Movements => Set<Movement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ledger");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Movement>()
            .HasQueryFilter(m => m.UserId == _userId);
    }
}
