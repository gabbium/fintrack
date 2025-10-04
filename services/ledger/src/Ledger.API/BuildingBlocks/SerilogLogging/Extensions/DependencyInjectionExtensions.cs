using Serilog;

namespace Ledger.API.BuildingBlocks.SerilogLogging.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddCustomSerilog(
        this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((sp, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration);

            var otlpExporterEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

            if (!string.IsNullOrWhiteSpace(otlpExporterEndpoint))
            {
                loggerConfiguration.WriteTo.OpenTelemetry(otlpExporterEndpoint);
            }
        });

        return builder;
    }
}
