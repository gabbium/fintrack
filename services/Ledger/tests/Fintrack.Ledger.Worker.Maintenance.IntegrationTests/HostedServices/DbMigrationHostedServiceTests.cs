using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.Worker.Maintenance.IntegrationTests.TestHelpers.Containers;
using Fintrack.Ledger.Worker.Maintenance.IntegrationTests.TestHelpers.Infrastructure;
using Fintrack.Ledger.Worker.Maintenance.Services;

namespace Fintrack.Ledger.Worker.Maintenance.IntegrationTests.HostedServices;

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
