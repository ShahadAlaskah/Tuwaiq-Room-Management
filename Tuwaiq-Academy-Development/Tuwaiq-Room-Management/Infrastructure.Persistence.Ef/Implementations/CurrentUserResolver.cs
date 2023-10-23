using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Application.Interfaces;

namespace Infrastructure.Persistence.Ef.Implementations;

public class CurrentUserResolver : ICurrentUserResolver
{
    public CurrentUserResolver(IHttpContextAccessor httpContextAccessor)
    {
        IsAuthenticated = false;

        if (httpContextAccessor.HttpContext != null && httpContextAccessor.HttpContext.User.Identity != null &&
            httpContextAccessor.HttpContext.User.Identity != null &&
            httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
        {
            CurrentUser = httpContextAccessor.HttpContext.User;
            IsAuthenticated = httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }

    public ClaimsPrincipal? CurrentUser { get; }
    public bool IsAuthenticated { get; }
}