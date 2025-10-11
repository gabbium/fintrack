using Fintrack.Ledger.Api.FunctionalTests.TestSupport;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Clients;

namespace Fintrack.Ledger.Api.FunctionalTests.Scenarios.Movements;

public class DeleteMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly MovementClient _movement;

    public DeleteMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _movement = new(httpClient);
    }

    [Fact]
    public async Task UserCanDeleteAnExistingMovementSuccessfully()
    {
        _auth.LoginAsUser();
        var movement = await _movement.EnsureMovementExists();

        var response = await _movement.DeleteMovement(movement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task DeletingANonExistentMovementIsIdempotent()
    {
        _auth.LoginAsUser();

        var nonExistentMovementId = Guid.NewGuid();
        var response = await _movement.DeleteMovement(nonExistentMovementId);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task DeletingAMovementWithAnInvalidIdFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var invalidMovementId = Guid.Empty;
        var response = await _movement.DeleteMovement(invalidMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotDeleteAMovement()
    {
        var movementId = Guid.NewGuid();
        var response = await _movement.DeleteMovement(movementId);

        response.ShouldBeUnauthorized();
    }
}
