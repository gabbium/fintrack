using Fintrack.ServiceName.Infrastructure;

namespace Fintrack.ServiceName.MigrationService.HostedServices;

public sealed class DbMigrationHostedService(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Fintrack.ServiceName.MigrationService";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        using var scope = serviceProvider.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var logger = scopeServices.GetRequiredService<ILogger<DbContextName>>();
        var dbContext = scopeServices.GetRequiredService<DbContextName>();
        var contextName = typeof(DbContextName).Name;

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContext}", contextName);

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

            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContext}", contextName);

            throw;
        }
        finally
        {
            hostApplicationLifetime.StopApplication();
        }
    }
}

