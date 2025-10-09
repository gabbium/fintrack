using BuildingBlocks.MigrationService.HostedServices;
using Fintrack.Ledger.Application.Interfaces;
using Fintrack.Ledger.Infrastructure;
using Fintrack.Ledger.MigrationService.Services;

namespace Fintrack.Ledger.MigrationService;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWorkerServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<DbMigrationHostedService<LedgerDbContext>>();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

        return builder;
    }
}
