using AnimalService.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AnimalService.Infrastructure.Implementations;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    private HttpContext? Context => accessor.HttpContext;

    public string? UserId =>
        Context?.Request.Headers["X-User-Id"].FirstOrDefault();

    public string? Username =>
        Context?.Request.Headers["X-Username"].FirstOrDefault();
}