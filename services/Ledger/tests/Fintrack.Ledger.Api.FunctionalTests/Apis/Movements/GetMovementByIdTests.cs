using Fintrack.Ledger.Api.FunctionalTests.Steps;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.Apis.Movements;

public class GetMovementByIdTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovement_WhenGettingExistingMovement_ThenOkWithBody()
    {
        _auth.Given_LoggedInUser();
        var movement = await _movement.Given_ExistingMovement();

        var response = await _movement.When_AttemptToGetById(movement.Id);

        var body = await response.ShouldBeOkWithBody<MovementDto>();
        body.Id.ShouldBe(movement.Id);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovement_WhenGettingOtherUsersExistingMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();
        var otherUsersMovement = await _movement.Given_ExistingMovement();
        _auth.Given_LoggedInUser();

        var response = await _movement.When_AttemptToGetById(otherUsersMovement.Id);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenGettingNonExistentMovement_ThenNotFoundWithProblem()
    {
        _auth.Given_LoggedInUser();

        var nonExistentMovementId = Guid.NewGuid();
        var response = await _movement.When_AttemptToGetById(nonExistentMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenGettingMovementWithInvalidId_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var invalidMovementId = Guid.Empty;
        var response = await _movement.When_AttemptToGetById(invalidMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenGettingMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var movementId = Guid.NewGuid();
        var response = await _movement.When_AttemptToGetById(movementId);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
