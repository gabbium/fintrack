namespace Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;

[Collection(FunctionalTestsCollection.Name)]
public abstract class TestBase(TestFixture fx) : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await fx.ResetStateAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}
