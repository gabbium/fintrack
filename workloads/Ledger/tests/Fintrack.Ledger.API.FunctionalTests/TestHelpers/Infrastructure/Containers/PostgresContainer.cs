using Fintrack.Ledger.Application.Interfaces;
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

    public async ValueTask InitializeAsync()
    {
        await _container.StartAsync();

        var dbOptions = new DbContextOptionsBuilder<LedgerDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        var identityService = new IdentityService();
        using var dbContext = new LedgerDbContext(dbOptions, identityService);
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

    public async ValueTask DisposeAsync()
    {
        await _container.DisposeAsync();
    }

    private class IdentityService : IIdentityService
    {
        public Guid GetUserIdentity()
        {
            return Guid.Empty;
        }
    }
}

