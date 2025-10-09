namespace BuildingBlocks.MigrationService;

public static class MigrationExtensions
{
    public static readonly string ActivitySourceName = "BuildingBlocks.MigrationService";

    public static IHostApplicationBuilder AddMigration<TDbContext>(this IHostApplicationBuilder builder)
        where TDbContext : DbContext
    {
        builder.Services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(ActivitySourceName));

        builder.Services.AddHostedService<MigrationSeedWorker<TDbContext>>();

        return builder;
    }
}
