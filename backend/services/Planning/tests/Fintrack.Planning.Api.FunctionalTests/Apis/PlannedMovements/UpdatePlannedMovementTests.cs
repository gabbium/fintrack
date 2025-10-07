using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class UpdatePlannedMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndActivePlannedMovement_WhenUpdatingPlannedMovementWithValidRequest_ThenOkWithBody()
    {
        _auth.Given_LoggedInUser();
        var activePlannedMovement = await _plannedMovement.Given_ExistingPlannedMovement();

        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToUpdate(activePlannedMovement.Id, request);

        await response.ShouldBeOkWithBody<PlannedMovementDto>();
    }

    [Fact]
    public async Task GivenLoggedInUserAndCanceledPlannedMovement_WhenUpdatingPlannedMovementWithValidRequest_ThenUnprocessableEntityWithProblem()
    {
        _auth.Given_LoggedInUser();
        var canceledPlannedMovement = await _plannedMovement.Given_ExistingCanceledPlannedMovement();

        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToUpdate(canceledPlannedMovement.Id, request);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingNonExistentPlannedMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToUpdate(nonExistentPlannedMovementId, request);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenUpdatingPlannedMovementWithInvalidRequest_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var plannedMovementId = Guid.NewGuid();
        var request = new UpdatePlannedMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _plannedMovement.When_AttemptToUpdate(plannedMovementId, request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenUpdatingPlannedMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var plannedMovementId = Guid.NewGuid();
        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToUpdate(plannedMovementId, request);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
