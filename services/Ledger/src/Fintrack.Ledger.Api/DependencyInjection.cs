using Fintrack.ServiceDefaults.ApiVersioning;
using Fintrack.ServiceDefaults.Authentication;
using Fintrack.ServiceDefaults.OpenApi;

namespace Fintrack.Ledger.Api;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddApiVersioningAndExplorer();

        builder.AddOidcJwtAuthentication();

        builder.AddOpenApiWithTransformers(["v1"]);

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
