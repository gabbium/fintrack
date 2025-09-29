namespace Fintrack.Ledger.Application.Commands.DeleteMovement;

internal sealed class DeleteMovementCommandValidator
    : AbstractValidator<DeleteMovementCommand>
{
    public DeleteMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
