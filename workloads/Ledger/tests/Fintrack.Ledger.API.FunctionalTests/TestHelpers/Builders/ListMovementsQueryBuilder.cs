using Fintrack.Ledger.Application.Queries.ListMovements;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;

public class ListMovementsQueryBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

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

    public ListMovementsQuery Build()
    {
        return new(_pageNumber, _pageSize);
    }
}
