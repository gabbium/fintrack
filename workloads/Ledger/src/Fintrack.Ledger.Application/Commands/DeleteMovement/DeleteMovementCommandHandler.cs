using Fintrack.Ledger.Domain.AggregatesModel.MovementAggregate;

namespace Fintrack.Ledger.Application.Commands.DeleteMovement;

internal sealed class DeleteMovementCommandHandler(
    IMovementRepository movementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMovementCommand>
{
    public async Task<Result> HandleAsync(
        DeleteMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = await movementRepository.GetByIdAsync(command.Id, cancellationToken);

        if (movement is null)
        {
            return Result.Success();
        }

        await movementRepository.RemoveAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
