namespace Fintrack.Ledger.Application.Commands.CreateMovement;

internal sealed class CreateMovementCommandValidator
    : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementCommandValidator()
    {
        RuleFor(command => command.Amount)
            .GreaterThan(0)
            .PrecisionScale(18, 2, true);

        RuleFor(command => command.Description)
            .MaximumLength(128);
    }
}
