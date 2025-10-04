using Fintrack.ServiceName.Application.Interfaces;
using Fintrack.ServiceName.Worker.Maintenance.HostedServices;
using Fintrack.ServiceName.Worker.Maintenance.Services;

namespace Fintrack.ServiceName.Worker.Maintenance;

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
