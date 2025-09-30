using Fintrack.Ledger.API.FunctionalTests.Steps;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios.Movements;

public class DeleteMovementTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovement_WhenDeletingExistingMovement_ThenNoContent()
    {
        _auth.Given_LoggedInUser();
        var movement = await _movement.Given_ExistingMovement();

        var response = await _movement.When_AttemptToDelete(movement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenDeletingNonExistentMovement_ThenNoContent()
    {
        _auth.Given_LoggedInUser();

        var nonExistentMovementId = Guid.NewGuid();
        var response = await _movement.When_AttemptToDelete(nonExistentMovementId);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenDeletingWithInvalidId_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var invalidMovementId = Guid.Empty;
        var response = await _movement.When_AttemptToDelete(invalidMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenDeletingMovement_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var movementId = Guid.NewGuid();
        var response = await _movement.When_AttemptToDelete(movementId);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
