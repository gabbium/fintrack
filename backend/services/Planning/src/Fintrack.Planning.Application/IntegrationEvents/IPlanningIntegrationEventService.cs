using BuildingBlocks.Application.EventBus;

namespace Fintrack.Planning.Application.IntegrationEvents;

public interface IPlanningIntegrationEventService
{
    Task AddAndSaveEventAsync(IntegrationEvent integrationEvent);
}
