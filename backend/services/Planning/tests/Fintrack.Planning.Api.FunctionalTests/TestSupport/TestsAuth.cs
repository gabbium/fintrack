namespace Fintrack.Planning.Api.FunctionalTests.TestSupport;

public static class TestsAuth
{
    public const string Scheme = "Test";

    public static IServiceCollection AddTestAuth(this IServiceCollection services)
        => services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = Scheme;
                o.DefaultChallengeScheme = Scheme;
            })
            .AddScheme<AuthenticationSchemeOptions, TestsAuthHandler>(Scheme, _ => { })
            .Services;
}

public sealed class TestsAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    private const string HeaderUserId = "X-Test-UserId";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey(HeaderUserId))
            return Task.FromResult(AuthenticateResult.NoResult());

        var claims = new List<Claim>();

        if (Request.Headers.TryGetValue(HeaderUserId, out var sub) && !string.IsNullOrWhiteSpace(sub))
            claims.Add(new Claim(ClaimTypes.NameIdentifier, sub!));

        var identity = new ClaimsIdentity(claims, TestsAuth.Scheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(), TestsAuth.Scheme);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
