using BuildingBlocks.Application.Identity;
using Fintrack.Ledger.Domain.MovementAggregate;
using Fintrack.Ledger.Infrastructure.Configurations;

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
        modelBuilder.ApplyConfiguration(new MovementConfiguration());

        modelBuilder.Entity<Movement>()
            .HasQueryFilter(movement => movement.UserId == _userId);
    }
}

