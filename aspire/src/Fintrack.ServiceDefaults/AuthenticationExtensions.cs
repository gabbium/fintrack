namespace Fintrack.ServiceDefaults;

public static class AuthenticationExtensions
{
    public static IHostApplicationBuilder AddAuthenticationDefaults(this IHostApplicationBuilder builder)
    {
        var identitySection = builder.Configuration.GetRequiredSection("Identity");
        var realm = identitySection.GetRequiredValue("Realm");
        var audience = identitySection.GetRequiredValue("Audience");

        builder.Services.AddAuthentication()
            .AddKeycloakJwtBearer(
                serviceName: "keycloak",
                realm: realm,
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
