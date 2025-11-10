using BuildingBlocks.Application.Behaviors;
using Fintrack.Planning.Application.IntegrationEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Fintrack.Planning.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPlanningIntegrationEventService, PlanningIntegrationEventService>();

        builder.Services.AddMediator(config =>
        {
            config.FromAssembly(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(LoggingBehavior<,>));
            config.AddBehavior(typeof(ValidationBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly(),
            includeInternalTypes: true);

        return builder;
    }
}
