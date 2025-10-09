using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class CreatePlannedMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingPlannedMovementWithValidRequest_ThenCreatedWithBodyAndLocation()
    {
        _auth.Given_LoggedInUser();

        var request = new CreatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToCreate(request);

        await response.ShouldBeCreatedWithBodyAndLocation<PlannedMovementDto>(
            body => $"/api/v1/planned-movements/{body.Id}");
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingPlannedMovementWithInvalidRequest_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var request = new CreatePlannedMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _plannedMovement.When_AttemptToCreate(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCreatingPlannedMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var request = new CreatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToCreate(request);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}

