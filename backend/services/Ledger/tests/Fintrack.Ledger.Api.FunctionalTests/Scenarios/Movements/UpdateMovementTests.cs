using Fintrack.Ledger.Api.FunctionalTests.TestSupport;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.Scenarios.Movements;

public class UpdateMovementTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly MovementClient _movement;

    public UpdateMovementTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _movement = new(httpClient);
    }

    [Fact]
    public async Task UserCanUpdateAnExistingMovementSuccessfully()
    {
        _auth.LoginAsUser();
        var movement = await _movement.EnsureMovementExists();

        var request = new UpdateMovementRequestBuilder().Build();
        var response = await _movement.UpdateMovement(movement.Id, request);

        await response.ShouldBeOkWithBody<MovementDto>();
    }

    [Fact]
    public async Task UserCannotUpdateANonExistentMovement()
    {
        _auth.LoginAsUser();

        var nonExistentMovementId = Guid.NewGuid();
        var request = new UpdateMovementRequestBuilder().Build();
        var response = await _movement.UpdateMovement(nonExistentMovementId, request);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task UpdatingAMovementWithAnInvalidRequestFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var movementId = Guid.NewGuid();
        var request = new UpdateMovementRequestBuilder().WithDescription(new string('a', 129)).Build();
        var response = await _movement.UpdateMovement(movementId, request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotUpdateAMovement()
    {
        var movementId = Guid.NewGuid();
        var request = new UpdateMovementRequestBuilder().Build();
        var response = await _movement.UpdateMovement(movementId, request);

        response.ShouldBeUnauthorized();
    }
}
