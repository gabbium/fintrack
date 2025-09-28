using Fintrack.Ledger.API.FunctionalTests.Steps;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios.Movements;

public class CreateMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingWithValidData_Then201WithBodyAndLocation()
    {
        var command = _movement.Given_ValidCreateCommand();
        var response = await _movement.When_AttemptToCreate(command);
        await response.ShouldBeOkWithBody<MovementDto>();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingWithInvalidData_Then400WithValidation()
    {
        var command = _movement.Given_InvalidCreateCommand_TooLongDescription();
        var response = await _movement.When_AttemptToCreate(command);
        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCreating_Then401WithBearerChallenge()
    {
        var command = _movement.Given_ValidCreateCommand();
        var response = await _movement.When_AttemptToCreate(command);
        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
