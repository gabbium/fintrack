using Fintrack.API.FunctionalTests.Steps;
using Fintrack.API.FunctionalTests.TestHelpers.Assertions;
using Fintrack.API.FunctionalTests.TestHelpers.Infrastructure.Support;
using Fintrack.Identity.Application.Models;

namespace Fintrack.API.FunctionalTests.Scenarios.Users;

public class PasswordlessLoginTests(TestFixture fx) : TestBase(fx)
{
    private readonly AuthSteps _auth = new(fx);
    private readonly UserSteps _user = new(fx);

    [Fact]
    public async Task GivenAnonymousUser_WhenLoggingInWithExistentUser_Then200WithBody()
    {
        await _auth.Given_AnonymousUser();
        var credentials = await _user.Given_ExistentUserWithCredentials();
        var command = _user.Given_PasswordlessLoginCommand_WithEmail(credentials.Email);
        var response = await _user.When_AttemptToPasswordlessLogin(command);
        await response.ShouldBeOkWithBody<AuthDto>();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenLoggingInWithNonExistentUser_Then200WithBody()
    {
        await _auth.Given_AnonymousUser();
        var command = _user.Given_ValidPasswordlessLoginCommand();
        var response = await _user.When_AttemptToPasswordlessLogin(command);
        await response.ShouldBeOkWithBody<AuthDto>();
    }

    [Fact]
    public async Task GivenAnonymousUser_WhenLoggingInWithInvalidData_Then400WithValidation()
    {
        await _auth.Given_AnonymousUser();
        var command = _user.Given_InvalidPasswordlessLoginCommand_InvalidEmail();
        var response = await _user.When_AttemptToPasswordlessLogin(command);
        await response.ShouldBeBadRequestWithValidation();
    }
}
