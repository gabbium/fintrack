namespace CleanArch.Mediator.Primitives;

public abstract class DomainEventBase : IDomainEvent
{
    public DateTimeOffset RaisedAt { get; } = DateTimeOffset.UtcNow;
}
