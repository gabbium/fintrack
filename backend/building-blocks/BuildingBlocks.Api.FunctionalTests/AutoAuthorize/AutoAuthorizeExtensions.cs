namespace BuildingBlocks.Api.FunctionalTests.AutoAuthorize;

public static class AutoAuthorizeExtensions
{
    public static IServiceCollection AddAutoAuthorize(this IServiceCollection services)
    {
        services.AddSingleton<IAutoAuthorizeAccessor, AutoAuthorizeAccessor>();
        services.AddSingleton<IStartupFilter, AutoAuthorizeStartupFilter>();

        return services;
    }

    private sealed class AutoAuthorizeStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseMiddleware<AutoAuthorizeMiddleware>();
                next(app);
            };
        }
    }
}

