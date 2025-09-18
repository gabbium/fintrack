using Fintrack.Identity.Application.Models;

namespace Fintrack.Identity.Application.Commands.PasswordlessLogin;

public sealed record PasswordlessLoginCommand(
    string Email)
    : ICommand<AuthDto>;
