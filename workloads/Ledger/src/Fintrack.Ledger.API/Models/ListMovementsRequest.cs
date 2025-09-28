namespace Fintrack.Ledger.API.Models;

public sealed class ListMovementsRequest
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}
