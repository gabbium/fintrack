using BuildingBlocks.Api.FunctionalTests.Assertions;
using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class RealizePlannedMovementTests(FunctionalTestsFixture fx) : FunctionalTestsBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndActivePlannedMovement_WhenRealizingPlannedMovement_ThenNoContent()
    {
        _auth.Given_LoggedInUser();
        var activePlannedMovement = await _plannedMovement.Given_ExistingPlannedMovement();

        var response = await _plannedMovement.When_AttemptToRealize(activePlannedMovement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task GivenLoggedInUserAndCanceledPlannedMovement_WhenRealizingPlannedMovement_ThenUnprocessableEntityWithProblem()
    {
        _auth.Given_LoggedInUser();
        var canceledPlannedMovement = await _plannedMovement.Given_ExistingCanceledPlannedMovement();

        var response = await _plannedMovement.When_AttemptToRealize(canceledPlannedMovement.Id);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenRealizingNonExistentPlannedMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToRealize(nonExistentPlannedMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenRealizingPlannedMovementWithInvalidId_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.When_AttemptToRealize(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenRealizingPlannedMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToRealize(plannedMovementId);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
