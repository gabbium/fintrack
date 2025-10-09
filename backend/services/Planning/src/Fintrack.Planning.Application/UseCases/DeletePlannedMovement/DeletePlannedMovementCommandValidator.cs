namespace Fintrack.Planning.Application.UseCases.DeletePlannedMovement;

internal sealed class DeletePlannedMovementCommandValidator
    : AbstractValidator<DeletePlannedMovementCommand>
{
    public DeletePlannedMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
