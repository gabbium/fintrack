using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Assertions;

namespace Fintrack.Ledger.Api.FunctionalTests.Smokes;

public class OpenApiTests(TestFixture fx) : TestBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task GivenApplicationStarted_WhenGettingOpenApiV1_ThenOk()
    {
        var response = await _httpClient.GetAsync("/openapi/v1.json");
        response.ShouldBeOk();
    }
}

