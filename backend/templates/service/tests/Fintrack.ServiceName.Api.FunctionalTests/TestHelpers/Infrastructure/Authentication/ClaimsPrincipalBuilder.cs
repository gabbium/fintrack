namespace Fintrack.ServiceName.Api.FunctionalTests.TestHelpers.Infrastructure.Authentication;

public class ClaimsPrincipalBuilder
{
    private Guid _userId = Guid.NewGuid();

    public ClaimsPrincipalBuilder WithUserId(Guid userId)
    {
        _userId = userId;
        return this;
    }

    public ClaimsPrincipal Build()
    {
        var identity = new ClaimsIdentity("cookies");

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, _userId.ToString()));

        var principal = new ClaimsPrincipal(identity);

        return principal;
    }
}

