namespace Fintrack.ServiceDefaults;

internal static class OpenApiOptionsExtensions
{
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
}
