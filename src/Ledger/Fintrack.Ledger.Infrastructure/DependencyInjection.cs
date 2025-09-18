using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Infrastructure.Data;

namespace Fintrack.Ledger.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LedgerDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("postgres"),
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable("__efmigrations_history", "ledger"));
        });

        builder.Services.AddScoped<ILedgerUnitOfWork>(sp => sp.GetRequiredService<LedgerDbContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<LedgerDbContext>(name: "ledgerdb", tags: ["ready"]);

        return builder;
    }
}
