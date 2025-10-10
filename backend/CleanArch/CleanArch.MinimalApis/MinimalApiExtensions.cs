namespace CleanArch.MinimalApis;

public static class MinimalApiExtensions
{
    public static IServiceCollection AddMinimalApisFromAssembly(
        this IServiceCollection services,
        Assembly assembly,
        bool includeInternalTypes = false)
    {
        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo<IMinimalApi>(), publicOnly: !includeInternalTypes)
            .As<IMinimalApi>()
            .WithSingletonLifetime());

        return services;
    }

    public static IEndpointRouteBuilder MapMinimalApis(
        this IEndpointRouteBuilder app)
    {
        var apis = app.ServiceProvider
            .GetRequiredService<IEnumerable<IMinimalApi>>();

        foreach (var api in apis)
        {
            api.Map(app);
        }

        return app;
    }
}
