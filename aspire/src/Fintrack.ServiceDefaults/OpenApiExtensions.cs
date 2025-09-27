namespace Fintrack.ServiceDefaults;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddOpenApiDefaults(this IHostApplicationBuilder builder)
    {
        var openApi = builder.Configuration.GetSection("OpenApi");

        var title = openApi["Document:Title"];
        var description = openApi["Document:Description"];

        builder.Services.AddOpenApi("v1", options =>
        {
            options.ApplyApiVersionInfo(title, description);
        });

        return builder;
    }

    public static WebApplication MapOpenApiEndpoints(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.DefaultFonts = false;
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

        return app;
    }
}
