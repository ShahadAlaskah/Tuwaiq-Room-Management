using System.Security.Claims;
using Shared.Exceptions;
using Shared.Helpers;

namespace Shared.Extensions;

public static class UserExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        if (user == null) throw new FormsNotFoundException("User");
        var s = user.Claims.FirstOrDefault(s => s.Type == "sub");
        if (s != null)
        {
            Guid.TryParse((string?)s.Value, out var result);
            return result;
        }

        return Guid.Empty;
    }

    public static string GetClientId(this ClaimsPrincipal user)
    {
        if (user == null) throw new FormsNotFoundException("Client");
        var s = user.Claims.FirstOrDefault(s => s.Type == "client_id");
        if (s != null)
        {
            return s.Value;
        }

        return string.Empty;
    }

    public static string GetUsername(this ClaimsPrincipal user)
    {
        return user.Claims.FirstOrDefault(s => s.Type == "name")?.Value ?? "";
    }

    public static bool IsMobileVerified(this ClaimsPrincipal user)
    {
        var result = false;
        var claim = user.FindFirst(MyConstants.ClaimMobileConfirmed);
        if (claim != null) bool.TryParse(claim.Value, out result);
        return result;
    }

    public static bool IsEmailVerified(this ClaimsPrincipal user)
    {
        var result = false;
        var claim = user.FindFirst(MyConstants.ClaimEmailConfirmed);
        if (claim != null) bool.TryParse(claim.Value, out result);
        return result;
    }

    public static string GetName(this ClaimsPrincipal user)
    {
        return user.FindFirst(ClaimTypes.Surname)?.Value ?? "";
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.IsInRole(MyConstants.RoleAdmin);
    }
}