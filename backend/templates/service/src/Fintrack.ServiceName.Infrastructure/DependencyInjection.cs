namespace Fintrack.ServiceName.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DbContextName>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseName"));
        });

        builder.EnrichNpgsqlDbContext<DbContextName>();

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<DbContextName>());

        return builder;
    }
}
