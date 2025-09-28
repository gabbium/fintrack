using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.Ledger.API.FunctionalTests.Smokes;

public class HealthTests(TestFixture fx) : TestBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task GivenApplicationStarted_WhenGettingAlive_Then200()
    {
        var response = await _httpClient.GetAsync("/health/alive", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }

    [Fact]
    public async Task GivenDependenciesHealthy_WhenCheckingReady_Then200()
    {
        var response = await _httpClient.GetAsync("/health/ready", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
