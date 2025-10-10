using Fintrack.ServiceDefaults.HealthCheck;
using Fintrack.ServiceDefaults.OpenTelemetry;
using Fintrack.ServiceDefaults.ProblemDetail;
using Fintrack.ServiceDefaults.SerilogLogging;

namespace Fintrack.ServiceDefaults;

public static class Extensions
{
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.AddBasicServiceDefaults();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

        builder.Services.AddProblemDetails();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(http =>
        {
            http.AddStandardResilienceHandler();
            http.AddServiceDiscovery();
        });

        return builder;
    }

    public static IHostApplicationBuilder AddBasicServiceDefaults(this IHostApplicationBuilder builder)
    {
        builder.AddDefaultSerilog();

        builder.AddDefaultOpenTelemetry();

        builder.AddDefaultHealthChecks();

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        app.MapDefaultSerilog();

        app.MapDefaultHealthChecks();

        app.UseExceptionHandler();

        return app;
    }
}
