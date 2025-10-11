using Fintrack.Ledger.Api.FunctionalTests.TestSupport;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.Scenarios.Movements;

public class CreateMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly MovementClient _movement;

    public CreateMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _movement = new(httpClient);
    }

    [Fact]
    public async Task UserCanCreateAMovementSuccessfully()
    {
        _auth.LoginAsUser();

        var request = new CreateMovementRequestBuilder().Build();
        var response = await _movement.CreateMovement(request);

        await response.ShouldBeCreatedWithBodyAndLocation<MovementDto>(
            body => $"/api/v1/movements/{body.Id}");
    }

    [Fact]
    public async Task CreatingAMovementWithAnInvalidRequestFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var request = new CreateMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _movement.CreateMovement(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotCreateAMovement()
    {
        var request = new CreateMovementRequestBuilder().Build();
        var response = await _movement.CreateMovement(request);

        response.ShouldBeUnauthorized();
    }
}
