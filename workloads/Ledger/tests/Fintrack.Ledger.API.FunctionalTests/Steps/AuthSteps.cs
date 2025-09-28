using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Authorize;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.Ledger.API.FunctionalTests.Steps;

public class AuthSteps(TestFixture fx)
{
    public void Given_LoggedInUser()
    {
        using var scope = fx.Factory.Services.CreateScope();
        var autoAuthorizeAccessor = scope.ServiceProvider.GetRequiredService<IAutoAuthorizeAccessor>();
        autoAuthorizeAccessor.User = new ClaimsPrincipalBuilder().Build();
    }

    public void Given_AnonymousUser()
    {
        using var scope = fx.Factory.Services.CreateScope();
        var autoAuthorizeAccessor = scope.ServiceProvider.GetRequiredService<IAutoAuthorizeAccessor>();
        autoAuthorizeAccessor.User = null;
    }
}
