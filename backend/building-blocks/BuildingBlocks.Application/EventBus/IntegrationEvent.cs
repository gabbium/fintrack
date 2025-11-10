namespace BuildingBlocks.Application.EventBus;

public record IntegrationEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;
}
