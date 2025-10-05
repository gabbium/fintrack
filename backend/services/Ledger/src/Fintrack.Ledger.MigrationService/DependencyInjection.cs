using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.MigrationService.HostedServices;
using Fintrack.Ledger.MigrationService.Services;

namespace Fintrack.Ledger.MigrationService;

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
