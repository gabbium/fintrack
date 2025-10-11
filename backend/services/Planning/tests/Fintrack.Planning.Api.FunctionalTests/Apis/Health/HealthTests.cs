using BuildingBlocks.Api.FunctionalTests.Assertions;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.Health;

public class HealthTests(FunctionalTestsFixture fx) : FunctionalTestsBase(fx)
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

