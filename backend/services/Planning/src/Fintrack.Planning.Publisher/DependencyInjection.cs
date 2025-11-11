using BuildingBlocks.Application.Identity;
using Fintrack.Planning.Publisher.HostedServices;

namespace Fintrack.Planning.Publisher;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddPublisherServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<IntegrationEventPublisher>();

        builder.Services.AddTransient<IIdentityService, EmptyIdentityService>();

        return builder;
    }
}
