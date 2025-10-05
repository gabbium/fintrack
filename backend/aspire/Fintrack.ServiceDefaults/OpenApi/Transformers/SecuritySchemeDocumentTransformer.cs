using Fintrack.ServiceDefaults.Authentication.OidcJwt;

namespace Fintrack.ServiceDefaults.OpenApi.Transformers;

internal sealed class SecuritySchemeDocumentTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var oidcJwtOptions = context.ApplicationServices.GetRequiredService<IOptions<OidcJwtOptions>>().Value;

        document.Components ??= new();

        var scopesDictionary = oidcJwtOptions.OpenApiScopes.ToDictionary(
            scope => scope,
            scope => $"Access to {scope}"
        );

        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{oidcJwtOptions.Authority}/protocol/openid-connect/auth"),
                    TokenUrl = new Uri($"{oidcJwtOptions.Authority}/protocol/openid-connect/token"),
                    Scopes = scopesDictionary,
                },
            },
        };

        document.Components.SecuritySchemes.Add("oauth2", securityScheme);

        return Task.CompletedTask;
    }
}

