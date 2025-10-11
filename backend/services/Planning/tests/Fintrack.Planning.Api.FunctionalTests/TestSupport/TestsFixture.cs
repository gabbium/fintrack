using Fintrack.Planning.Api.FunctionalTests.TestSupport.Containers;

namespace Fintrack.Planning.Api.FunctionalTests.TestSupport;

public class TestsFixture : IAsyncLifetime
{
    public PostgresContainer Database { get; private set; } = null!;
    public CustomWebApplicationFactory Factory { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        Database = new PostgresContainer();
        await Database.InitializeAsync();

        Factory = new CustomWebApplicationFactory(Database.ConnectionString);
    }

    public async Task ResetStateAsync()
    {
        await Database.ResetAsync();
    }

    public async Task DisposeAsync()
    {
        await Factory.DisposeAsync();
        await Database.DisposeAsync();
    }
}
