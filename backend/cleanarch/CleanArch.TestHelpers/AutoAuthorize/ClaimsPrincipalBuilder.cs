namespace CleanArch.TestHelpers.AutoAuthorize;

public sealed class ClaimsPrincipalBuilder
{
    private readonly List<Claim> _claims = [];

    public ClaimsPrincipalBuilder WithClaim(string type, string value)
    {
        _claims.Add(new Claim(type, value));
        return this;
    }

    public ClaimsPrincipal Build()
    {
        var id = new ClaimsIdentity(authenticationType: "TestAuth");

        foreach (var claim in _claims)
        {
            id.AddClaim(claim);
        }

        return new ClaimsPrincipal(id);
    }
}

