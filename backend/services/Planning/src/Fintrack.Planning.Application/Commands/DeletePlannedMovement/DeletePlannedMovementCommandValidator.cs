namespace Fintrack.Planning.Application.Commands.DeletePlannedMovement;

internal sealed class DeletePlannedMovementCommandValidator
    : AbstractValidator<DeletePlannedMovementCommand>
{
    public DeletePlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
