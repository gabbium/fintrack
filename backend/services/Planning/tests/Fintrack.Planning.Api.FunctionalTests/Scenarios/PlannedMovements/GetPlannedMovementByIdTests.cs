using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class GetPlannedMovementByIdTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public GetPlannedMovementByIdTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanViewAnExistingPlannedMovementById()
    {
        _auth.LoginAsUser();
        var plannedMovement = await _plannedMovement.EnsureActivePlannedMovementExists();

        var response = await _plannedMovement.GetPlannedMovementById(plannedMovement.Id);

        var body = await response.ShouldBeOkWithBody<PlannedMovementDto>();
        body.Id.ShouldBe(plannedMovement.Id);
    }

    [Fact]
    public async Task UserCannotViewAnotherUsersPlannedMovement()
    {
        _auth.LoginAsUser();
        var anotherUsersPlannedMovement = await _plannedMovement.EnsureActivePlannedMovementExists();

        _auth.LoginAsUser();
        var response = await _plannedMovement.GetPlannedMovementById(anotherUsersPlannedMovement.Id);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task UserCannotViewANonExistentPlannedMovement()
    {
        _auth.LoginAsUser();

        var nonExistentPlannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.GetPlannedMovementById(nonExistentPlannedMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task ViewingAPlannedMovementWithAnInvalidIdFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var invalidPlannedMovementId = Guid.Empty;
        var response = await _plannedMovement.GetPlannedMovementById(invalidPlannedMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotViewAPlannedMovement()
    {
        var plannedMovementId = Guid.NewGuid();
        var response = await _plannedMovement.GetPlannedMovementById(plannedMovementId);

        response.ShouldBeUnauthorized();
    }
}

