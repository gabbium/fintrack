namespace Fintrack.Users.API.FunctionalTests.TestHelpers.Infrastructure;

public class TestFixture : IAsyncLifetime
{
    private static readonly TimeSpan s_buildStopTimeout = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan s_startStopTimeout = TimeSpan.FromSeconds(120);

    public DistributedApplication App { get; private set; } = null!;
    public HttpClient Client { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Fintrack_Ledger_API>(TestContext.Current.CancellationToken);

        App = await builder.BuildAsync(TestContext.Current.CancellationToken)
            .WaitAsync(s_buildStopTimeout, TestContext.Current.CancellationToken);

        await App.StartAsync(TestContext.Current.CancellationToken)
            .WaitAsync(s_startStopTimeout, TestContext.Current.CancellationToken);

        Client = App.CreateHttpClient("users-api");
    }

    public Task ResetStateAsync()
    {
        Client = App.CreateHttpClient("users-api");
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        Client.Dispose();
        await App.DisposeAsync();
    }
}
