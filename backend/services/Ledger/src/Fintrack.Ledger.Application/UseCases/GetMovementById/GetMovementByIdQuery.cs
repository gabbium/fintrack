using Fintrack.Ledger.Application.Models;

namespace Fintrack.Ledger.Application.UseCases.GetMovementById;

public sealed record GetMovementByIdQuery(
    Guid Id)
    : IQuery<MovementDto>;
