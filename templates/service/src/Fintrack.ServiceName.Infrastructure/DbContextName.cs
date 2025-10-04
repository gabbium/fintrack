using Fintrack.ServiceName.Application.Interfaces;

namespace Fintrack.ServiceName.Infrastructure;

public sealed class DbContextName(
    DbContextOptions<DbContextName> options,
    IIdentityService identityService)
    : DbContext(options), IUnitOfWork
{
    private readonly Guid _userId = identityService.GetUserIdentity();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("DbSchemaName");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

