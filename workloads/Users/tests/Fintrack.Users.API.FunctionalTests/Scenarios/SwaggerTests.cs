//using Fintrack.Users.API.FunctionalTests.TestHelpers.Assertions;
//using Fintrack.Users.API.FunctionalTests.TestHelpers.Infrastructure;

//namespace Fintrack.Users.API.FunctionalTests.Scenarios;

//public class SwaggerTests(TestFixture fx) : TestBase(fx)
//{
//    [Fact]
//    public async Task GivenApplicationStarted_WhenGettingSwaggerV1_Then200()
//    {
//        var response = await _fx.UsersApiClient.GetAsync("/openapi/v1.json", TestContext.Current.CancellationToken);
//        response.ShouldBeOk();
//    }
//}
