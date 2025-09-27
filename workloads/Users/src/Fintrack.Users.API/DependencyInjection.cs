using Fintrack.Users.API.Infrastructure;

namespace Fintrack.Users.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddProblemDetails();

        return builder;
    }

    public static WebApplication UseWebApp(this WebApplication app)
    {
        app.UseExceptionHandler();

        app.UseSerilogRequestLogging(options =>
        {
            options.IncludeQueryInRequestPath = true;
        });

        var versionSet = app.NewApiVersionSet()
            .ReportApiVersions()
            .Build();

        app.MapGroup("/api/v{version:apiVersion}")
            .WithApiVersionSet(versionSet)
            .MapApis(Assembly.GetExecutingAssembly());

        return app;
    }
}
