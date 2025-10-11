using Fintrack.Planning.Api.Models;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Api.FunctionalTests.TestSupport.Builders;

public class ListPlannedMovementsRequestBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private string? _order;
    private PlannedMovementKind[]? _kind;
    private PlannedMovementStatus[]? _status;
    private DateTimeOffset? _minDueOn;
    private DateTimeOffset? _maxDueOn;

    public ListPlannedMovementsRequestBuilder WithPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public ListPlannedMovementsRequestBuilder WithPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public ListPlannedMovementsRequestBuilder WithOrder(string order)
    {
        _order = order;
        return this;
    }

    public ListPlannedMovementsRequestBuilder WithKind(PlannedMovementKind[] kind)
    {
        _kind = kind;
        return this;
    }

    public ListPlannedMovementsRequestBuilder WithStatus(PlannedMovementStatus[] status)
    {
        _status = status;
        return this;
    }

    public ListPlannedMovementsRequestBuilder WithMinDueOn(DateTimeOffset minDueOn)
    {
        _minDueOn = minDueOn;
        return this;
    }

    public ListPlannedMovementsRequestBuilder WithMaxDueOn(DateTimeOffset maxDueOn)
    {
        _maxDueOn = maxDueOn;
        return this;
    }

    public ListPlannedMovementsRequest Build()
    {
        return new()
        {
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            Order = _order,
            Kind = _kind,
            Status = _status,
            MinDueOn = _minDueOn,
            MaxDueOn = _maxDueOn
        };
    }
}
