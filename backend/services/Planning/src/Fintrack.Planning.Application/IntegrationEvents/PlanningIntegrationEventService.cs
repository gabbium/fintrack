using BuildingBlocks.Application.EventBus;

namespace Fintrack.Planning.Application.IntegrationEvents;

public class PlanningIntegrationEventService(
    IIntegrationEventLogService integrationEventLogService,
    ILogger<PlanningIntegrationEventService> logger) : IPlanningIntegrationEventService
{
    public async Task AddAndSaveEventAsync(IntegrationEvent integrationEvent)
    {
        logger.LogInformation(
            "Enqueuing integration event {IntegrationEventId} to repository ({@IntegrationEvent})",
            integrationEvent.Id,
            integrationEvent);

        await integrationEventLogService.AddEventAsync(integrationEvent);
    }
}
