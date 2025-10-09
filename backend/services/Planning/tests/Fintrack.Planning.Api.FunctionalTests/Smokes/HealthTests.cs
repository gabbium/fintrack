using Fintrack.Planning.Api.FunctionalTests.TestHelpers;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Assertions;

namespace Fintrack.Planning.Api.FunctionalTests.Smokes;

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

