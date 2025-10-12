using Fintrack.Planning.Application.Queries.ListPlannedMovements;
using Fintrack.Planning.Domain.PlannedMovementAggregate;

namespace Fintrack.Planning.Application.UnitTests.TestSupport.Builders;

public class ListPlannedMovementsQueryBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private string? _order;
    private List<PlannedMovementKind>? _kind;
    private List<PlannedMovementStatus>? _status;
    private DateTimeOffset? _minDueOn;
    private DateTimeOffset? _maxDueOn;

    public ListPlannedMovementsQueryBuilder WithPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public ListPlannedMovementsQueryBuilder WithPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public ListPlannedMovementsQueryBuilder WithOrder(string order)
    {
        _order = order;
        return this;
    }

    public ListPlannedMovementsQueryBuilder WithKind(List<PlannedMovementKind> kind)
    {
        _kind = kind;
        return this;
    }

    public ListPlannedMovementsQueryBuilder WithStatus(List<PlannedMovementStatus> status)
    {
        _status = status;
        return this;
    }

    public ListPlannedMovementsQueryBuilder WithMinDueOn(DateTimeOffset minDueOn)
    {
        _minDueOn = minDueOn;
        return this;
    }

    public ListPlannedMovementsQueryBuilder WithMaxDueOn(DateTimeOffset maxDueOn)
    {
        _maxDueOn = maxDueOn;
        return this;
    }

    public ListPlannedMovementsQuery Build()
    {
        return new(
            _pageNumber,
            _pageSize,
            _order,
            _kind,
            _status,
            _minDueOn,
            _maxDueOn
        );
    }
}
