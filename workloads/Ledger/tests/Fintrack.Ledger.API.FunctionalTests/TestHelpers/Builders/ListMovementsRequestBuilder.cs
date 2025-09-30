using Fintrack.Ledger.API.Models;
using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;

public class ListMovementsRequestBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
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

    public ListMovementsRequestBuilder WithKinds(MovementKind[] kinds)
    {
        _kind = kinds;
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
            Kind = _kind,
            MinOccurredOn = _minOccurredOn,
            MaxOccurredOn = _maxOccurredOn
        };
    }
}
