namespace Fintrack.Ledger.API.FunctionalTests.TestHelpers.Infrastructure.Authorize;

public class AutoAuthorizeMiddleware(RequestDelegate reqDelegate)
{
    public async Task Invoke(HttpContext httpContext, IAutoAuthorizeAccessor accessor)
    {
        if (accessor.User is not null)
        {
            httpContext.User = accessor.User;
        }

        await reqDelegate.Invoke(httpContext);
    }
}
