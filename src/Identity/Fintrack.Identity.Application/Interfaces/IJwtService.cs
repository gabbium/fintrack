using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Application.Interfaces;

public interface IJwtService
{
    string CreateAccessToken(User user);
}
