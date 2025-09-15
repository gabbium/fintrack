using Fintrack.Identity.Application;
using Fintrack.Identity.Infrastructure;

namespace Fintrack.API;

public static class IdentityDependencyInjection
{
    public static IHostApplicationBuilder AddIdentityServices(this IHostApplicationBuilder builder)
    {
        builder.AddApplicationServices();
        builder.AddInfrastructureServices();

        return builder;
    }
}
