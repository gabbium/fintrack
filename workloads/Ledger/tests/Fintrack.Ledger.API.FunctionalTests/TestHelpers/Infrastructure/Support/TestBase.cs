namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Support;

[Collection(TestsCollection.Name)]
public abstract class TestBase(TestFixture fx) : IAsyncLifetime
{
    public async ValueTask InitializeAsync()
    {
        await fx.ResetStateAsync();
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}
