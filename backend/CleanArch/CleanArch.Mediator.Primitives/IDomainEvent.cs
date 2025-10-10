namespace CleanArch.Mediator.Primitives;

public interface IDomainEvent
{
    DateTimeOffset RaisedAt { get; }
}
