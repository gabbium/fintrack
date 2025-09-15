using Fintrack.API.FunctionalTests.Steps;
using Fintrack.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.API.FunctionalTests.Scenarios;

public class SwaggerTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly SwaggerSteps _swagger = new(fx);

    [Fact]
    public async Task GivenAnonymousUser_WhenCheckingV1_Then200()
    {
        // Arrange
        await _auth.Given_AnonymousUser();

        // Act
        var response = await _swagger.When_AttemptToGetV1();

        // Assert
        response.ShouldBeOk();
    }
}
