using BuildingBlocks.Api.ApiVersioning;
using BuildingBlocks.Api.Authentication;
using BuildingBlocks.Api.Authentication.Identity;
using BuildingBlocks.Api.OpenApi;
using BuildingBlocks.Api.ProblemDetail;
using BuildingBlocks.Application.Identity;

namespace BuildingBlocks.Api;

public static class Extensions
{
    public static IHostApplicationBuilder AddApíDefaults(this IHostApplicationBuilder builder, Assembly assembly)
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

        builder.Services.AddMinimalApisFromAssembly(assembly);

        return builder;
    }

    public static WebApplication MapApiDefaultEndpoints(this WebApplication app)
    {
        app.UseExceptionHandler();

        app.MapGroup("/api/v{version:apiVersion}")
            .WithApiVersionSet(app.NewApiVersionSet().ReportApiVersions().Build())
            .MapMinimalApis();

        app.MapOpenApiAndScalar();

        return app;
    }
}
