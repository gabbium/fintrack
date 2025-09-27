using Fintrack.Users.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Users.API.FunctionalTests.TestHelpers.Infrastructure;

namespace Fintrack.Users.API.FunctionalTests.Scenarios;

public class HealthTests(TestFixture fx) : TestBase(fx)
{
    [Fact]
    public async Task GivenApplicationStarted_WhenGettingAlive_Then200()
    {
        var response = await _fx.UsersApiClient.GetAsync("/health/alive", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }

    [Fact]
    public async Task GivenDependenciesHealthy_WhenCheckingReady_Then200()
    {
        var response = await _fx.UsersApiClient.GetAsync("/health/ready", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
