using Fintrack.API.FunctionalTests.Steps;
using Fintrack.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.API.FunctionalTests.Scenarios.Smoke;

public class HealthTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly SmokeSteps _smoke = new(fx);

    [Fact]
    public async Task GivenAnonymousUser_WhenCheckingAlive_Then200()
    {
        // Arrange
        await _auth.Given_AnonymousUser();

        // Act
        var response = await _smoke.When_AttemptToGetAlive();

        // Assert
        response.ShouldBeOk();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCheckingReady_Then200()
    {
        // Arrange
        await _auth.Given_AnonymousUser();

        // Act
        var response = await _smoke.When_AttemptToGetReady();

        // Assert
        response.ShouldBeOk();
    }
}
