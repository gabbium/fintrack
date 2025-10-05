namespace Fintrack.ServiceName.MigrationService.IntegrationTests.TestHelpers.Containers;

public class PostgresContainer : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;

    public PostgresContainer()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .Build();
    }

    public string ConnectionString => _container.GetConnectionString();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}
