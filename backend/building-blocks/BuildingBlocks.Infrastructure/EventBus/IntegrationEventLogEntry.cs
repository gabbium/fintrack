using System.Text.Json;
using BuildingBlocks.Application.EventBus;

namespace BuildingBlocks.Infrastructure.EventBus;

public class IntegrationEventLogEntry
{
    private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };

    public Guid EventId { get; private set; }
    public string? EventTypeName { get; private set; }
    public EventStateEnum State { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public string? Content { get; private set; }

    private IntegrationEventLogEntry()
    {
    }

    public IntegrationEventLogEntry(IntegrationEvent integrationEvent)
    {
        EventId = integrationEvent.Id;
        EventTypeName = integrationEvent.GetType().Name;
        State = EventStateEnum.NotPublished;
        CreatedAt = integrationEvent.CreatedAt;
        Content = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType(), s_indentedOptions);
    }
}
