using Fintrack.Ledger.Domain.Movements;
using Fintrack.Ledger.Infrastructure.Data;

namespace Fintrack.Ledger.MigrationService.Workers;

public class MigrationWorker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LedgerDbContext>();

            await RunMigrationAsync(dbContext, stoppingToken);
            await SeedDataAsync(dbContext, stoppingToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(LedgerDbContext dbContext, CancellationToken stoppingToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.MigrateAsync(stoppingToken);
        });
    }

    private static async Task SeedDataAsync(LedgerDbContext dbContext, CancellationToken stoppingToken)
    {
        if (await dbContext.Movements.AnyAsync(stoppingToken))
        {
            return;
        }

        var movements = new List<Movement>
        {
            new(Guid.NewGuid(), MovementKind.Income, 2500.50m, "Salary", DateTimeOffset.UtcNow.AddDays(-10)),
            new(Guid.NewGuid(), MovementKind.Expense, 150.75m, "Groceries", DateTimeOffset.UtcNow.AddDays(-7)),
            new(Guid.NewGuid(), MovementKind.Expense, 89.99m, "Restaurant", DateTimeOffset.UtcNow.AddDays(-2)),
            new(Guid.NewGuid(), MovementKind.Income, 300.00m, "Freelance", DateTimeOffset.UtcNow.AddDays(-1)),
        };

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(stoppingToken);
            await dbContext.Movements.AddRangeAsync(movements, stoppingToken);
            await dbContext.SaveChangesAsync(stoppingToken);
            await transaction.CommitAsync(stoppingToken);
        });
    }
}
