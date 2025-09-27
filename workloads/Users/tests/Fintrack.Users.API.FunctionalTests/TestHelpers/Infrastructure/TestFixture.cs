namespace Fintrack.Users.API.FunctionalTests.TestHelpers.Infrastructure;

public class TestFixture : IAsyncLifetime
{
    public DistributedApplication App { get; private set; } = null!;
    public HttpClient UsersApiClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Fintrack_Users_API>();

        App = await builder.BuildAsync();

        await App.StartAsync(TestContext.Current.CancellationToken);
        await App.ResourceNotifications.WaitForResourceHealthyAsync("users-api", TestContext.Current.CancellationToken);

        UsersApiClient = App.CreateHttpClient("users-api");
    }
    public Task ResetStateAsync()
    {
        return Task.CompletedTask;
    }

    public async ValueTask DisposeAsync()
    {
        await App.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
