using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Ledger.Api.FunctionalTests.Apis.Health;

public class HealthTests(TestFixture fx) : TestBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task GivenApplicationStarted_WhenGettingAlive_ThenOk()
    {
        var response = await _httpClient.GetAsync("/alive");
        response.ShouldBeOk();
    }

    [Fact]
    public async Task GivenDependenciesHealthy_WhenCheckingHealth_ThenOk()
    {
        var response = await _httpClient.GetAsync("/health");
        response.ShouldBeOk();
    }
}

