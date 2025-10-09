using BuildingBlocks.MigrationService.HostedServices;
using Fintrack.ServiceName.Application.Interfaces;
using Fintrack.ServiceName.Infrastructure;
using Fintrack.ServiceName.MigrationService.Services;

namespace Fintrack.ServiceName.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<DbMigrationHostedService<DbContextName>>();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        return builder;
    }
}
