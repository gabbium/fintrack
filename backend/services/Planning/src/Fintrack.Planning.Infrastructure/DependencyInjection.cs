namespace Fintrack.Planning.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<PlanningDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("PlanningDb"));
        });

        builder.EnrichNpgsqlDbContext<PlanningDbContext>();

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<PlanningDbContext>());

        return builder;
    }
}
