using Serilog.Sinks.OpenTelemetry;

namespace Fintrack.ServiceDefaults;

public static class LoggingExtensions
{
    public static IHostApplicationBuilder ConfigureSerilog(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((_, config) =>
        {
            config.ReadFrom.Configuration(builder.Configuration)
               .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                   .WithDefaultDestructurers()
                   .WithDestructurers([new DbUpdateExceptionDestructurer()]));

            var otlpExporterEndpoint = builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"];

            if (!string.IsNullOrWhiteSpace(otlpExporterEndpoint))
            {
                config.WriteTo.OpenTelemetry(otlpExporterEndpoint, OtlpProtocol.Grpc);
            }
        });

        return builder;
    }

    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing.AddSource(builder.Environment.ApplicationName)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            builder.Services.AddOpenTelemetry().UseOtlpExporter();
        }

        return builder;
    }
}
