using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.API.FunctionalTests.Steps;

public class AuthSteps(TestFixture fx)
{
    public Task Given_AnonymousUser()
    {
        fx.Client.DefaultRequestHeaders.Authorization = null;

        return Task.CompletedTask;
    }
}
