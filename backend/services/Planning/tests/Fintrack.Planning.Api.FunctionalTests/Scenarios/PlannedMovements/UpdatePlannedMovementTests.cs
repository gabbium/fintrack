using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class UpdatePlannedMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public UpdatePlannedMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanUpdateAnActivePlannedMovementSuccessfully()
    {
        _auth.LoginAsUser();
        var activePlannedMovement = await _plannedMovement.EnsureActivePlannedMovementExists();

        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.UpdatePlannedMovement(activePlannedMovement.Id, request);

        await response.ShouldBeOkWithBody<PlannedMovementDto>();
    }

    [Fact]
    public async Task UserCannotUpdateACanceledPlannedMovement()
    {
        _auth.LoginAsUser();
        var canceledPlannedMovement = await _plannedMovement.EnsureCanceledPlannedMovementExists();

        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.UpdatePlannedMovement(canceledPlannedMovement.Id, request);

        await response.ShouldBeUnprocessableEntityWithProblem();
    }

    [Fact]
    public async Task UserCannotUpdateANonExistentPlannedMovement()
    {
        _auth.LoginAsUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.UpdatePlannedMovement(nonExistentPlannedMovementId, request);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task UpdatingAPlannedMovementWithAnInvalidRequestFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var plannedMovementId = Guid.NewGuid();
        var request = new UpdatePlannedMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _plannedMovement.UpdatePlannedMovement(plannedMovementId, request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotUpdateAPlannedMovement()
    {
        var plannedMovementId = Guid.NewGuid();
        var request = new UpdatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.UpdatePlannedMovement(plannedMovementId, request);

        response.ShouldBeUnauthorized();
    }
}
