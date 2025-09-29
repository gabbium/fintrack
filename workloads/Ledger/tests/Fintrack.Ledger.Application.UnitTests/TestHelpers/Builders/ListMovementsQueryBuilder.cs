using Fintrack.Ledger.Application.Queries.ListMovements;
using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Application.UnitTests.TestHelpers.Builders;

public class ListMovementsQueryBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private List<MovementKind> _kinds = [];

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

    public ListMovementsQueryBuilder WithKinds(List<MovementKind> kinds)
    {
        _kinds = kinds;
        return this;
    }

    public ListMovementsQuery Build()
    {
        return new(_pageNumber, _pageSize, _kinds);
    }
}
