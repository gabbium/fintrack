using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Identity.Application.Interfaces;
using Fintrack.Identity.Domain.UnitTests.TestHelpers.Builders;

namespace Fintrack.API.FunctionalTests.Steps;

public class AuthSteps(TestFixture fx)
{
    public Task Given_LoggedInUser()
    {
        var user = new UserBuilder().Build();

        using var scope = fx.Factory.Services.CreateScope();
        var jwtService = scope.ServiceProvider.GetRequiredService<IJwtService>();
        var accessToken = jwtService.CreateAccessToken(user);

        fx.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        return Task.CompletedTask;
    }

    public Task Given_AnonymousUser()
    {
        fx.Client.DefaultRequestHeaders.Authorization = null;

        return Task.CompletedTask;
    }
}
