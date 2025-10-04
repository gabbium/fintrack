using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UseCases.UpdateMovement;

internal sealed class UpdateMovementCommandHandler(
    IMovementRepository movementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateMovementCommand, MovementDto>
{
    public async Task<Result<MovementDto>> HandleAsync(
        UpdateMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = await movementRepository.GetByIdAsync(command.Id, cancellationToken);

        if (movement is null)
        {
            return Error.NotFound("Movement was not found.");
        }

        movement.ChangeKind(command.Kind);
        movement.ChangeAmount(command.Amount);
        movement.ChangeDescription(command.Description);
        movement.ChangeOccurredOn(command.OccurredOn);

        await movementRepository.UpdateAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MovementDto.FromDomain(movement);
    }
}
