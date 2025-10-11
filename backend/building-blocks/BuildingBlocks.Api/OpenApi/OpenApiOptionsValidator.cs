namespace BuildingBlocks.Api.OpenApi;

public sealed class OpenApiOptionsValidator : IValidateOptions<OpenApiOptions>
{
    public ValidateOptionsResult Validate(
        string? name,
        OpenApiOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Title))
            return ValidateOptionsResult.Fail("OpenApi title is required");

        if (string.IsNullOrWhiteSpace(options.Description))
            return ValidateOptionsResult.Fail("OpenApi description is required");

        return ValidateOptionsResult.Success;
    }
}
