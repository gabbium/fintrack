using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Containers;
using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Hosting;

namespace Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

public class TestFixture : IAsyncLifetime
{
    public PostgresContainer Database { get; private set; } = null!;
    public CustomWebApplicationFactory Factory { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Database = new PostgresContainer();
        await Database.StartAsync();

        Factory = new CustomWebApplicationFactory(Database.ConnectionString);
        Client = Factory.CreateClient();
    }

    public async Task ResetStateAsync()
    {
        await Database.ResetAsync();
        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        await Factory.DisposeAsync();
        await Database.DisposeAsync();
    }
}
