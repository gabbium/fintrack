namespace Fintrack.Users.API.FunctionalTests.TestHelpers.Infrastructure;

[Collection(TestsCollection.Name)]
public abstract class TestBase(TestFixture fx) : IAsyncLifetime
{
    protected readonly TestFixture _fx = fx;

    public async ValueTask InitializeAsync()
    {
        await _fx.ResetStateAsync();
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }
}
