namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure;

[Collection(TestsCollection.Name)]
public abstract class TestBase : IAsyncLifetime
{
    public ValueTask InitializeAsync()
    {
        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
