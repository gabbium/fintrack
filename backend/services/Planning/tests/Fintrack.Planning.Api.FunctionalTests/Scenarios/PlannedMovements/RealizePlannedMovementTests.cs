using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class RealizePlannedMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public RealizePlannedMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanRealizeAnActivePlannedMovementSuccessfully()
    {
        _auth.LoginAsUser();
        var activePlannedMovement = await _plannedMovement.EnsureActivePlannedMovementExists();

        var response = await _plannedMovement.RealizePlannedMovement(activePlannedMovement.Id);

        response.ShouldBeNoContent();
    }

    [Fact]
    public async Task UserCannotRealizeACanceledPlannedMovement()
    {
        _auth.LoginAsUser();
        var canceledPlannedMovement = await _plannedMovement.EnsureCanceledPlannedMovementExists();

        var response = await _plannedMovement.RealizePlannedMovement(canceledPlannedMovement.Id);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task UserCannotRealizeANonExistentPlannedMovement()
    {
        _auth.LoginAsUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.RealizePlannedMovement(nonExistentPlannedMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task RealizingAPlannedMovementWithAnInvalidIdFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.RealizePlannedMovement(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotRealizeAPlannedMovement()
    {
        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.RealizePlannedMovement(plannedMovementId);

        response.ShouldBeUnauthorized();
    }
}
