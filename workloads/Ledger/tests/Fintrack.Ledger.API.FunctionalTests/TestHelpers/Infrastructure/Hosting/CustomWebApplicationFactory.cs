using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Authorize;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Hosting;

public class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Identity:Url", "http://localhost");
        builder.UseSetting("ConnectionStrings:LedgerDb", connectionString);

        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IAutoAuthorizeAccessor, AutoAuthorizeAccessor>();
            services.AddSingleton<IStartupFilter>(new AutoAuthorizeStartupFilter());
        });
    }

    private class AutoAuthorizeStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<AutoAuthorizeMiddleware>();
                next(builder);
            };
        }
    }
}
