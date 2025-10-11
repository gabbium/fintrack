using BuildingBlocks.Api.Authentication.OidcJwt;

namespace BuildingBlocks.Api.Authentication;

public static class AuthenticationExtension
{
    public static IHostApplicationBuilder AddOidcJwtAuthentication(
        this IHostApplicationBuilder builder)
    {
        builder.Services
            .AddSingleton<IValidateOptions<OidcJwtOptions>, OidcJwtOptionsValidator>()
            .AddOptions<OidcJwtOptions>()
            .BindConfiguration(OidcJwtOptions.SectionName)
            .ValidateOnStart();

        var oidcJwtOptions = builder.Configuration
            .GetSection(OidcJwtOptions.SectionName)
            .Get<OidcJwtOptions>()!;

        builder.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Audience = oidcJwtOptions.Audience;
                options.Authority = oidcJwtOptions.Authority;
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
            });

        builder.Services.AddAuthorization();

        return builder;
    }
}
