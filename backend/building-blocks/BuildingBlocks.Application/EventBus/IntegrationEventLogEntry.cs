using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Application.EventBus;

public class IntegrationEventLogEntry
{
    public Guid EventId { get; set; }
    public string EventType { get; set; } = default!;
    public EventStateEnum State { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Content { get; set; } = default!;

    private IntegrationEventLogEntry()
    {
    }

    public IntegrationEventLogEntry(IntegrationEvent integrationEvent)
    {
        EventId = integrationEvent.Id;
        EventType = integrationEvent.GetType().AssemblyQualifiedName!;
        State = EventStateEnum.NotPublished;
        CreatedAt = integrationEvent.CreatedAt;
        Content = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType(), SerializerOptions);
    }

    public static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
}
