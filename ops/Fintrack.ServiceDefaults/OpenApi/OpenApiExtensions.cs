using Fintrack.ServiceDefaults.OpenApi.Transformers;

namespace Fintrack.ServiceDefaults.OpenApi;

public static class OpenApiExtensions
{
    public static IHostApplicationBuilder AddOpenApiWithTransformers(
        this IHostApplicationBuilder builder,
        string[] versions)
    {
        builder.Services
            .AddSingleton<IValidateOptions<OpenApiOptions>, OpenApiOptionsValidator>()
            .AddOptions<OpenApiOptions>()
            .BindConfiguration(OpenApiOptions.SectionName)
            .ValidateOnStart();

        foreach (var documentName in versions)
        {
            builder.Services.AddOpenApi(
                documentName,
                options =>
                {
                    options.AddDocumentTransformer<OpenApiVersioningDocumentTransformer>();
                    options.AddOperationTransformer<AuthorizationChecksTransformers>();
                    options.AddDocumentTransformer<SecuritySchemeDocumentTransformer>();
                }
            );
        }

        return builder;
    }

    public static WebApplication MapOpenApiAndScalar(
        this WebApplication app)
    {
        app.MapOpenApi();

        app.MapScalarApiReference(scalarOptions =>
        {
            scalarOptions.Theme = ScalarTheme.Mars;
            scalarOptions.DefaultFonts = false;
            scalarOptions.AddPreferredSecuritySchemes("oauth2");
            scalarOptions.AddAuthorizationCodeFlow("oauth2", flow =>
            {
                flow.WithClientId("scalar");
                flow.WithPkce(Pkce.Sha256);
            });
            scalarOptions.WithPersistentAuthentication();
        });

        app.MapGet("/", () => Results.Redirect("/scalar/v1"));

        return app;
    }
}
