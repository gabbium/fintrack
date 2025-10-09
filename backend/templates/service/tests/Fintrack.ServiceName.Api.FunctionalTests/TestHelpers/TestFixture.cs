using Fintrack.ServiceName.Api.FunctionalTests.TestHelpers.Infrastructure;
using Fintrack.ServiceName.Api.FunctionalTests.TestHelpers.Infrastructure.Authentication;
using Fintrack.ServiceName.Api.FunctionalTests.TestHelpers.Infrastructure.Containers;

namespace Fintrack.ServiceName.Api.FunctionalTests.TestHelpers;

public class TestFixture : IAsyncLifetime
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
        autoAuthorizeAccessor.User = null;

        await Database.ResetAsync();
    }

    public async Task DisposeAsync()
    {
        await Factory.DisposeAsync();
        await Database.DisposeAsync();
    }
}
