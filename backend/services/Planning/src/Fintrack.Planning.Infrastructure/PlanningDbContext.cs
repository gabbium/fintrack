using Fintrack.Planning.Application.Interfaces;

namespace Fintrack.Planning.Infrastructure;

public sealed class PlanningDbContext(
    DbContextOptions<PlanningDbContext> options,
    IIdentityService identityService)
    : DbContext(options), IUnitOfWork
{
    private readonly Guid _userId = identityService.GetUserIdentity();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("planning");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

