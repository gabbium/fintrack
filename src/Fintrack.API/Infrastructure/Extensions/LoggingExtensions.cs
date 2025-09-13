namespace Fintrack.API.Infrastructure.Extensions;

public static class LoggingExtensions
{
    public static IHostApplicationBuilder AddDefaultLogging(this IHostApplicationBuilder builder)
    {
        builder.Services.AddSerilog((_, config) =>
        {
            config.ReadFrom.Configuration(builder.Configuration)
               .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                   .WithDefaultDestructurers()
                   .WithDestructurers([new DbUpdateExceptionDestructurer()]));
        });

        return builder;
    }
}
