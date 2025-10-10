using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class CancelPlannedMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndActivePlannedMovement_WhenCancelingPlannedMovement_ThenNoContent()
    {
        _auth.Given_LoggedInUser();
        var activePlannedMovement = await _plannedMovement.Given_ExistingPlannedMovement();

        var response = await _plannedMovement.When_AttemptToCancel(activePlannedMovement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task GivenLoggedInUserAndRealizedPlannedMovement_WhenCancelingPlannedMovement_ThenUnprocessableEntityWithProblem()
    {
        _auth.Given_LoggedInUser();
        var realizedPlannedMovement = await _plannedMovement.Given_ExistingRealizedPlannedMovement();

        var response = await _plannedMovement.When_AttemptToCancel(realizedPlannedMovement.Id);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCancelingNonExistentPlannedMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToCancel(nonExistentPlannedMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCancelingPlannedMovementWithInvalidId_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.When_AttemptToCancel(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCancelingPlannedMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToCancel(plannedMovementId);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
