using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.MigrationService.Workers;

namespace Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Infrastructure.Hosting;

public static class HostBuilderFactory
{
    public static IHost BuildMigrationWorker(string connectionString)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContext<LedgerDbContext>(o => o.UseNpgsql(connectionString));

                services.AddHostedService<MigrationWorker>();
            })
            .Build();
    }
}
