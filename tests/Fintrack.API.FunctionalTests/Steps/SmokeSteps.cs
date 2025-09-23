using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.API.FunctionalTests.Steps;

public class SmokeSteps(TestFixture fx)
{
    public async Task<HttpResponseMessage> When_AttemptToGetAlive()
    {
        return await fx.Client.GetAsync("/health/alive");
    }

    public async Task<HttpResponseMessage> When_AttemptToGetReady()
    {
        return await fx.Client.GetAsync("/health/ready");
    }

    public async Task<HttpResponseMessage> When_AttemptToGetSwaggerV1()
    {
        return await fx.Client.GetAsync("/openapi/v1.json");
    }
}
