namespace CleanArch.Persistence.EFCore;

public interface IDataSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(
        TContext context,
        CancellationToken cancellationToken);
}
