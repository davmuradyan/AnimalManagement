using Microsoft.AspNetCore.Mvc;
using UserService.Api.ControllerExtensions;
using UserService.Application.Services.UserServices;
using UserService.Domain.Contracts.UserRequests;

namespace UserService.Api.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var result = await service.CreateUser(request);
        return this.ErrorOrConverter(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await service.Login(request);
        return this.ErrorOrConverter(result);
    }
}