using Fintrack.Ledger.Api.FunctionalTests.Steps;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.Apis.Movements;

public class CreateMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingMovementWithValidRequest_ThenCreatedWithBodyAndLocation()
    {
        _auth.Given_LoggedInUser();

        var request = new CreateMovementRequestBuilder().Build();
        var response = await _movement.When_AttemptToCreate(request);

        await response.ShouldBeCreatedWithBodyAndLocation<MovementDto>(
            body => $"/api/v1/movements/{body.Id}");
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenCreatingMovementWithInvalidRequest_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var request = new CreateMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _movement.When_AttemptToCreate(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenCreatingMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var request = new CreateMovementRequestBuilder().Build();
        var response = await _movement.When_AttemptToCreate(request);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
