namespace Fintrack.Planning.Api.FunctionalTests.TestSupport;

[Collection(TestsCollection.Name)]
public abstract class TestsBase(TestsFixture fx) : IAsyncLifetime
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
