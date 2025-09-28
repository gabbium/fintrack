using Fintrack.Ledger.API.FunctionalTests.Steps;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios.Movements;

public class CreateMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingWithValidData_ThenCreatedWithBodyAndLocation()
    {
        _auth.Given_LoggedInUser();
        var command = _movement.Given_ValidCreateCommand();
        var response = await _movement.When_AttemptToCreate(command);
        await response.ShouldBeCreatedWithBodyAndLocation<MovementDto>(
            body => $"/api/v1/movements/{body.Id}");
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingWithInvalidData_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();
        var command = _movement.Given_InvalidCreateCommand_TooLongDescription();
        var response = await _movement.When_AttemptToCreate(command);
        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCreating_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();
        var command = _movement.Given_ValidCreateCommand();
        var response = await _movement.When_AttemptToCreate(command);
        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
