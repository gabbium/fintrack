namespace CleanArch.Persistence.EFCore;

public sealed class MigrationSeedWorker<TContext>(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime lifetime,
    ILogger<MigrationSeedWorker<TContext>> logger)
    : IHostedService
    where TContext : DbContext
{
    private static readonly ActivitySource s_activitySource = new(MigrationExtensions.ActivitySourceName);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        using var scope = serviceProvider.CreateScope();
        var scopeServiceProvider = scope.ServiceProvider;
        var seeder = scope.ServiceProvider.GetService<IDataSeeder<TContext>>();
        var context = scopeServiceProvider.GetRequiredService<TContext>();

        try
        {
            await RunMigrationAsync(context, cancellationToken);
            await SeedDataAsync(context, seeder, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddTag("exception.type", ex.GetType().FullName);
            activity?.AddTag("exception.message", ex.Message);
            activity?.AddTag("exception.stacktrace", ex.ToString());
            activity?.SetStatus(ActivityStatusCode.Error);

            throw;
        }
        finally
        {
            lifetime.StopApplication();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task RunMigrationAsync(
        TContext context,
        CancellationToken cancellationToken)
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);

        if (!pendingMigrations.Any())
        {
            logger.LogInformation("No pending migrations found for {DbContext}. Skipping migration.", typeof(TContext).Name);
            return;
        }

        logger.LogInformation("Migrating database associated with {DbContext}", typeof(TContext).Name);

        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await context.Database.MigrateAsync(cancellationToken);
        });

        logger.LogInformation("Database migration completed for {DbContext}.", typeof(TContext).Name);
    }

    private async Task SeedDataAsync(
        TContext context,
        IDataSeeder<TContext>? seeder,
        CancellationToken cancellationToken)
    {
        if (seeder is null)
        {
            logger.LogInformation("No data seeder found for {DbContext}. Skipping seeding.", typeof(TContext).Name);
            return;
        }

        logger.LogInformation("Seeding database associated with {DbContext}", typeof(TContext).Name);

        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await seeder.SeedAsync(context, cancellationToken);
        });

        logger.LogInformation("Database seeding completed for {DbContext}.", typeof(TContext).Name);
    }
}
