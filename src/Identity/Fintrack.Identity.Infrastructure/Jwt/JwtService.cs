using Fintrack.Identity.Application.Interfaces;
using Fintrack.Identity.Domain.Entities;

namespace Fintrack.Identity.Infrastructure.Jwt;

internal sealed class JwtService(IConfiguration configuration) : IJwtService
{
    public string CreateAccessToken(User user)
    {
        var identity = configuration.GetRequiredSection("Identity");
        var secret = identity["Secret"];
        var issuer = identity["Issuer"];
        var audience = identity["Audience"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
