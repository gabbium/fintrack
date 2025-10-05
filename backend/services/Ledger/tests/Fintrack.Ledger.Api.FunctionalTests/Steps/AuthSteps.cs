using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Infrastructure.Authorize;

namespace Fintrack.Ledger.Api.FunctionalTests.Steps;

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
