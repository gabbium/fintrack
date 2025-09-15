using Fintrack.Ledger.Application;
using Fintrack.Ledger.Infrastructure;

namespace Fintrack.API;

public static class LedgerDependencyInjection
{
    public static IHostApplicationBuilder AddLedgerServices(this IHostApplicationBuilder builder)
    {
        builder.AddApplicationServices();
        builder.AddInfrastructureServices();

        return builder;
    }
}
