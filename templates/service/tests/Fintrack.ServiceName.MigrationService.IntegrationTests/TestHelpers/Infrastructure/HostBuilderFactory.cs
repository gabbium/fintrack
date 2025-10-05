using Fintrack.ServiceName.Application.Interfaces;
using Fintrack.ServiceName.Infrastructure;
using Fintrack.ServiceName.MigrationService.HostedServices;
using Fintrack.ServiceName.MigrationService.Services;

namespace Fintrack.ServiceName.MigrationService.IntegrationTests.TestHelpers.Infrastructure;

public static class HostBuilderFactory
{
    public static IHost BuildMigrationWorker(string connectionString)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContext<DbContextName>(o => o.UseNpgsql(connectionString));

                services.AddHostedService<DbMigrationHostedService>();

                services.AddTransient<IIdentityService, IdentityService>();
            })
            .Build();
    }
}

