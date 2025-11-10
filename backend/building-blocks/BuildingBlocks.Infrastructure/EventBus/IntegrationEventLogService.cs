using BuildingBlocks.Application.EventBus;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EventBus;

public class IntegrationEventLogService<TContext>(TContext context) : IIntegrationEventLogService
    where TContext : DbContext
{
    public async Task AddEventAsync(IntegrationEvent integrationEvent)
    {
        var integrationEventLogEntry = new IntegrationEventLogEntry(integrationEvent);
        await context.Set<IntegrationEventLogEntry>().AddAsync(integrationEventLogEntry);
    }
}
