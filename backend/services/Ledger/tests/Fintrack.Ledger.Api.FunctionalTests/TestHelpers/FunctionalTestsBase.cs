namespace Fintrack.Ledger.Api.FunctionalTests.TestHelpers;

[Collection(FunctionalTestsCollection.Name)]
public abstract class FunctionalTestsBase(FunctionalTestsFixture fx) : IAsyncLifetime
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
