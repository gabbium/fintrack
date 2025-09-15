namespace Fintrack.Ledger.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<LedgerContext>(options =>
        {
            options.UseSnakeCaseNamingConvention();
            options.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
        });

        builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<LedgerContext>());

        builder.Services
            .AddHealthChecks()
            .AddDbContextCheck<LedgerContext>(name: "ledgerdb", tags: ["ready"]);

        return builder;
    }
}
