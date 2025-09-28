using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.MigrationService.Services;
using Fintrack.Ledger.MigrationService.Workers;

namespace Fintrack.Ledger.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<MigrationWorker>();

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(MigrationWorker.ActivitySourceName));

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        return builder;
    }
}
