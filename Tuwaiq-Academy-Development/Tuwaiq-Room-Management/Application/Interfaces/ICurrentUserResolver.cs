using System.Security.Claims;

namespace Application.Interfaces;

public interface ICurrentUserResolver
{
    ClaimsPrincipal? CurrentUser { get; }
    bool IsAuthenticated { get; }
}