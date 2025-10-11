using BuildingBlocks.Api;

namespace Fintrack.Planning.Api;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        builder.AddApíDefaults(Assembly.GetExecutingAssembly());

        return builder;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.MapApiDefaultEndpoints();

        return app;
    }
}
