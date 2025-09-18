using Fintrack.Identity.Application.Interfaces;
using Fintrack.Identity.Domain.Interfaces;
using Fintrack.Identity.Infrastructure.Data;
using Fintrack.Identity.Infrastructure.Data.Repositories;
using Fintrack.Identity.Infrastructure.Jwt;

namespace Fintrack.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
        });

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<IdentityDbContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<IdentityDbContext>(name: "identitydb", tags: ["ready"]);

        builder.Services.AddSingleton<IJwtService, JwtService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}
