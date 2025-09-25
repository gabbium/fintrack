namespace Fintrack.ServiceDefaults;

public static class ApiVersioningExtensions
{
    public static IHostApplicationBuilder AddApiVersioningDefaults(this IHostApplicationBuilder builder)
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
