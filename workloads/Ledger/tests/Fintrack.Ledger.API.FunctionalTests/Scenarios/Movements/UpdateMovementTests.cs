using Fintrack.Ledger.API.FunctionalTests.Steps;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios.Movements;

public class UpdateMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovement_WhenUpdatingMovementWithValidRequest_ThenOkWithBody()
    {
        _auth.Given_LoggedInUser();
        var movement = await _movement.Given_ExistingMovement();

        var request = new UpdateMovementRequestBuilder().Build();
        var response = await _movement.When_AttemptToUpdate(movement.Id, request);

        await response.ShouldBeOkWithBody<MovementDto>();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingNonExistentMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();

        var nonExistentMovementId = Guid.NewGuid();
        var request = new UpdateMovementRequestBuilder().Build();
        var response = await _movement.When_AttemptToUpdate(nonExistentMovementId, request);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingMovementWithInvalidRequest_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var movementId = Guid.NewGuid();
        var request = new UpdateMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _movement.When_AttemptToUpdate(movementId, request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenUpdatingMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var movementId = Guid.NewGuid();
        var request = new UpdateMovementRequestBuilder().Build();
        var response = await _movement.When_AttemptToUpdate(movementId, request);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
