using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Ledger.Api.FunctionalTests.Steps;

public class AuthSteps(TestFixture fx)
{
    public void Given_LoggedInUser()
    {
        using var scope = fx.Factory.Services.CreateScope();
        var autoAuthorizeAccessor = scope.ServiceProvider.GetRequiredService<IAutoAuthorizeAccessor>();
        autoAuthorizeAccessor.Impersonate(new ClaimsPrincipalBuilder()
            .WithClaim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            .Build());
    }

    public void Given_AnonymousUser()
    {
        using var scope = fx.Factory.Services.CreateScope();
        var autoAuthorizeAccessor = scope.ServiceProvider.GetRequiredService<IAutoAuthorizeAccessor>();
        autoAuthorizeAccessor.Impersonate(null);
    }
}
