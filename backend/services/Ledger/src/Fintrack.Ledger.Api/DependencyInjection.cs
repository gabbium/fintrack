using BuildingBlocks.Api.ApiVersioning;
using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.Authentication.Identity;
using BuildingBlocks.Api.OpenApi;
using BuildingBlocks.Api.ProblemDetail;
using BuildingBlocks.Application.Identity;

namespace Fintrack.Ledger.Api;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddApiVersioningAndExplorer();

        builder.AddOidcJwtAuthentication();

        builder.AddOpenApiWithTransformers(["v1"]);

        builder.Services.AddExceptionHandler<DefaultExceptionHandler>();

        builder.Services.AddProblemDetails();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        builder.Services.AddMinimalApisFromAssembly(Assembly.GetExecutingAssembly());

        return builder;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseExceptionHandler();

        app.MapGroup("/api/v{version:apiVersion}")
            .WithApiVersionSet(app.NewApiVersionSet().ReportApiVersions().Build())
            .MapMinimalApis();

        app.MapOpenApiAndScalar();

        return app;
    }
}
