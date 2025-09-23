using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Application.Queries.ListMovements;
using Fintrack.Ledger.Domain.Enums;
using Fintrack.Ledger.Domain.Interfaces;
using Fintrack.Ledger.Infrastructure.Data;
using Fintrack.Ledger.Infrastructure.Data.Queries;
using Fintrack.Ledger.Infrastructure.Data.Repositories;

namespace Fintrack.Ledger.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LedgerDbContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"), npgsqlOptions =>
            {
                npgsqlOptions.MigrationsHistoryTable("__efmigrations_history", "ledger");
                npgsqlOptions.MapEnum<MovementKind>(schemaName: "ledger");
            });
        });

        builder.Services.AddScoped<ILedgerUnitOfWork>(sp => sp.GetRequiredService<LedgerDbContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<LedgerDbContext>(name: "ledgerdb", tags: ["ready"]);

        builder.Services.AddScoped<IMovementRepository, MovementRepository>();
        builder.Services.AddScoped<IListMovementsQueryService, ListMovementsQueryService>();

        return builder;
    }
}
