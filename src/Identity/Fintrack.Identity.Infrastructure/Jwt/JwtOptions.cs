namespace Fintrack.Identity.Infrastructure.Jwt;

public sealed class JwtOptions
{
    [Required]
    public string Secret { get; set; } = default!;

    [Required]
    public string Issuer { get; set; } = default!;

    [Required]
    public string Audience { get; set; } = default!;

    [Range(1, 60)]
    public int ExpirationMinutes { get; set; } = default!;
}
