using BuildingBlocks.Application.Identity;
using BuildingBlocks.Infrastructure.EventBus;
using Fintrack.Planning.Domain.PlannedMovementAggregate;
using Fintrack.Planning.Infrastructure.Configurations;

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
        modelBuilder.UseIntegrationEventLogs();

        modelBuilder.ApplyConfiguration(new PlannedMovementConfiguration());

        modelBuilder.Entity<PlannedMovement>()
            .HasQueryFilter(plannedMovement => plannedMovement.UserId == _userId);
    }
}

