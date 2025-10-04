using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;

namespace Ledger.API.BuildingBlocks.SerilogLogging.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddCustomSerilog(
        this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((sp, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
               .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                   .WithDefaultDestructurers()
                   .WithDestructurers([new DbUpdateExceptionDestructurer()]));

            var otlpExporterEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

            if (!string.IsNullOrWhiteSpace(otlpExporterEndpoint))
            {
                loggerConfiguration.WriteTo.OpenTelemetry(otlpExporterEndpoint);
            }
        });

        return builder;
    }
}
