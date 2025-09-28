using Fintrack.Ledger.Domain.Movements;
using Fintrack.Ledger.Infrastructure.Data;
using Fintrack.Ledger.Infrastructure.Data.Repositories;

namespace Fintrack.Ledger.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LedgerDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgresdb"), npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("__efmigrations_history", "ledger");
                npgsqlOptions.MapEnum<MovementKind>(schemaName: "ledger");
            });
        });

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LedgerDbContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<LedgerDbContext>(name: "ledgerdb", tags: ["ready"]);

        builder.Services.AddScoped<IMovementRepository, MovementRepository>();

        return builder;
    }
}
