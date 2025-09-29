namespace Fintrack.ServiceDefaults;

public static class AuthenticationExtensions
{
    public static IHostApplicationBuilder AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        var identitySection = builder.Configuration.GetSection("Identity");
        if (!identitySection.Exists())
        {
            return builder;
        }

        var audience = identitySection.GetRequiredValue("Audience");

        builder.Services.AddAuthentication()
            .AddKeycloakJwtBearer(
                serviceName: "keycloak",
                realm: "fintrack",
                options =>
                {
                    options.Audience = audience;

                    if (builder.Environment.IsDevelopment())
                    {
                        options.RequireHttpsMetadata = false;
                    }
                });

        builder.Services.AddAuthorization();

        return builder;
    }
}
