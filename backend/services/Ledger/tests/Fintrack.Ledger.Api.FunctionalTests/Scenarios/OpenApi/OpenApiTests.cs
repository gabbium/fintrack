using Fintrack.Ledger.Api.FunctionalTests.TestSupport;

namespace Fintrack.Ledger.Api.FunctionalTests.Scenarios.OpenApi;

public class OpenApiTests(TestsFixture fx) : TestsBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task ApplicationExposesOpenApiV1SpecificationSuccessfully()
    {
        var response = await _httpClient.GetAsync("/openapi/v1.json");
        response.ShouldBeOk();
    }
}

