using Fintrack.Planning.Api.Services;
using Fintrack.Planning.Application.Interfaces;
using Fintrack.ServiceDefaults.ApiVersioning;
using Fintrack.ServiceDefaults.Authentication;
using Fintrack.ServiceDefaults.OpenApi;

namespace Fintrack.Planning.Api;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddApiVersioningAndExplorer();

        builder.AddOidcJwtAuthentication();

        builder.AddOpenApiWithTransformers(["v1"]);

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        return builder;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapGroup("/api/v{version:apiVersion}")
            .WithApiVersionSet(app.NewApiVersionSet().ReportApiVersions().Build())
            .MapApis(Assembly.GetExecutingAssembly());

        app.MapOpenApiAndScalar();

        return app;
    }
}
