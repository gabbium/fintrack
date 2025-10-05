namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

[Collection(TestCollection.Name)]
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
