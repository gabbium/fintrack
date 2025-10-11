using BuildingBlocks.Api.FunctionalTests.Assertions;
using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class DeletePlannedMovementTests(FunctionalTestsFixture fx) : FunctionalTestsBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndActivePlannedMovement_WhenDeletingPlannedMovement_ThenNoContent()
    {
        _auth.Given_LoggedInUser();
        var activePlannedMovement = await _plannedMovement.Given_ExistingPlannedMovement();

        var response = await _plannedMovement.When_AttemptToDelete(activePlannedMovement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task GivenLoggedInUserAndCanceledPlannedMovement_WhenDeletingPlannedMovement_ThenUnprocessableEntityWithProblem()
    {
        _auth.Given_LoggedInUser();
        var canceledPlannedMovement = await _plannedMovement.Given_ExistingCanceledPlannedMovement();

        var response = await _plannedMovement.When_AttemptToDelete(canceledPlannedMovement.Id);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenDeletingNonExistentPlannedMovement_ThenNoContent()
    {
        _auth.Given_LoggedInUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToDelete(nonExistentPlannedMovementId);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenDeletingPlannedMovementWithInvalidId_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.When_AttemptToDelete(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenDeletingPlannedMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToDelete(plannedMovementId);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
