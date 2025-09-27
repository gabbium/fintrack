using Fintrack.Ledger.API.FunctionalTests.TestHelpers.Assertions;

namespace Fintrack.Ledger.API.FunctionalTests.Scenarios;

public class IntegrationTests
{
    private static readonly TimeSpan s_buildStopTimeout = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan s_startStopTimeout = TimeSpan.FromSeconds(120);

    [Fact]
    public async Task GivenApplicationStarted_WhenGettingSwaggerV1_Then200()
    {
        var builder = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Fintrack_Ledger_API>(TestContext.Current.CancellationToken);

        await using var app = await builder.BuildAsync(TestContext.Current.CancellationToken)
            .WaitAsync(s_buildStopTimeout, TestContext.Current.CancellationToken);

        await app.StartAsync(TestContext.Current.CancellationToken)
            .WaitAsync(s_startStopTimeout, TestContext.Current.CancellationToken);

        await app.ResourceNotifications.WaitForResourceHealthyAsync("ledger-api", TestContext.Current.CancellationToken)
            .WaitAsync(s_startStopTimeout, TestContext.Current.CancellationToken);

        var client = app.CreateHttpClient("ledger-api");

        var response = await client.GetAsync("/openapi/v1.json", TestContext.Current.CancellationToken);
        response.ShouldBeOk();
    }
}
