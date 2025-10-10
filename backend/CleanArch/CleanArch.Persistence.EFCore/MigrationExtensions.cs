namespace CleanArch.Persistence.EFCore;

public static class MigrationExtensions
{
    public static readonly string ActivitySourceName = "DbMigrations";

    public static IServiceCollection AddMigration<TContext>(
        this IServiceCollection services)
        where TContext : DbContext
    {
        services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(ActivitySourceName));

        services.AddHostedService<MigrationSeedWorker<TContext>>();

        return services;
    }

    public static IServiceCollection AddMigration<TContext, TDbSeeder>(
        this IServiceCollection services)
        where TContext : DbContext
        where TDbSeeder : class, IDataSeeder<TContext>
    {
        services.AddScoped<IDataSeeder<TContext>, TDbSeeder>();

        services.AddOpenTelemetry()
            .WithTracing(tracing => tracing.AddSource(ActivitySourceName));

        services.AddHostedService<MigrationSeedWorker<TContext>>();

        return services;
    }
}
