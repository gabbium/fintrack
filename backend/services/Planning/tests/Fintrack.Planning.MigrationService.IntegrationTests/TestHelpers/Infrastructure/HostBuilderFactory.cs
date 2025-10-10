using Fintrack.Planning.Application.Interfaces;
using Fintrack.Planning.Infrastructure;
using Fintrack.Planning.MigrationService.Services;

namespace Fintrack.Planning.MigrationService.IntegrationTests.TestHelpers.Infrastructure;

public static class HostBuilderFactory
{
    public static IHost BuildMigrationWorker(string connectionString)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddDbContext<PlanningDbContext>(o => o.UseNpgsql(connectionString));

                services.AddMigration<PlanningDbContext>();

                services.AddTransient<IIdentityService, IdentityService>();
            })
            .Build();
    }
}

