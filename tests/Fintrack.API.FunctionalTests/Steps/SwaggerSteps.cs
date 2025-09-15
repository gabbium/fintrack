using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.API.FunctionalTests.Steps;

public class SwaggerSteps(TestFixture fx)
{
    public async Task<HttpResponseMessage> When_AttemptToGetV1()
    {
        return await fx.Client.GetAsync("/scalar/v1");
    }
}
