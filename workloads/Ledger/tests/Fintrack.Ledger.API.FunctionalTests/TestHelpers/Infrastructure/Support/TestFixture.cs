using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Authorize;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Containers;
using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Hosting;

namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

public class TestFixture : IAsyncLifetime
{
    public PostgresContainer Database { get; private set; } = null!;
    public CustomWebApplicationFactory Factory { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        Database = new PostgresContainer();
        await Database.InitializeAsync();

        Factory = new CustomWebApplicationFactory(Database.ConnectionString);
    }

    public async Task ResetStateAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var autoAuthorizeAccessor = scope.ServiceProvider.GetRequiredService<IAutoAuthorizeAccessor>();
        autoAuthorizeAccessor.User = null;

        await Database.ResetAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Factory.DisposeAsync();
        await Database.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
