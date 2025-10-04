using Ledger.API.BuildingBlocks.OpenApi;
using Ledger.API.BuildingBlocks.OpenApi.Transformers;

namespace Ledger.API.BuildingBlocks.OpenApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static IHostApplicationBuilder AddCustomOpenApi(this IHostApplicationBuilder builder, string[] versions)
    {
        builder.Services
            .AddOptions<OpenApiOptions>()
            .BindConfiguration(typeof(OpenApiOptions).Name);

        foreach (var documentName in versions)
        {
            builder.Services.AddOpenApi(
                documentName,
                options =>
                {
                    options.AddDocumentTransformer<OpenApiVersioningDocumentTransformer>();
                }
            );
        }

        return builder;
    }
}
