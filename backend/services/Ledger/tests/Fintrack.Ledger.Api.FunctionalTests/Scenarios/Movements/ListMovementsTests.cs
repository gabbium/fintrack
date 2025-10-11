using Fintrack.Ledger.Api.FunctionalTests.TestSupport;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Ledger.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Api.FunctionalTests.Scenarios.Movements;

public class ListMovementsTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly MovementClient _movement;

    public ListMovementsTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _movement = new(httpClient);
    }

    [Fact]
    public async Task UserCanListMovementsWithPagination()
    {
        _auth.LoginAsUser();
        await _movement.EnsureMovementExists();
        await _movement.EnsureMovementExists();
        await _movement.EnsureMovementExists();
        await _movement.EnsureMovementExists();

        var requestPage1 = new ListMovementsRequestBuilder().WithPageNumber(1).WithPageSize(2).Build();
        var responsePage1 = await _movement.ListMovements(requestPage1);

        var bodyPage1 = await responsePage1.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        bodyPage1.Items.Count.ShouldBe(2);

        var requestPage2 = new ListMovementsRequestBuilder().WithPageNumber(2).WithPageSize(2).Build();
        var responsePage2 = await _movement.ListMovements(requestPage2);

        var bodyPage2 = await responsePage2.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        bodyPage2.Items.Count.ShouldBe(2);

        var requestPage3 = new ListMovementsRequestBuilder().WithPageNumber(3).WithPageSize(2).Build();
        var responsePage3 = await _movement.ListMovements(requestPage3);

        var bodyPage3 = await responsePage3.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        bodyPage3.Items.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("occurredon asc", SortDirection.Ascending)]
    [InlineData("occurredon desc", SortDirection.Descending)]
    public async Task UserCanListMovementsOrderedByOccurredDate(string orderBy, SortDirection sortDirection)
    {
        _auth.LoginAsUser();
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().Build());

        var request = new ListMovementsRequestBuilder().WithOrder(orderBy).Build();
        var response = await _movement.ListMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.ShouldNotBeEmpty();
        body.Items.Select(movement => movement.OccurredOn).ShouldBeInOrder(sortDirection);
    }

    [Theory]
    [InlineData("amount asc", SortDirection.Ascending)]
    [InlineData("amount desc", SortDirection.Descending)]
    public async Task UserCanListMovementsOrderedByAmount(string orderBy, SortDirection sortDirection)
    {
        _auth.LoginAsUser();
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().Build());

        var request = new ListMovementsRequestBuilder().WithOrder(orderBy).Build();
        var response = await _movement.ListMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.ShouldNotBeEmpty();
        body.Items.Select(movement => movement.Amount).ShouldBeInOrder(sortDirection);
    }

    [Fact]
    public async Task UserCanFilterMovementsByKind()
    {
        _auth.LoginAsUser();
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithKind(MovementKind.Expense).Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithKind(MovementKind.Income).Build());

        var request = new ListMovementsRequestBuilder().WithKind([MovementKind.Expense]).Build();
        var response = await _movement.ListMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.Count.ShouldBe(1);
        body.Items.ShouldAllBe(movement => movement.Kind == MovementKind.Expense);
    }

    [Fact]
    public async Task UserCanFilterMovementsByMinOccurredDate()
    {
        _auth.LoginAsUser();
        var occurredOn = DateTimeOffset.Parse("2025-10-10T00:00:00Z");
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(-1)).Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn).Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(1)).Build());

        var request = new ListMovementsRequestBuilder().WithMinOccurredOn(occurredOn).Build();
        var response = await _movement.ListMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(movement => movement.OccurredOn >= occurredOn);
    }

    [Fact]
    public async Task UserCanFilterMovementsByMaxOccurredDate()
    {
        _auth.LoginAsUser();
        var occurredOn = DateTimeOffset.Parse("2025-10-20T00:00:00Z");
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(-1)).Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn).Build());
        await _movement.EnsureMovementExists(new CreateMovementRequestBuilder().WithOccurredOn(occurredOn.AddDays(1)).Build());

        var request = new ListMovementsRequestBuilder().WithMaxOccurredOn(occurredOn).Build();
        var response = await _movement.ListMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<MovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(movement => movement.OccurredOn <= occurredOn);
    }

    [Fact]
    public async Task ListingMovementsWithAnInvalidRequestFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var request = new ListMovementsRequestBuilder().WithPageNumber(-1).Build();
        var response = await _movement.ListMovements(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotListMovements()
    {
        var request = new ListMovementsRequestBuilder().Build();
        var response = await _movement.ListMovements(request);

        response.ShouldBeUnauthorized();
    }
}
