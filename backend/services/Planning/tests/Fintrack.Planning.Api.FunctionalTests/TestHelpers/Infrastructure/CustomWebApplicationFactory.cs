using Fintrack.Planning.Api.FunctionalTests.TestHelpers.Infrastructure.Authorize;
using Fintrack.Planning.Api;

namespace Fintrack.Planning.Api.FunctionalTests.TestHelpers.Infrastructure;

public class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Authentication:OidcJwt:Authority", "http://localhost");
        builder.UseSetting("ConnectionStrings:PlanningDb", connectionString);

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
