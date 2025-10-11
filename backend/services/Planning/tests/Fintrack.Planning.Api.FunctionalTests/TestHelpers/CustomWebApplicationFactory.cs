using BuildingBlocks.Api.FunctionalTests.AutoAuthorize;

namespace Fintrack.Planning.Api.FunctionalTests.TestHelpers;

public class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Authentication:OidcJwt:Authority", "http://localhost");
        builder.UseSetting("ConnectionStrings:PlanningDb", connectionString);

        builder.ConfigureServices(services =>
        {
            services.AddAutoAuthorize();
        });
    }
}
