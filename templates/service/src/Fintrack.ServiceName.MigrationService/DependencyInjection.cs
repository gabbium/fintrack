using Fintrack.ServiceName.Application.Interfaces;
using Fintrack.ServiceName.MigrationService.HostedServices;
using Fintrack.ServiceName.MigrationService.Services;

namespace Fintrack.ServiceName.MigrationService;

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
