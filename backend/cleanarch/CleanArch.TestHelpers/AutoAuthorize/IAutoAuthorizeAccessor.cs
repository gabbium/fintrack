namespace CleanArch.TestHelpers.AutoAuthorize;

public interface IAutoAuthorizeAccessor
{
    ClaimsPrincipal? Current { get; }

    void Impersonate(ClaimsPrincipal? user);
}

