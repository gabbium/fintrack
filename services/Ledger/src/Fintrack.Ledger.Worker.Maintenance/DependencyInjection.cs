using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Worker.Maintenance.HostedServices;
using Fintrack.Ledger.Worker.Maintenance.Services;

namespace Fintrack.Ledger.Worker.Maintenance;

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
