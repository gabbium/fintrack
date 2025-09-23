using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.Entities;
using Fintrack.Ledger.Domain.Interfaces;

namespace Fintrack.Ledger.Application.Commands.CreateMovement;

internal sealed class CreateMovementCommandHandler(
    IUser user,
    IMovementRepository movementRepository,
    ILedgerUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovementCommand, MovementDto>
{
    public async Task<Result<MovementDto>> HandleAsync(
        CreateMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = new Movement(
            user.Id,
            command.Kind,
            command.Amount,
            command.Description,
            command.OccurredOn);

        await movementRepository.AddAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MovementDto.FromDomain(movement);
    }
}
