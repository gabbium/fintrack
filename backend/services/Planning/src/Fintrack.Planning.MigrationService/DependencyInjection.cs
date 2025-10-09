using Fintrack.Planning.Application.Interfaces;
using Fintrack.Planning.MigrationService.HostedServices;
using Fintrack.Planning.MigrationService.Services;

namespace Fintrack.Planning.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<DbMigrationHostedService>();

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(DbMigrationHostedService.ActivitySourceName));

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        return builder;
    }
}
