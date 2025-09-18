using Fintrack.Identity.Application.Interfaces;
using Fintrack.Identity.Application.Models;
using Fintrack.Identity.Domain.Entities;
using Fintrack.Identity.Domain.Interfaces;

namespace Fintrack.Identity.Application.Commands.PasswordlessLogin;

internal sealed class PasswordlessLoginCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IJwtService jwtService)
    : ICommandHandler<PasswordlessLoginCommand, AuthDto>
{
    public async Task<Result<AuthDto>> HandleAsync(
        PasswordlessLoginCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByEmailAsync(command.Email, cancellationToken);

        if (user is null)
        {
            user = new User(command.Email);
            await userRepository.AddAsync(user, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var accessToken = jwtService.CreateAccessToken(user);

        return AuthDto.FromDomain(user, accessToken);
    }
}
