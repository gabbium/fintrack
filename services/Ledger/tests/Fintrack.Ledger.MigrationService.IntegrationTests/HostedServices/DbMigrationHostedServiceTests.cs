using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Containers;
using Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Infrastructure;
using Fintrack.Ledger.MigrationService.Services;

namespace Fintrack.Ledger.MigrationService.IntegrationTests.HostedServices;

public class DbMigrationHostedServiceTests(PostgresContainer postgres) : IClassFixture<PostgresContainer>
{
    [Fact]
    public async Task RunsMigrationsAndLeavesNoPending()
    {
        var host = HostBuilderFactory.BuildMigrationWorker(postgres.ConnectionString);
        await host.RunAsync();

        var dbOptions = new DbContextOptionsBuilder<LedgerDbContext>()
            .UseNpgsql(postgres.ConnectionString)
            .Options;

        using var dbContext = new LedgerDbContext(dbOptions, new IdentityService());

        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        pendingMigrations.ShouldBeEmpty();
    }

    [Fact]
    public async Task ThrowsArgumentExceptionWhenInvalidConnectionString()
    {
        var host = HostBuilderFactory.BuildMigrationWorker("invalid-connection-string");

        await Should.ThrowAsync<ArgumentException>(async () =>
        {
            await host.RunAsync();
        });
    }
}
