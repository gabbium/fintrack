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
        var authority = identitySection.GetRequiredValue("Authority");

        builder.Services.AddAuthentication()
            .AddJwtBearer(
                options =>
                {
                    options.Audience = audience;
                    options.Authority = authority;

                    if (builder.Environment.IsDevelopment())
                    {
                        options.RequireHttpsMetadata = false;
                    }
                });

        builder.Services.AddAuthorization();

        return builder;
    }
}
