namespace Fintrack.Ledger.Application.UseCases.DeleteMovement;

internal sealed class DeleteMovementCommandValidator
    : AbstractValidator<DeleteMovementCommand>
{
    public DeleteMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
