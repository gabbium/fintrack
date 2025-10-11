using BuildingBlocks.Application.Identity;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Infrastructure;

public sealed class PlanningDbContext(
    DbContextOptions<PlanningDbContext> options,
    IIdentityService identityService)
    : DbContext(options), IUnitOfWork
{
    private readonly Guid _userId = identityService.GetUserIdentity();

    public DbSet<PlannedMovement> PlannedMovements => Set<PlannedMovement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("planning");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PlannedMovement>()
            .HasQueryFilter(pm => pm.UserId == _userId);
    }
}

