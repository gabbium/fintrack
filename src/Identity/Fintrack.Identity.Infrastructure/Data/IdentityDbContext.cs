using Fintrack.Identity.Application.Interfaces;
using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Infrastructure.Data;

internal sealed class IdentityDbContext(
    DbContextOptions<IdentityDbContext> options)
    : DbContext(options), IIdentityUnitOfWork
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

