using Serilog;

namespace Ledger.API.BuildingBlocks.SerilogLogging.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddCustomSerilog(
        this IHostApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        builder.Services.AddSerilog((sp, loggerConfiguration) =>
        {
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration);
        });

        return builder;
    }
}
