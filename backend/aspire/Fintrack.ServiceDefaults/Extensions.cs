using Fintrack.ServiceDefaults.HealthCheck;
using Fintrack.ServiceDefaults.OpenTelemetry;
using Fintrack.ServiceDefaults.SerilogLogging;

namespace Fintrack.ServiceDefaults;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.AddDefaultSerilog();

        builder.AddDefaultOpenTelemetry();

        builder.AddDefaultHealthChecks();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static WebApplication MapServiceDefaultEndpoints(this WebApplication app)
    {
        app.MapDefaultSerilog();

        app.MapDefaultHealthChecks();

        return app;
    }
}
