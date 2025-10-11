using BuildingBlocks.Api.FunctionalTests.AutoAuthorize;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Planning.Api.FunctionalTests.Steps;

public class AuthSteps(FunctionalTestsFixture fx)
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
