using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class CancelPlannedMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public CancelPlannedMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanCancelAnActivePlannedMovementSuccessfully()
    {
        _auth.LoginAsUser();
        var activePlannedMovement = await _plannedMovement.EnsureActivePlannedMovementExists();

        var response = await _plannedMovement.CancelPlannedMovement(activePlannedMovement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task UserCannotCancelARealizedPlannedMovement()
    {
        _auth.LoginAsUser();
        var realizedPlannedMovement = await _plannedMovement.EnsureRealizedPlannedMovementExists();

        var response = await _plannedMovement.CancelPlannedMovement(realizedPlannedMovement.Id);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task UserCannotCancelANonExistentPlannedMovement()
    {
        _auth.LoginAsUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.CancelPlannedMovement(nonExistentPlannedMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task CancelingAPlannedMovementWithAnInvalidIdFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.CancelPlannedMovement(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotCancelAPlannedMovement()
    {
        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.CancelPlannedMovement(plannedMovementId);

        response.ShouldBeUnauthorized();
    }
}
