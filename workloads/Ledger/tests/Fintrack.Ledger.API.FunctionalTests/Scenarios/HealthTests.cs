using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios;

public class HealthTests(TestFixture fx) : TestBase
{
    [Fact]
    public async Task GivenApplicationStarted_WhenGettingAlive_Then200()
    {
        var response = await fx.LedgerApiClient.GetAsync("/health/alive", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }

    [Fact]
    public async Task GivenDependenciesHealthy_WhenCheckingReady_Then200()
    {
        var response = await fx.LedgerApiClient.GetAsync("/health/ready", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
