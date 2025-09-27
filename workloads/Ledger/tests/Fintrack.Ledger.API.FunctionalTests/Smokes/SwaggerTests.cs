using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure;

namespace Fintrack.Ledger.API.FunctionalTests.Smokes;

public class SwaggerTests(TestFixture fx) : TestBase(fx)
{
    [Fact]
    public async Task GivenApplicationStarted_WhenGettingSwaggerV1_Then200()
    {
        var response = await _fx.Client.GetAsync("/openapi/v1.json", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
