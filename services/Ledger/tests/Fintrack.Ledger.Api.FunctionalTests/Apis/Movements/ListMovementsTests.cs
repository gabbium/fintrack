using Fintrack.Ledger.Api.FunctionalTests.Steps;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Assertions;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Api.FunctionalTests.Apis.Movements;

public class ListMovementsTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly MovementSteps _movement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovements_WhenListingMovementsWithPagination_ThenOkWithBodyAndCorrectPagination()
    {
        _auth.Given_LoggedInUser();
        await _movement.Given_ExistingMovement();
        await _movement.Given_ExistingMovement();
        await _movement.Given_ExistingMovement();
        await _movement.Given_ExistingMovement();

        var requestPage1 = new ListMovementsRequestBuilder().WithPageNumber(1).WithPageSize(2).Build();
        var responsePage1 = await _movement.When_AttemptToList(requestPage1);

        var bodyPage1 = await responsePage1.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        bodyPage1.Items.Count.ShouldBe(2);

        var requestPage2 = new ListMovementsRequestBuilder().WithPageNumber(2).WithPageSize(2).Build();
        var responsePage2 = await _movement.When_AttemptToList(requestPage2);

        var bodyPage2 = await responsePage2.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        bodyPage2.Items.Count.ShouldBe(2);

        var requestPage3 = new ListMovementsRequestBuilder().WithPageNumber(3).WithPageSize(2).Build();
        var responsePage3 = await _movement.When_AttemptToList(requestPage3);

        var bodyPage3 = await responsePage3.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        bodyPage3.Items.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovements_WhenListingMovementsWithOrder_ThenOkWithBodyOrdered()
    {
        _auth.Given_LoggedInUser();
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().Build());

        var request = new ListMovementsRequestBuilder().WithOrder("occurredon asc").Build();
        var response = await _movement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.ShouldNotBeEmpty();
        body.Items.Select(movement => movement.OccurredOn).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovements_WhenListingMovementsWithKindFilter_ThenOkWithBodyFilteredByKind()
    {
        _auth.Given_LoggedInUser();
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithKind(MovementKind.Expense).Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithKind(MovementKind.Income).Build());

        var request = new ListMovementsRequestBuilder().WithKind([MovementKind.Expense]).Build();
        var response = await _movement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.Count.ShouldBe(1);
        body.Items.ShouldAllBe(movement => movement.Kind == MovementKind.Expense);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovements_WhenListingMovementsWithMinOccurredOnFilter_ThenOkWithBodyFilteredByOccurredOn()
    {
        _auth.Given_LoggedInUser();
        var occurredOn = DateTimeOffset.Parse("2025-10-10T00:00:00Z");
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(-1)).Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn).Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(1)).Build());

        var request = new ListMovementsRequestBuilder().WithMinOccurredOn(occurredOn).Build();
        var response = await _movement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(movement => movement.OccurredOn >= occurredOn);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingMovements_WhenListingMovementsWithMaxOccurredOnFilter_ThenOkWithBodyFilteredByOccurredOn()
    {
        _auth.Given_LoggedInUser();
        var occurredOn = DateTimeOffset.Parse("2025-10-20T00:00:00Z");
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(-1)).Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn).Build());
        await _movement.Given_ExistingMovement(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(1)).Build());

        var request = new ListMovementsRequestBuilder().WithMaxOccurredOn(occurredOn).Build();
        var response = await _movement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(movement => movement.OccurredOn <= occurredOn);
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenListingMovementsWithInvalidRequest_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var request = new ListMovementsRequestBuilder().WithPageNumber(-1).Build();
        var response = await _movement.When_AttemptToList(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenListingMovements_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var request = new ListMovementsRequestBuilder().Build();
        var response = await _movement.When_AttemptToList(request);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}
