using Fintrack.Ledger.Infrastructure.Data;

namespace Fintrack.Ledger.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LedgerDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
        });

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LedgerDbContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<LedgerDbContext>(name: "ledgerdb", tags: ["ready"]);

        return builder;
    }
}
