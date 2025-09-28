using Fintrack.Ledger.API.Models;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Builders;

public class ListMovementsRequestBuilder
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

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

    public ListMovementsRequest Build()
    {
        return new()
        {
            PageNumber = _pageNumber,
            PageSize = _pageSize
        };
    }
}
