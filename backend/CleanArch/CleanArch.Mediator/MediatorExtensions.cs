namespace CleanArch.Mediator;

public static class MediatorExtensions
{
    public static IServiceCollection AddMediator(
        this IServiceCollection services,
        Action<MediatorOptions>? configure = null)
    {
        var opts = new MediatorOptions();

        configure?.Invoke(opts);

        var handlerInterfaces = new List<Type>()
        {
            typeof(ICommandHandler<,>),
            typeof(IQueryHandler<,>),
            typeof(IDomainEventHandler<>)
        };

        services.Scan(scan => scan.FromAssemblies(opts._assemblies)
            .AddClasses(c => c.AssignableToAny(handlerInterfaces), publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        foreach (var behavior in opts._behaviors.Distinct())
        {
            services.AddScoped(typeof(IMediatorBehavior<,>), behavior);
        }

        return services;
    }
}
