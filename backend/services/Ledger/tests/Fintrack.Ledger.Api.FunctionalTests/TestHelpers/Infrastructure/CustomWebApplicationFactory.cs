namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Infrastructure;

public class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("Authentication:OidcJwt:Authority", "http://localhost");
        builder.UseSetting("ConnectionStrings:LedgerDb", connectionString);

        builder.ConfigureServices(services =>
        {
            services.AddAutoAuthorize();
        });
    }
}
