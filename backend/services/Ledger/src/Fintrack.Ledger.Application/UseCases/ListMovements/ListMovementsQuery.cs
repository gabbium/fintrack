using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UseCases.ListMovements;

public sealed record ListMovementsQuery(
    int PageNumber,
    int PageSize,
    string? Order,
    List<MovementKind>? Kind,
    DateTimeOffset? MinOccurredOn,
    DateTimeOffset? MaxOccurredOn)
    : IQuery<PaginatedList<MovementDto>>;
