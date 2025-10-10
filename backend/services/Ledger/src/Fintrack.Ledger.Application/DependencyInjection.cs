namespace Fintrack.Ledger.Application;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMediator(config =>
        {
            config.FromAssembly(Assembly.GetExecutingAssembly());
            config.AddBehavior(typeof(LoggingBehavior<,>));
            config.AddBehavior(typeof(ValidationBehavior<,>));
        });

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        return builder;
    }
}
