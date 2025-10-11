using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class DeletePlannedMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public DeletePlannedMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanDeleteAnActivePlannedMovementSuccessfully()
    {
        _auth.LoginAsUser();
        var activePlannedMovement = await _plannedMovement.EnsureActivePlannedMovementExists();

        var response = await _plannedMovement.DeletePlannedMovement(activePlannedMovement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task UserCannotDeleteACanceledPlannedMovement()
    {
        _auth.LoginAsUser();
        var canceledPlannedMovement = await _plannedMovement.EnsureCanceledPlannedMovementExists();

        var response = await _plannedMovement.DeletePlannedMovement(canceledPlannedMovement.Id);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task DeletingANonExistentPlannedMovementIsIdempotent()
    {
        _auth.LoginAsUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.DeletePlannedMovement(nonExistentPlannedMovementId);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task DeletingAPlannedMovementWithAnInvalidIdFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.DeletePlannedMovement(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotDeleteAPlannedMovement()
    {
        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.DeletePlannedMovement(plannedMovementId);

        response.ShouldBeUnauthorized();
    }
}
