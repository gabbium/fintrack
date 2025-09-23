using Fintrack.Identity.Infrastructure.Jwt;

namespace Fintrack.API.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IHostApplicationBuilder AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOptions<JwtOptions>()
            .Bind(builder.Configuration.GetSection("Jwt"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

        ArgumentNullException.ThrowIfNull(jwtOptions);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
