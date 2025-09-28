using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.Ledger.API.FunctionalTests.Smokes;

public class SwaggerTests(TestFixture fx) : TestBase(fx)
{
    private readonly HttpClient _httpClient = fx.Factory.CreateDefaultClient();

    [Fact]
    public async Task GivenApplicationStarted_WhenGettingSwaggerV1_Then200()
    {
        var response = await _httpClient.GetAsync("/openapi/v1.json", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
