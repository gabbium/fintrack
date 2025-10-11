using Fintrack.Planning.Api.FunctionalTests.TestSupport;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.Health;

public class HealthTests(TestsFixture fx) : TestsBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task ApplicationRespondsAsAliveSuccessfully()
    {
        var response = await _httpClient.GetAsync("/alive");
        response.ShouldBeOk();
    }

    [Fact]
    public async Task ApplicationReportsHealthyStatusSuccessfully()
    {
        var response = await _httpClient.GetAsync("/health");
        response.ShouldBeOk();
    }
}

