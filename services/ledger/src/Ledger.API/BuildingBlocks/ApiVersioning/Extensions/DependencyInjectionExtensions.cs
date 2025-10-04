using Asp.Versioning;

namespace Ledger.API.BuildingBlocks.ApiVersioning.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddCustomVersioning(
        this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return builder;
    }
}
