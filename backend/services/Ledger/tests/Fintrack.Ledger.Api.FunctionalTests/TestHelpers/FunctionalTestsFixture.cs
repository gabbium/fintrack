using BuildingBlocks.Api.FunctionalTests.AutoAuthorize;
using Fintrack.Ledger.Api.FunctionalTests.TestHelpers.Containers;

namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

public class FunctionalTestsFixture : IAsyncLifetime
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
        using var scope = Factory.Services.CreateScope();
        var autoAuthorizeAccessor = scope.ServiceProvider.GetRequiredService<IAutoAuthorizeAccessor>();
        autoAuthorizeAccessor.Impersonate(null);

        await Database.ResetAsync();
    }

    public async Task DisposeAsync()
    {
        await Factory.DisposeAsync();
        await Database.DisposeAsync();
    }
}
