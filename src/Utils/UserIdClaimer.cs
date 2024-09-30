using System.Security.Claims;

namespace ExpenseTrackerGrupo4.src.Utils;

public static class UserIdClaimer
{
    public static Guid GetCurrentUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim != null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
    }
}
