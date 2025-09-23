using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.Interfaces;

namespace Fintrack.Ledger.Application.Queries.GetMovementById;

internal sealed class GetMovementByIdQueryHandler(
    IMovementRepository movementRepository)
    : IQueryHandler<GetMovementByIdQuery, MovementDto>
{
    public async Task<Result<MovementDto>> HandleAsync(
        GetMovementByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var movement = await movementRepository.GetByIdAsync(query.Id, cancellationToken);

        if (movement is null)
            return Error.NotFound("Movement was not found.");

        return MovementDto.FromDomain(movement);
    }
}
