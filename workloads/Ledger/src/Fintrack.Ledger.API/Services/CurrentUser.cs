using Fintrack.Ledger.Application.Interfaces;

namespace Fintrack.Ledger.API.Services;

internal sealed class CurrentUser(IHttpContextAccessor accessor) : IUser
{
    public Guid UserId => accessor.HttpContext?.User.GetUserId() ?? Guid.Empty;
}

internal static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId)
            ? parsedUserId
            : null;
    }
}

