using BuildingBlocks.Application.Identity;
using Fintrack.Planning.Infrastructure;

namespace Fintrack.Planning.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMigration<PlanningDbContext>();

        builder.Services.AddTransient<IIdentityService, EmptyIdentityService>();

        return builder;
    }
}
