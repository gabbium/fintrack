namespace Fintrack.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
        });

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<IdentityContext>(name: "identitydb", tags: ["ready"]);

        return builder;
    }
}
