namespace Fintrack.ServiceDefaults.OpenApi;

public sealed class OpenApiOptions
{
    public const string SectionName = "OpenApi";

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}
