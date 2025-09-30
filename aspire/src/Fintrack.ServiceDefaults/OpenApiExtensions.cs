namespace Fintrack.ServiceDefaults;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddDefaultOpenApi(this IHostApplicationBuilder builder)
    {
        var openApi = builder.Configuration.GetSection("OpenApi");
        var identitySection = builder.Configuration.GetSection("Identity");

        var scopes = identitySection.GetSection("Scopes").GetChildren().ToDictionary(p => p.Key, p => p.Value);

        builder.Services.AddOpenApi("v1", options =>
        {
            options.ApplyApiVersionInfo(openApi.GetRequiredValue("Document:Title"), openApi.GetRequiredValue("Document:Description"));
            options.ApplyAuthorizationChecks([.. scopes.Keys]);
            options.ApplySecuritySchemeDefinitions();
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Servers = [];
                return Task.CompletedTask;
            });
        });

        return builder;
    }

    public static WebApplication MapOpenApiEndpoints(this WebApplication app)
    {
        var openApiSection = app.Configuration.GetSection("OpenApi");
        if (!openApiSection.Exists())
        {
            return app;
        }

        app.MapOpenApi();

        app.MapScalarApiReference(options =>
        {
            options.WithTheme(ScalarTheme.Mars);
            options.WithDefaultFonts(false);
            options.AddPreferredSecuritySchemes("oauth2");
            options.AddAuthorizationCodeFlow("oauth2", flow =>
            {
                flow.WithClientId("scalar");
                flow.WithPkce(Pkce.Sha256);
            });
            options.WithPersistentAuthentication();
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();

        return app;
    }
}
