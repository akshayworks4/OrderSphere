using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OrderSphere.Application.Interfaces.Security;

namespace OrderSphere.Infrastructure.Security;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId => Guid.Parse(_httpContextAccessor.HttpContext?
            .User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    public string? Email => _httpContextAccessor.HttpContext?
            .User.FindFirst(ClaimTypes.Email)?.Value;

    public string? Role => _httpContextAccessor.HttpContext?
            .User.FindFirst(ClaimTypes.Role)?.Value;
}