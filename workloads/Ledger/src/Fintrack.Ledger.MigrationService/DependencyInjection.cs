namespace Fintrack.Ledger.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<Worker>();

        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

        return builder;
    }
}
