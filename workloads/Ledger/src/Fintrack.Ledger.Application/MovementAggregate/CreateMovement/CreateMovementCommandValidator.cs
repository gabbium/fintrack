namespace Fintrack.Ledger.Application.MovementAggregate.CreateMovement;

internal sealed class CreateMovementCommandValidator
    : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementCommandValidator()
    {
        RuleFor(c => c.Amount)
            .GreaterThan(0m)
            .PrecisionScale(18, 2, true);

        RuleFor(c => c.Description)
            .MaximumLength(128);
    }
}
