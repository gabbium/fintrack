using Fintrack.Ledger.Api.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Api.FunctionalTests.TestSupport.Builders;

public class ListMovementsRequestBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private string? _order;
    private MovementKind[]? _kind;
    private DateTimeOffset? _minOccurredOn;
    private DateTimeOffset? _maxOccurredOn;

    public ListMovementsRequestBuilder WithPageNumber(int pageNumber)
    {
        _pageNumber = pageNumber;
        return this;
    }

    public ListMovementsRequestBuilder WithPageSize(int pageSize)
    {
        _pageSize = pageSize;
        return this;
    }

    public ListMovementsRequestBuilder WithOrder(string order)
    {
        _order = order;
        return this;
    }

    public ListMovementsRequestBuilder WithKind(MovementKind[] kind)
    {
        _kind = kind;
        return this;
    }

    public ListMovementsRequestBuilder WithMinOccurredOn(DateTimeOffset minOccurredOn)
    {
        _minOccurredOn = minOccurredOn;
        return this;
    }

    public ListMovementsRequestBuilder WithMaxOccurredOn(DateTimeOffset maxOccurredOn)
    {
        _maxOccurredOn = maxOccurredOn;
        return this;
    }

    public ListMovementsRequest Build()
    {
        return new()
        {
            PageNumber = _pageNumber,
            PageSize = _pageSize,
            Order = _order,
            Kind = _kind,
            MinOccurredOn = _minOccurredOn,
            MaxOccurredOn = _maxOccurredOn
        };
    }
}
