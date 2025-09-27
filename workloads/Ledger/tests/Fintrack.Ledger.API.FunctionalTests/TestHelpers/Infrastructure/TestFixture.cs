namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure;

public class TestFixture : IAsyncLifetime
{
    public DistributedApplication App { get; private set; } = null!;
    public HttpClient LedgerApiClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Fintrack_Ledger_API>();

        App = await builder.BuildAsync();

        await App.StartAsync(TestContext.Current.CancellationToken);
        await App.ResourceNotifications.WaitForResourceHealthyAsync("ledger-api", TestContext.Current.CancellationToken);

        LedgerApiClient = App.CreateHttpClient("ledger-api");
    }

    public Task ResetStateAsync()
    {
        LedgerApiClient = App.CreateHttpClient("ledger-api");
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        LedgerApiClient.Dispose();
        await App.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
