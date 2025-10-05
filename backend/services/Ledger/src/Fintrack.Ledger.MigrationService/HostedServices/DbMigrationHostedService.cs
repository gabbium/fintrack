using Fintrack.Ledger.Infrastructure;

namespace Fintrack.Ledger.MigrationService.HostedServices;

public sealed class DbMigrationHostedService(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Fintrack.Ledger.MigrationService";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        using var scope = serviceProvider.CreateScope();
        var scopeServices = scope.ServiceProvider;
        var logger = scopeServices.GetRequiredService<ILogger<LedgerDbContext>>();
        var dbContext = scopeServices.GetRequiredService<LedgerDbContext>();

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContext}", typeof(LedgerDbContext).Name);

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await dbContext.Database.MigrateAsync(stoppingToken);
            });

            logger.LogInformation("Database migration completed for {DbContext}.", typeof(LedgerDbContext).Name);
        }
        catch (Exception ex)
        {
            activity?.AddTag("exception.type", ex.GetType().FullName);
            activity?.AddTag("exception.message", ex.Message);
            activity?.AddTag("exception.stacktrace", ex.ToString());
            activity?.SetStatus(ActivityStatusCode.Error);

            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContext}", typeof(LedgerDbContext).Name);

            throw;
        }

        hostApplicationLifetime.StopApplication();
    }
}

