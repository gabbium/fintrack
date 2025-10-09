using BuildingBlocks.CleanArch;

namespace Fintrack.Ledger.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddMediator(Assembly.GetExecutingAssembly());

        return builder;
    }
}
