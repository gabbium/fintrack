using Fintrack.API.FunctionalTests.Steps;
using Fintrack.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.API.FunctionalTests.Scenarios;

public class HealthTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly HealthSteps _health = new(fx);

    [Fact]
    public async Task GivenAnonymousUser_WhenCheckingAlive_Then200()
    {
        // Arrange
        await _auth.Given_AnonymousUser();

        // Act
        var response = await _health.When_AttemptToGetAlive();

        // Assert
        response.ShouldBeOk();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCheckingReady_Then200()
    {
        // Arrange
        await _auth.Given_AnonymousUser();

        // Act
        var response = await _health.When_AttemptToGetReady();

        // Assert
        response.ShouldBeOk();
    }
}
