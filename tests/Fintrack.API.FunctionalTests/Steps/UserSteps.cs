using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Identity.Application.Commands.PasswordlessLogin;
using Fintrack.Identity.Application.Models;
using Fintrack.Identity.Application.UnitTests.TestHelpers.Builders;

namespace Fintrack.API.FunctionalTests.Steps;

public class UserSteps(TestFixture fx)
{
    public readonly record struct Credentials(string Email);

    public PasswordlessLoginCommand Given_ValidPasswordlessLoginCommand()
    {
        return new PasswordlessLoginCommandBuilder().Build();
    }

    public PasswordlessLoginCommand Given_InvalidPasswordlessLoginCommand_InvalidEmail()
    {
        return new PasswordlessLoginCommandBuilder()
                .WithEmail("invalid-email")
                .Build();
    }

    public PasswordlessLoginCommand Given_PasswordlessLoginCommand_WithEmail(string email)
    {
        return new PasswordlessLoginCommandBuilder()
            .WithEmail(email)
            .Build();
    }

    public async Task<Credentials> Given_ExistentUserWithCredentials()
    {
        var command = new PasswordlessLoginCommandBuilder().Build();

        var response = await fx.Client.PostAsJsonAsync("/api/v1/users/passwordless-login", command);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<AuthDto>(TestConstants.Json);
        body.ShouldNotBeNull();

        return new Credentials(command.Email);
    }

    public async Task<HttpResponseMessage> When_AttemptToPasswordlessLogin(PasswordlessLoginCommand command)
    {
        return await fx.Client.PostAsJsonAsync("/api/v1/users/passwordless-login", command);
    }
}
