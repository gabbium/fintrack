namespace CleanArch.Persistence.Primitives;

public interface IRepository<T> where T : IAggregateRoot;
