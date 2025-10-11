namespace BuildingBlocks.Api.FunctionalTests.AutoAuthorize;

public sealed class AutoAuthorizeAccessor : IAutoAuthorizeAccessor
{
    public ClaimsPrincipal? Current { get; private set; }

    public void Impersonate(ClaimsPrincipal? user)
    {
        Current = user;
    }
}

