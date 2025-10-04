using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Ledger.API.BuildingBlocks.HealthCheck.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddDefaultHealthChecks(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }
}
