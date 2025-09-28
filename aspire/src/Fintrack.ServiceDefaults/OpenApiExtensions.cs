namespace Fintrack.ServiceDefaults;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        var openApiSection = builder.Configuration.GetRequiredSection("OpenApi");
        var title = openApiSection.GetRequiredValue("Document:Title");
        var description = openApiSection.GetRequiredValue("Document:Description");

        builder.Services.AddOpenApi("v1", options =>
        {
            options.ApplyApiVersionInfo(title, description);
            options.ApplyOAuth2Keycloak(builder.Configuration);
        });

        return builder;
    }

    public static WebApplication MapOpenApiEndpoints(this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.DefaultFonts = false;
            options.AddPreferredSecuritySchemes("oauth2");
            options.AddAuthorizationCodeFlow("oauth2", flow =>
            {
                flow.ClientId = "swagger";
                flow.Pkce = Pkce.Sha256;
            });
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

        return app;
    }
}
