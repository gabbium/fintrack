namespace Fintrack.ServiceDefaults.OpenApi.Transformers;

internal sealed class OpenApiVersioningDocumentTransformer(
    IApiVersionDescriptionProvider apiVersionDescriptionProvider,
    IOptions<OpenApiOptions> options)
    : IOpenApiDocumentTransformer
{
    private readonly OpenApiOptions _openApiOptions = options.Value;

    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var apiDescription = apiVersionDescriptionProvider.ApiVersionDescriptions
            .SingleOrDefault(description => description.GroupName == context.DocumentName);

        if (apiDescription is null)
        {
            return Task.CompletedTask;
        }

        document.Info.Version = apiDescription.ApiVersion.ToString();
        document.Info.Title = _openApiOptions.Title;
        document.Info.Description = _openApiOptions.Description;

        return Task.CompletedTask;
    }
}

