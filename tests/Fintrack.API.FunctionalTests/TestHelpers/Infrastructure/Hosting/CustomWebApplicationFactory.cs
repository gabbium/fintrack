namespace Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Hosting;

public class CustomWebApplicationFactory(string connectionString) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:Postgres", connectionString);
        builder.UseSetting("Jwt:Secret", Guid.NewGuid().ToString());
    }
}

