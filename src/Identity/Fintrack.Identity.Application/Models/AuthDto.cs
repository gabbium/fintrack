using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Application.Models;

public sealed record AuthDto
{
    public UserDto User { get; init; } = default!;
    public string AccessToken { get; init; } = null!;

    public static AuthDto FromDomain(User user, string accessToken)
    {
        return new()
        {
            User = UserDto.FromDomain(user),
            AccessToken = accessToken
        };
    }
}
