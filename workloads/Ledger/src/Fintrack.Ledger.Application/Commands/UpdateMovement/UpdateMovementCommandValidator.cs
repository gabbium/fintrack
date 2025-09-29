namespace Fintrack.Ledger.Application.Commands.UpdateMovement;

internal sealed class UpdateMovementCommandValidator
    : AbstractValidator<UpdateMovementCommand>
{
    public UpdateMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();

        RuleFor(command => command.Amount)
            .GreaterThan(0)
            .PrecisionScale(18, 2, true);

        RuleFor(command => command.Description)
            .MaximumLength(128);
    }
}
