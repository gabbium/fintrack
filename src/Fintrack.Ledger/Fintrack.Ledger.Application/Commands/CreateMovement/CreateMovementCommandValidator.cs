namespace Fintrack.Ledger.Application.Commands.CreateMovement;

public class CreateMovementCommandValidator : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
