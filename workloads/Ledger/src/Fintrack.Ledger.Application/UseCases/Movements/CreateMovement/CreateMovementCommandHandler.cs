using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.Movements;

namespace Fintrack.Ledger.Application.UseCases.Movements.CreateMovement;

internal sealed class CreateMovementCommandHandler(
    IUser user,
    IMovementRepository movementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovementCommand, MovementDto>
{
    public async Task<Result<MovementDto>> HandleAsync(
        CreateMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = new Movement(
            user.UserId,
            command.Kind,
            command.Amount,
            command.Description,
            command.OccurredOn);

        await movementRepository.AddAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MovementDto.FromDomain(movement);
    }
}
