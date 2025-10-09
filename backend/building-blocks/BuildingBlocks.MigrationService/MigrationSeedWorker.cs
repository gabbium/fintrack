namespace BuildingBlocks.MigrationService;

public sealed class MigrationSeedWorker<TDbContext>(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime)
    : BackgroundService
    where TDbContext : DbContext
{
    private static readonly ActivitySource s_activitySource = new(MigrationExtensions.ActivitySourceName);

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        using var scope = serviceProvider.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var logger = scopeServices.GetRequiredService<ILogger<TDbContext>>();
        var dbContext = scopeServices.GetRequiredService<TDbContext>();
        var contextName = typeof(TDbContext).Name;

        try
        {
            logger.LogInformation("Migrating database associated with {DbContext}", contextName);

            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(stoppingToken);

            if (!pendingMigrations.Any())
            {
                logger.LogInformation("No pending migrations found for {DbContext}. Skipping migration.", contextName);
                return;
            }

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await dbContext.Database.MigrateAsync(stoppingToken);
            });

            logger.LogInformation("Database migration completed for {DbContext}.", contextName);
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
            hostApplicationLifetime.StopApplication();
        }
    }
}
