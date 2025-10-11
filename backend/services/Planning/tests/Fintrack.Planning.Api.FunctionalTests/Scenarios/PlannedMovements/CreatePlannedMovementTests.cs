using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Planning.Application.Models;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class CreatePlannedMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public CreatePlannedMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanCreateAPlannedMovementSuccessfully()
    {
        _auth.LoginAsUser();

        var request = new CreatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.CreatePlannedMovement(request);

        await response.ShouldBeCreatedWithBodyAndLocation<PlannedMovementDto>(
            body => $"/api/v1/planned-movements/{body.Id}");
    }

    [Fact]
    public async Task CreatingAPlannedMovementWithAnInvalidRequestFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var request = new CreatePlannedMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _plannedMovement.CreatePlannedMovement(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotCreateAPlannedMovement()
    {
        var request = new CreatePlannedMovementRequestBuilder().Build();
        var response = await _plannedMovement.CreatePlannedMovement(request);

        response.ShouldBeUnauthorized();
    }
}

