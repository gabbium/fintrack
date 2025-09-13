namespace Fintrack.API.Infrastructure.Extensions;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        var openApi = builder.Configuration.GetRequiredSection("OpenApi");

        var title = openApi.GetRequiredValue("Document:Title");
        var description = openApi.GetRequiredValue("Document:Description");

        builder.Services.AddOpenApi("v1", options =>
        {
            options.ApplyApiVersionInfo(title, description);
            options.ApplySecuritySchemeDefinitions();
        });

        return builder;
    }

    public static IApplicationBuilder MapOpenApiEndpoints(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.DefaultFonts = false;
            options.AddPreferredSecuritySchemes("Bearer");
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

        return app;
    }
}

