using Fintrack.Ledger.API.FunctionalTests.Steps;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios.Movements;

public class UpdateMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingWithValidData_ThenOkWithBody()
    {
        _auth.Given_LoggedInUser();
        var movement = await _movement.Given_ExistingMovement();
        var request = _movement.Given_ValidUpdateRequest();
        var response = await _movement.When_AttemptToUpdate(movement.Id, request);
        var body = await response.ShouldBeOkWithBody<MovementDto>();
        body.Id.ShouldBe(movement.Id);
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingNonExistent_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();
        var nonExistentMovementId = Guid.NewGuid();
        var request = _movement.Given_ValidUpdateRequest();
        var response = await _movement.When_AttemptToUpdate(nonExistentMovementId, request);
        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingWithInvalidData_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();
        var movementId = Guid.NewGuid();
        var request = _movement.Given_InvalidUpdateRequest_TooLongDescription();
        var response = await _movement.When_AttemptToUpdate(movementId, request);
        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenUpdating_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();
        var movementId = Guid.NewGuid();
        var request = _movement.Given_ValidUpdateRequest();
        var response = await _movement.When_AttemptToUpdate(movementId, request);
        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
