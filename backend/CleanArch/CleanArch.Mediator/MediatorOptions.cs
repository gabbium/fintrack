namespace CleanArch.Mediator;

public sealed class MediatorOptions
{
    internal readonly HashSet<Assembly> _assemblies = [];
    internal readonly List<Type> _behaviors = [];

    public MediatorOptions FromAssembly(Assembly assembly)
    {
        _assemblies.Add(assembly);
        return this;
    }

    public MediatorOptions AddBehavior(Type openGenericBehavior)
    {
        _behaviors.Add(openGenericBehavior);
        return this;
    }
}
