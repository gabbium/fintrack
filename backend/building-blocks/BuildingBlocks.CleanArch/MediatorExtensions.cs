namespace BuildingBlocks.CleanArch;

public static class MediatorExtensions
{
    public static IHostApplicationBuilder AddMediator(this IHostApplicationBuilder builder, Assembly assembly)
    {
        builder.Services.Scan(source => source.FromAssemblies(assembly)
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime()
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime()
            .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime()
            .AddClasses(filter => filter.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
                .AsImplementedInterfaces().WithScopedLifetime());

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationBehavior.CommandHandler<>));
        builder.Services.TryDecorate(typeof(ICommandHandler<,>), typeof(ValidationBehavior.CommandHandler<,>));
        builder.Services.TryDecorate(typeof(IQueryHandler<,>), typeof(ValidationBehavior.QueryHandler<,>));

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingBehavior.CommandHandler<>));
        builder.Services.TryDecorate(typeof(ICommandHandler<,>), typeof(LoggingBehavior.CommandHandler<,>));
        builder.Services.TryDecorate(typeof(IQueryHandler<,>), typeof(LoggingBehavior.QueryHandler<,>));

        builder.Services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        builder.Services.AddScoped<IMediator, Mediator>();

        return builder;
    }
}
