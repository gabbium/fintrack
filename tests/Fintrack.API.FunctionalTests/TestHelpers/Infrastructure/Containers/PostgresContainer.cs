using Fintrack.Identity.Infrastructure.Data;
using Fintrack.Ledger.Infrastructure.Data;

namespace Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Containers;

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

        var identityOptions = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        var ledgerOptions = new DbContextOptionsBuilder<LedgerDbContext>()
            .UseSnakeCaseNamingConvention()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        using var identityContext = new IdentityDbContext(identityOptions);
        await identityContext.Database.MigrateAsync();

        using var ledgerContext = new LedgerDbContext(ledgerOptions);
        await ledgerContext.Database.MigrateAsync();

        await InitRespawnerAsync();
    }

    private async Task InitRespawnerAsync()
    {
        await using var conn = new NpgsqlConnection(ConnectionString);

        await conn.OpenAsync();

        _respawner = await Respawner.CreateAsync(conn, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["identity", "ledger"],
            TablesToIgnore = [
                new Table("identity", "__EFMigrationsHistory"),
                new Table("ledger", "__EFMigrationsHistory")]
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

