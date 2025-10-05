using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Assertions;

namespace Fintrack.Ledger.Api.FunctionalTests.Smokes;

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

