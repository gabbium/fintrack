using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.Worker.Maintenance.HostedServices;
using Fintrack.Ledger.Worker.Maintenance.Services;

namespace Fintrack.Ledger.Worker.Maintenance.IntegrationTests.TestHelpers.Infrastructure;

public static class HostBuilderFactory
{
    public static IHost BuildMigrationWorker(string connectionString)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContext<LedgerDbContext>(o => o.UseNpgsql(connectionString));

                services.AddHostedService<DbMigrationHostedService>();

                services.AddTransient<IIdentityService, IdentityService>();
            })
            .Build();
    }
}

