using BuildingBlocks.Application.Identity;
using Fintrack.Ledger.Infrastructure;

namespace Fintrack.Ledger.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMigration<LedgerDbContext>();

        builder.Services.AddTransient<IIdentityService, EmptyIdentityService>();

        return builder;
    }
}
