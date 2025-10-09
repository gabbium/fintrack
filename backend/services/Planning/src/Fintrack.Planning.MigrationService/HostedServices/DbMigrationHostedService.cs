using Fintrack.Planning.Infrastructure;

namespace Fintrack.Planning.MigrationService.HostedServices;

public sealed class DbMigrationHostedService(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Fintrack.Planning.MigrationService";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        using var scope = serviceProvider.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var logger = scopeServices.GetRequiredService<ILogger<PlanningDbContext>>();
        var dbContext = scopeServices.GetRequiredService<PlanningDbContext>();
        var contextName = typeof(PlanningDbContext).Name;

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

