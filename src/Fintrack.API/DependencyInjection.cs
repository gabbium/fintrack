using Fintrack.API.Infrastructure;
using Fintrack.API.Services;
using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.API;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddWebServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddScoped<IUser, CurrentUser>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddProblemDetails();

        builder.Services.AddScoped<IMediator, Mediator>();

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationBehavior.CommandHandler<>));
        builder.Services.TryDecorate(typeof(ICommandHandler<,>), typeof(ValidationBehavior.CommandHandler<,>));
        builder.Services.TryDecorate(typeof(IQueryHandler<,>), typeof(ValidationBehavior.QueryHandler<,>));

        builder.Services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingBehavior.CommandHandler<>));
        builder.Services.TryDecorate(typeof(ICommandHandler<,>), typeof(LoggingBehavior.CommandHandler<,>));
        builder.Services.TryDecorate(typeof(IQueryHandler<,>), typeof(LoggingBehavior.QueryHandler<,>));

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
