using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Application.Models;

public sealed record UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = null!;

    public static UserDto FromDomain(User user)
    {
        return new()
        {
            Id = user.Id,
            Email = user.Email
        };
    }
}
