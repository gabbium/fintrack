using Fintrack.Ledger.Infrastructure;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Containers;

public class PostgresContainer
{
    private readonly PostgreSqlContainer _container;
    private Respawner? _respawner;

    public PostgresContainer()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .Build();
    }

    public string ConnectionString => _container.GetConnectionString();

    public async Task StartAsync()
    {
        await _container.StartAsync();

        var dbOptions = new DbContextOptionsBuilder<LedgerDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        using var dbContext = new LedgerDbContext(dbOptions);
        await dbContext.Database.MigrateAsync();

        await InitRespawnerAsync();
    }

    private async Task InitRespawnerAsync()
    {
        await using var conn = new NpgsqlConnection(ConnectionString);

        await conn.OpenAsync();

        _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["ledger"],
            WithReseed = true
        });
    }

    public async Task ResetAsync()
    {
        if (_respawner is null)
        {
            await InitRespawnerAsync();
        }

        await using var conn = new NpgsqlConnection(ConnectionString);
        await conn.OpenAsync();
        await _respawner!.ResetAsync(conn);
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
    }
}

