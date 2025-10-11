namespace BuildingBlocks.Api.FunctionalTests.AutoAuthorize;

public class AutoAuthorizeMiddleware(RequestDelegate reqDelegate)
{
    public async Task Invoke(HttpContext httpContext, IAutoAuthorizeAccessor accessor)
    {
        if (accessor.Current is { } user)
        {
            httpContext.User = user;
        }

        await reqDelegate.Invoke(httpContext);
    }
}

