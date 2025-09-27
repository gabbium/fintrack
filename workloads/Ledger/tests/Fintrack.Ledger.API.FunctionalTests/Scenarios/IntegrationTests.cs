namespace Fintrack.Ledger.API.FunctionalTests.Scenarios;

public class IntegrationTests
{
    [Fact]
    public async Task GivenApplicationStarted_WhenGettingSwaggerV1_Then200()
    {
        // Arrange
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Fintrack_Ledger_API>(TestContext.Current.CancellationToken);

        builder.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        await using var app = await builder.BuildAsync(TestContext.Current.CancellationToken);

        await app.StartAsync(TestContext.Current.CancellationToken);

        // Act
        var httpClient = app.CreateHttpClient("ledger-api");

        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

        //await app.ResourceNotifications.WaitForResourceHealthyAsync(
        //    "ledger-api",
        //    cts.Token);

        var response = await httpClient.GetAsync("/", TestContext.Current.CancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
