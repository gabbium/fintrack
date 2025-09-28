namespace Fintrack.Ledger.MigrationService.IntegrationTests.TestHelpers.Infrastructure.Containers;

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

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}

