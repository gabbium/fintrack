using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios;

public class OpenApiTests(TestFixture fx) : TestBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task GivenApplicationStarted_WhenGettingOpenApiV1_ThenOk()
    {
        var response = await _httpClient.GetAsync("/openapi/v1.json", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
