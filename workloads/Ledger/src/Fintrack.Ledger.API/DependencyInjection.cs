using Fintrack.Ledger.API.Infrastructure;
using Fintrack.Ledger.API.Services;
using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.Ledger.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddProblemDetails();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<IIdentityService, IdentityService>();

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
