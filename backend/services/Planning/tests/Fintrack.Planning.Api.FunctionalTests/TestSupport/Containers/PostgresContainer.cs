using BuildingBlocks.Application.Identity;
using Fintrack.Planning.Infrastructure;

namespace Fintrack.Planning.Api.FunctionalTests.TestSupport.Containers;

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

        var dbOptions = new DbContextOptionsBuilder<PlanningDbContext>()
            .UseNpgsql(_container.GetConnectionString())
            .Options;

        var identityService = new EmptyIdentityService();
        using var dbContext = new PlanningDbContext(dbOptions, identityService);
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
            SchemasToInclude = ["planning"],
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
}

