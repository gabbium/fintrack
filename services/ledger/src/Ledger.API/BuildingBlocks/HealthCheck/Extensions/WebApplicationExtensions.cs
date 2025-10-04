using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ledger.API.BuildingBlocks.HealthCheck.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication MapDefaultHealthChecks(this WebApplication app)
    {
        var healthChecks = app.MapGroup("health");

        healthChecks.MapHealthChecks("/ready", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        healthChecks.MapHealthChecks("/alive", new HealthCheckOptions
        {
            Predicate = r => r.Tags.Contains("live"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
