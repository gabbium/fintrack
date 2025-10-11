using Fintrack.Planning.Api.FunctionalTests.TestSupport;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Builders;
using Fintrack.Planning.Api.FunctionalTests.TestSupport.Clients;
using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.FunctionalTests.Scenarios.PlannedMovements;

public class ListPlannedMovementsTests : TestsBase
{
    private readonly AuthClient _auth;
    private readonly PlannedMovementClient _plannedMovement;

    public ListPlannedMovementsTests(TestsFixture fx) : base(fx)
    {
        var httpClient = fx.Factory.CreateDefaultClient();
        _auth = new(httpClient);
        _plannedMovement = new(httpClient);
    }

    [Fact]
    public async Task UserCanListPlannedMovementsWithPagination()
    {
        _auth.LoginAsUser();
        await _plannedMovement.EnsureActivePlannedMovementExists();
        await _plannedMovement.EnsureActivePlannedMovementExists();
        await _plannedMovement.EnsureActivePlannedMovementExists();
        await _plannedMovement.EnsureActivePlannedMovementExists();

        var requestPage1 = new ListPlannedMovementsRequestBuilder().WithPageNumber(1).WithPageSize(2).Build();
        var responsePage1 = await _plannedMovement.ListPlannedMovements(requestPage1);

        var bodyPage1 = await responsePage1.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        bodyPage1.Items.Count.ShouldBe(2);

        var requestPage2 = new ListPlannedMovementsRequestBuilder().WithPageNumber(2).WithPageSize(2).Build();
        var responsePage2 = await _plannedMovement.ListPlannedMovements(requestPage2);

        var bodyPage2 = await responsePage2.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        bodyPage2.Items.Count.ShouldBe(2);

        var requestPage3 = new ListPlannedMovementsRequestBuilder().WithPageNumber(3).WithPageSize(2).Build();
        var responsePage3 = await _plannedMovement.ListPlannedMovements(requestPage3);

        var bodyPage3 = await responsePage3.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        bodyPage3.Items.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("dueon asc", SortDirection.Ascending)]
    [InlineData("dueon desc", SortDirection.Descending)]
    public async Task UserCanListPlannedMovementsOrderedByDueDate(string orderBy, SortDirection sortDirection)
    {
        _auth.LoginAsUser();
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().Build());

        var request = new ListPlannedMovementsRequestBuilder().WithOrder(orderBy).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.ShouldNotBeEmpty();
        body.Items.Select(plannedMovement => plannedMovement.DueOn).ShouldBeInOrder(sortDirection);
    }

    [Theory]
    [InlineData("amount asc", SortDirection.Ascending)]
    [InlineData("amount desc", SortDirection.Descending)]
    public async Task UserCanListPlannedMovementsOrderedByAmount(string orderBy, SortDirection sortDirection)
    {
        _auth.LoginAsUser();
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().Build());

        var request = new ListPlannedMovementsRequestBuilder().WithOrder(orderBy).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.ShouldNotBeEmpty();
        body.Items.Select(plannedMovement => plannedMovement.Amount).ShouldBeInOrder(sortDirection);
    }

    [Fact]
    public async Task UserCanFilterPlannedMovementsByKind()
    {
        _auth.LoginAsUser();
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithKind(PlannedMovementKind.Expense).Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithKind(PlannedMovementKind.Income).Build());

        var request = new ListPlannedMovementsRequestBuilder().WithKind([PlannedMovementKind.Expense]).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(1);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.Kind == PlannedMovementKind.Expense);
    }

    [Fact]
    public async Task UserCanFilterPlannedMovementsByStatus()
    {
        _auth.LoginAsUser();
        await _plannedMovement.EnsureActivePlannedMovementExists();
        await _plannedMovement.EnsureCanceledPlannedMovementExists();

        var request = new ListPlannedMovementsRequestBuilder().WithStatus([PlannedMovementStatus.Active]).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(1);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.Status == PlannedMovementStatus.Active);
    }

    [Fact]
    public async Task UserCanFilterPlannedMovementsByMinDueDate()
    {
        _auth.LoginAsUser();
        var dueOn = DateTimeOffset.Parse("2025-10-10T00:00:00Z");
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(-1)).Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn).Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(1)).Build());

        var request = new ListPlannedMovementsRequestBuilder().WithMinDueOn(dueOn).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.DueOn >= dueOn);
    }

    [Fact]
    public async Task UserCanFilterPlannedMovementsByMaxDueDate()
    {
        _auth.LoginAsUser();
        var dueOn = DateTimeOffset.Parse("2025-10-20T00:00:00Z");
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(-1)).Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn).Build());
        await _plannedMovement.EnsureActivePlannedMovementExists(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(1)).Build());

        var request = new ListPlannedMovementsRequestBuilder().WithMaxDueOn(dueOn).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.DueOn <= dueOn);
    }

    [Fact]
    public async Task ListingPlannedMovementsWithAnInvalidRequestFailsWithValidationError()
    {
        _auth.LoginAsUser();

        var request = new ListPlannedMovementsRequestBuilder().WithPageNumber(-1).Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task AnonymousUserCannotListPlannedMovements()
    {
        var request = new ListPlannedMovementsRequestBuilder().Build();
        var response = await _plannedMovement.ListPlannedMovements(request);

        response.ShouldBeUnauthorized();
    }
}

