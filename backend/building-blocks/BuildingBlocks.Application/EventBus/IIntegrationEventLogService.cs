namespace BuildingBlocks.Application.EventBus;

public interface IIntegrationEventLogService
{
    Task AddEventAsync(IntegrationEvent integrationEvent);
}
