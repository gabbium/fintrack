namespace Fintrack.ServiceDefaults.Authentication.OidcJwt;

public sealed class OidcJwtOptions
{
    public const string SectionName = "Authentication:OidcJwt";

    public string Authority { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public IList<string> Scopes { get; set; } = [];
    public IList<string> OpenApiScopes { get; set; } = [];
}
