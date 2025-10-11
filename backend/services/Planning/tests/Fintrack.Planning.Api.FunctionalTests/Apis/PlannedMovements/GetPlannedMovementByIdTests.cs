using BuildingBlocks.Api.FunctionalTests.Assertions;
using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class GetPlannedMovementByIdTests(FunctionalTestsFixture fx) : FunctionalTestsBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovement_WhenGettingPlannedMovement_ThenOkWithBody()
    {
        _auth.Given_LoggedInUser();
        var plannedMovement = await _plannedMovement.Given_ExistingPlannedMovement();

        var response = await _plannedMovement.When_AttemptToGetById(plannedMovement.Id);

        var body = await response.ShouldBeOkWithBody<PlannedMovementDto>();
        body.Id.ShouldBe(plannedMovement.Id);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovement_WhenGettingOtherUsersPlannedMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();
        var otherUsersPlannedMovement = await _plannedMovement.Given_ExistingPlannedMovement();
        _auth.Given_LoggedInUser();

        var response = await _plannedMovement.When_AttemptToGetById(otherUsersPlannedMovement.Id);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenGettingNonExistentPlannedMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToGetById(nonExistentPlannedMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenGettingPlannedMovementWithInvalidId_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.When_AttemptToGetById(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenGettingPlannedMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.When_AttemptToGetById(plannedMovementId);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}

