using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.MovementAggregate.CreateMovement;

internal sealed class CreateMovementCommandHandler(
    IMovementRepository movementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovementCommand, MovementDto>
{
    public async Task<Result<MovementDto>> HandleAsync(
        CreateMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = new Movement(
            Guid.NewGuid(),
            command.Kind,
            command.Amount,
            command.Description,
            command.OccurredOn);

        await movementRepository.AddAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MovementDto.FromDomain(movement);
    }
}
