namespace Fintrack.ServiceDefaults;

public static class HealthCheckExtensions
{
    public static IHostApplicationBuilder AddHealthCheckDefaults(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }

    public static WebApplication MapHealthCheckEndpoints(this WebApplication app)
    {
        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapHealthChecks("/health/alive", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
