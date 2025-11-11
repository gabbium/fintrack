using BuildingBlocks.Application.EventBus;
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

            builder.Property(eventLog => eventLog.EventType)
                .IsRequired();

            builder.Property(eventLog => eventLog.State)
                .HasConversion<string>()
                .HasMaxLength(30);

            builder.Property(eventLog => eventLog.CreatedAt);

            builder.Property(eventLog => eventLog.Content)
                .IsRequired();
        });
    }
}
