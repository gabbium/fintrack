using BuildingBlocks.MigrationService.HostedServices;
using Fintrack.Planning.Application.Interfaces;
using Fintrack.Planning.Infrastructure;
using Fintrack.Planning.MigrationService.Services;

namespace Fintrack.Planning.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<DbMigrationHostedService<PlanningDbContext>>();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        return builder;
    }
}
