namespace Fintrack.ServiceDefaults;

internal static class OpenApiOptionsExtensions
{
    public static OpenApiOptions ApplyAuthorizationChecks(this OpenApiOptions options)
    {
        options.AddOperationTransformer((operation, context, cancellationToken) =>
        {
            var metadata = context.Description.ActionDescriptor.EndpointMetadata;

            if (!metadata.OfType<IAuthorizeData>().Any())
            {
                return Task.CompletedTask;
            }

            operation.Responses.TryAdd("401", new OpenApiResponse
            {
                Description = "Unauthorized",
                Headers = new Dictionary<string, OpenApiHeader>
                {
                    ["Www-Authenticate"] = new()
                    {
                        Description = "Bearer challenge",
                        Schema = new OpenApiSchema { Type = "string" }
                    }
                }
            });

            return Task.CompletedTask;
        });
        return options;
    }

    public static OpenApiOptions ApplyApiVersionInfo(this OpenApiOptions options, string? title, string? description)
    {
        options.AddDocumentTransformer((document, context, cancellationToken) =>
        {
            var versionedDescriptionProvider = context.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
            var apiDescription = versionedDescriptionProvider?.ApiVersionDescriptions.SingleOrDefault(description => description.GroupName == context.DocumentName);

            if (apiDescription is not null)
            {
                document.Info.Title = title;
                document.Info.Version = apiDescription.ApiVersion.ToString();
                document.Info.Description = description;
            }

            return Task.CompletedTask;
        });

        return options;
    }

    public static OpenApiOptions ApplyOAuth2Keycloak(this OpenApiOptions options, IConfiguration configuration)
    {
        options.AddDocumentTransformer((doc, ctx, ct) =>
        {
            var openApiSection = configuration.GetRequiredSection("OpenApi");
            var oAuthUrl = openApiSection.GetRequiredValue("OAuth:Url");

            doc.Components ??= new OpenApiComponents();

            doc.Components.SecuritySchemes["oauth2"] = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{oAuthUrl}/protocol/openid-connect/auth"),
                        TokenUrl = new Uri($"{oAuthUrl}/protocol/openid-connect/token"),
                    }
                }
            };

            doc.SecurityRequirements.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            return Task.CompletedTask;
        });

        return options;
    }
}
