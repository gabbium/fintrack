using Fintrack.Ledger.Application.UseCases.ListMovements;
using Fintrack.Ledger.Domain.MovementAggregate;
using Fintrack.Ledger.Infrastructure.Queries;
using Fintrack.Ledger.Infrastructure.Repositories;

namespace Fintrack.Ledger.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LedgerDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("LedgerDb"));
        });

        builder.EnrichNpgsqlDbContext<LedgerDbContext>();

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LedgerDbContext>());

        builder.Services.AddScoped<IMovementRepository, MovementRepository>();
        builder.Services.AddScoped<IListMovementsQueryService, ListMovementsQueryService>();

        return builder;
    }
}
