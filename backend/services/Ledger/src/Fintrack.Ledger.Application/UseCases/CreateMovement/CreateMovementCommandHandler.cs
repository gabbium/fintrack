using BuildingBlocks.Application.Identity;
using Fintrack.Ledger.Application.Models;
using Fintrack.Ledger.Domain.MovementAggregate;

namespace Fintrack.Ledger.Application.UseCases.CreateMovement;

internal sealed class CreateMovementCommandHandler(
    IIdentityService identityService,
    IMovementRepository movementRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateMovementCommand, Result<MovementDto>>
{
    public async Task<Result<MovementDto>> HandleAsync(
        CreateMovementCommand command,
        CancellationToken cancellationToken = default)
    {
        var movement = new Movement(
            identityService.GetUserIdentity(),
            command.Kind,
            command.Amount,
            command.Description,
            command.OccurredOn);

        await movementRepository.AddAsync(movement, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MovementDto.FromDomain(movement);
    }
}
