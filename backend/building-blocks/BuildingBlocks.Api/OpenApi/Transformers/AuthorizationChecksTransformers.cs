using BuildingBlocks.Api.Authentication.OidcJwt;

namespace BuildingBlocks.Api.OpenApi.Transformers;

internal sealed class AuthorizationChecksTransformers : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var oidcJwtOptions = context.ApplicationServices.GetRequiredService<IOptions<OidcJwtOptions>>();

        var metadata = context.Description.ActionDescriptor.EndpointMetadata;

        if (!metadata.OfType<IAuthorizeData>().Any())
            return Task.CompletedTask;

        operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });

        var oAuthScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
        };

        operation.Security =
        [
            new()
            {
                [oAuthScheme] = oidcJwtOptions.Value.Scopes
            },
        ];

        return Task.CompletedTask;
    }
}

