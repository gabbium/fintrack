using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.EventBus;

public static class IntegrationLogExtensions
{
    public static void UseIntegrationEventLogs(this ModelBuilder builder)
    {
        builder.Entity<IntegrationEventLogEntry>(builder =>
        {
            builder.ToTable("IntegrationEventLogs");
            builder.HasKey(eventLog => eventLog.EventId);
        });
    }
}
