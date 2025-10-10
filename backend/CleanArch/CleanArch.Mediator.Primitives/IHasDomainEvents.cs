namespace CleanArch.Mediator.Primitives;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
}
