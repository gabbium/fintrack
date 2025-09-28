namespace Fintrack.Ledger.API.Apis;

public sealed class ListMovementsRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
