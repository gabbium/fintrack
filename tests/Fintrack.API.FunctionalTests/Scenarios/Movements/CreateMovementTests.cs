using Fintrack.API.FunctionalTests.Steps;
using Fintrack.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.API.FunctionalTests.Scenarios.Movements;

public class CreateMovementTests(TestFixture fixture) : TestBase(fixture)
{
    private readonly AuthSteps _auth = new(fixture);
    private readonly MovementSteps _movement = new(fixture);

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingWithValidData_Then201WithBodyAndLocation()
    {
        await _auth.Given_LoggedInUser();
        var command = _movement.Given_ValidCreateCommand();
        var response = await _movement.When_AttemptToCreate(command);
        await response.ShouldBeCreatedWithBodyAndLocation<MovementDto>(
            body => $"/api/v1/movements/{body.Id}");
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingWithInvalidData_Then400WithValidation()
    {
        await _auth.Given_LoggedInUser();
        var command = _movement.Given_InvalidCreateCommand_TooLongDescription();
        var response = await _movement.When_AttemptToCreate(command);
        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCreating_Then401WithBearerChallenge()
    {
        await _auth.Given_AnonymousUser();
        var command = _movement.Given_ValidCreateCommand();
        var response = await _movement.When_AttemptToCreate(command);
        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
