using BuildingBlocks.MigrationService.HostedServices;
using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.MigrationService.Services;

namespace Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Infrastructure;

public static class HostBuilderFactory
{
    public static IHost BuildMigrationWorker(string connectionString)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContext<LedgerDbContext>(o => o.UseNpgsql(connectionString));

                services.AddHostedService<DbMigrationHostedService<LedgerDbContext>>();

                services.AddTransient<IIdentityService, IdentityService>();
            })
            .Build();
    }
}

