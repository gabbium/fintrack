using BuildingBlocks.CleanArch;

namespace Fintrack.Planning.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.AddMediator(Assembly.GetExecutingAssembly());

        return builder;
    }
}
