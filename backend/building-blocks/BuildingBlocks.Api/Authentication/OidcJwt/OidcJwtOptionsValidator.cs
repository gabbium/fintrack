namespace BuildingBlocks.Api.Authentication.OidcJwt;

public sealed class OidcJwtOptionsValidator : IValidateOptions<OidcJwtOptions>
{
    public ValidateOptionsResult Validate(
        string? name,
        OidcJwtOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Authority))
            return ValidateOptionsResult.Fail("OIDC JWT authority is required");

        if (string.IsNullOrWhiteSpace(options.Audience))
            return ValidateOptionsResult.Fail("OIDC JWT audience is required");

        return ValidateOptionsResult.Success;
    }
}
