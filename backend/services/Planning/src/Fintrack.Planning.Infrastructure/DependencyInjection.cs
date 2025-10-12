using Fintrack.Planning.Application.Queries.ListPlannedMovements;
using Fintrack.Planning.Domain.PlannedMovementAggregate;
using Fintrack.Planning.Infrastructure.Interceptors;
using Fintrack.Planning.Infrastructure.Queries;
using Fintrack.Planning.Infrastructure.Repositories;

namespace Fintrack.Planning.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISaveChangesInterceptor, EventDispatchInterceptor>();

        builder.Services.AddDbContext<PlanningDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(builder.Configuration.GetConnectionString("PlanningDb"));
        });

        builder.EnrichNpgsqlDbContext<PlanningDbContext>();

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PlanningDbContext>());

        builder.Services.AddScoped<IPlannedMovementRepository, PlannedMovementRepository>();
        builder.Services.AddScoped<IListPlannedMovementsQueryService, ListPlannedMovementsQueryService>();

        return builder;
    }
}
