namespace Fintrack.Identity.Application.Commands.PasswordlessLogin;

internal sealed class PasswordlessLoginCommandValidator
    : AbstractValidator<PasswordlessLoginCommand>
{
    public PasswordlessLoginCommandValidator()
    {
        RuleFor(command => command.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256);
    }
}
