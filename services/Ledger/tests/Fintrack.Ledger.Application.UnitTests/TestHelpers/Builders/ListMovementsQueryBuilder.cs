using Fintrack.Ledger.Application.UseCases.ListMovements;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

public class ListMovementsQueryBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private string? _order;
    private List<MovementKind>? _kind;
    private DateTimeOffset? _minOccurredOn;
    private DateTimeOffset? _maxOccurredOn;

    public ListMovementsQueryBuilder WithPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public ListMovementsQueryBuilder WithPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public ListMovementsQueryBuilder WithOrder(string order)
    {
        _order = order;
        return this;
    }

    public ListMovementsQueryBuilder WithKind(List<MovementKind> kind)
    {
        _kind = kind;
        return this;
    }

    public ListMovementsQueryBuilder WithMinOccurredOn(DateTimeOffset minOccurredOn)
    {
        _minOccurredOn = minOccurredOn;
        return this;
    }

    public ListMovementsQueryBuilder WithMaxOccurredOn(DateTimeOffset maxOccurredOn)
    {
        _maxOccurredOn = maxOccurredOn;
        return this;
    }

    public ListMovementsQuery Build()
    {
        return new(
            _pageNumber,
            _pageSize,
            _order,
            _kind,
            _minOccurredOn,
            _maxOccurredOn
        );
    }
}
