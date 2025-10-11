using Fintrack.Ledger.Api.FunctionalTests.TestSupport;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Api.FunctionalTests.Scenarios.Movements;

public class GetMovementByIdTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly MovementClient _movement;

    public GetMovementByIdTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _movement = new(httpClient);
    }

    [Fact]
    public async Task UserCanViewAnExistingMovementById()
    {
        _auth.LoginAsUser();
        var movement = await _movement.EnsureMovementExists();

        var response = await _movement.GetMovementById(movement.Id);

        var body = await response.ShouldBeOkWithBody<MovementDto>();
        body.Id.ShouldBe(movement.Id);
    }

    [Fact]
    public async Task UserCannotViewAnotherUsersMovement()
    {
        _auth.LoginAsUser();
        var anotherUsersMovement = await _movement.EnsureMovementExists();
        _auth.LoginAsUser();

        var response = await _movement.GetMovementById(anotherUsersMovement.Id);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task UserCannotViewANonExistentMovement()
    {
        _auth.LoginAsUser();

        var nonExistentMovementId = Guid.NewGuid();
        var response = await _movement.GetMovementById(nonExistentMovementId);

        await response.ShouldBeNotFoundWithProblem();
    }

    [Fact]
    public async Task ViewingAMovementWithAnInvalidIdFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var invalidMovementId = Guid.Empty;
        var response = await _movement.GetMovementById(invalidMovementId);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotViewAMovement()
    {
        var movementId = Guid.NewGuid();
        var response = await _movement.GetMovementById(movementId);

        response.ShouldBeUnauthorized();
    }
}
