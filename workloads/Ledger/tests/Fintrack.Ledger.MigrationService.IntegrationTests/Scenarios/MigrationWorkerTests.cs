using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Infrastructure.Containers;
using Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Infrastructure.Hosting;

namespace Fintrack.Ledger.MigrationService.IntegrationTests.Scenarios;

public class MigrationWorkerTests(PostgresContainer postgres) : IClassFixture<PostgresContainer>
{
    [Fact]
    public async Task RunsMigrationsAndLeavesNoPending()
    {
        var host = HostBuilderFactory.BuildMigrationWorker(postgres.ConnectionString);
        await host.RunAsync(TestContext.Current.CancellationToken);

        var dbOptions = new DbContextOptionsBuilder<LedgerDbContext>()
            .UseNpgsql(postgres.ConnectionString)
            .Options;

        using var dbContext = new LedgerDbContext(dbOptions);

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(TestContext.Current.CancellationToken);
        pendingMigrations.ShouldBeEmpty();
    }

    [Fact]
    public async Task ThrowsArgumentExceptionWhenInvalidConnectionString()
    {
        var host = HostBuilderFactory.BuildMigrationWorker("invalid-connection-string");

        await Should.ThrowAsync<ArgumentException>(async () =>
        {
            await host.RunAsync(TestContext.Current.CancellationToken);
        });
    }
}
