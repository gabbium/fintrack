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

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContext}", typeof(PlanningDbContext).Name);

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await dbContext.Database.MigrateAsync(stoppingToken);
            });

            logger.LogInformation("Database migration completed for {DbContext}.", typeof(PlanningDbContext).Name);
        }
        catch (Exception ex)
        {
            activity?.AddTag("exception.type", ex.GetType().FullName);
            activity?.AddTag("exception.message", ex.Message);
            activity?.AddTag("exception.stacktrace", ex.ToString());
            activity?.SetStatus(ActivityStatusCode.Error);

            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContext}", typeof(PlanningDbContext).Name);

            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}

