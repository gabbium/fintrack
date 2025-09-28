using CleanArch;
using Fintrack.Ledger.API.FunctionalTests.Steps;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios.Movements;

public class ListMovementsTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUser_WhenListing_ThenOkWithBody()
    {
        _auth.Given_LoggedInUser();
        var request = _movement.Given_ValidListRequest();
        var response = await _movement.When_AttemptToList(request);
        await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenListing_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();
        var request = _movement.Given_InvalidValidListRequest_PageNumberNegative();
        var response = await _movement.When_AttemptToList(request);
        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenListing_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();
        var request = _movement.Given_ValidListRequest();
        var response = await _movement.When_AttemptToList(request);
        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
