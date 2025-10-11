using BuildingBlocks.Api.FunctionalTests.Assertions;
using Fintrack.Planning.Api.FunctionalTests.Steps;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers;
using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Builders;
using Fintrack.Planning.Application.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.FunctionalTests.Apis.PlannedMovements;

public class ListPlannedMovementsTests(FunctionalTestsFixture fx) : FunctionalTestsBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly PlannedMovementSteps _plannedMovement = new(fx);

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovements_WhenListingPlannedMovementsWithPagination_ThenOkWithBodyAndCorrectPagination()
    {
        _auth.Given_LoggedInUser();
        await _plannedMovement.Given_ExistingPlannedMovement();
        await _plannedMovement.Given_ExistingPlannedMovement();
        await _plannedMovement.Given_ExistingPlannedMovement();
        await _plannedMovement.Given_ExistingPlannedMovement();

        var requestPage1 = new ListPlannedMovementsRequestBuilder().WithPageNumber(1).WithPageSize(2).Build();
        var responsePage1 = await _plannedMovement.When_AttemptToList(requestPage1);

        var bodyPage1 = await responsePage1.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        bodyPage1.Items.Count.ShouldBe(2);

        var requestPage2 = new ListPlannedMovementsRequestBuilder().WithPageNumber(2).WithPageSize(2).Build();
        var responsePage2 = await _plannedMovement.When_AttemptToList(requestPage2);

        var bodyPage2 = await responsePage2.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        bodyPage2.Items.Count.ShouldBe(2);

        var requestPage3 = new ListPlannedMovementsRequestBuilder().WithPageNumber(3).WithPageSize(2).Build();
        var responsePage3 = await _plannedMovement.When_AttemptToList(requestPage3);

        var bodyPage3 = await responsePage3.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        bodyPage3.Items.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovements_WhenListingPlannedMovementsWithOrder_ThenOkWithBodyOrdered()
    {
        _auth.Given_LoggedInUser();
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().Build());

        var request = new ListPlannedMovementsRequestBuilder().WithOrder("dueon asc").Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.ShouldNotBeEmpty();
        body.Items.Select(plannedMovement => plannedMovement.DueOn).ShouldBeInOrder(SortDirection.Ascending);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovements_WhenListingPlannedMovementsWithKindFilter_ThenOkWithBodyFilteredByKind()
    {
        _auth.Given_LoggedInUser();
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithKind(PlannedMovementKind.Expense).Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithKind(PlannedMovementKind.Income).Build());

        var request = new ListPlannedMovementsRequestBuilder().WithKind([PlannedMovementKind.Expense]).Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(1);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.Kind == PlannedMovementKind.Expense);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovements_WhenListingPlannedMovementsWithStatusFilter_ThenOkWithBodyFilteredByStatus()
    {
        _auth.Given_LoggedInUser();
        await _plannedMovement.Given_ExistingPlannedMovement();
        await _plannedMovement.Given_ExistingCanceledPlannedMovement();

        var request = new ListPlannedMovementsRequestBuilder().WithStatus([PlannedMovementStatus.Active]).Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(1);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.Status == PlannedMovementStatus.Active);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovements_WhenListingPlannedMovementsWithMinDueOnFilter_ThenOkWithBodyFilteredByDueOn()
    {
        _auth.Given_LoggedInUser();
        var dueOn = DateTimeOffset.Parse("2025-10-10T00:00:00Z");
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(-1)).Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn).Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(1)).Build());

        var request = new ListPlannedMovementsRequestBuilder().WithMinDueOn(dueOn).Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.DueOn >= dueOn);
    }

    [Fact]
    public async Task GivenLoggedInUserAndExistingPlannedMovements_WhenListingPlannedMovementsWithMaxDueOnFilter_ThenOkWithBodyFilteredByDueOn()
    {
        _auth.Given_LoggedInUser();
        var dueOn = DateTimeOffset.Parse("2025-10-20T00:00:00Z");
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(-1)).Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn).Build());
        await _plannedMovement.Given_ExistingPlannedMovement(new CreatePlannedMovementRequestBuilder().WithDueOn(dueOn.AddDays(1)).Build());

        var request = new ListPlannedMovementsRequestBuilder().WithMaxDueOn(dueOn).Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        var body = await response.ShouldBeOkWithBody<PaginatedList<PlannedMovementDto>>();
        body.Items.Count.ShouldBe(2);
        body.Items.ShouldAllBe(plannedMovement => plannedMovement.DueOn <= dueOn);
    }

    [Fact]
    public async Task GivenLoggedInUser_WhenListingPlannedMovementsWithInvalidRequest_ThenBadRequestWithValidation()
    {
        _auth.Given_LoggedInUser();

        var request = new ListPlannedMovementsRequestBuilder().WithPageNumber(-1).Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        await response.ShouldBeBadRequestWithValidation();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenListingPlannedMovements_ThenUnauthorizedWithBearerChallenge()
    {
        _auth.Given_AnonymousUser();

        var request = new ListPlannedMovementsRequestBuilder().Build();
        var response = await _plannedMovement.When_AttemptToList(request);

        response.ShouldBeUnauthorizedWithBearerChallenge();
    }
}

