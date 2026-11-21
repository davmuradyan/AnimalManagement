using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Services.UserServices;
using UserService.Domain.Contracts.UserRequests;

namespace UserService.Api.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost("register")]
    [ApiVersion("1.0")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        await service.CreateUserAsync(request);
        return Ok();
    }
    
    [HttpPost("login")]
    [ApiVersion("1.0")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await service.LoginAsync(request);
        return Ok(result);
    }

    [HttpPost("rotate")]
    [ApiVersion("1.0")]
    public async Task<IActionResult> Rotate([FromBody] RotateRefreshTokenRequest request)
    {
        var result = await service.RotateRefreshTokenAsync(request);
        return Ok(result);
    }
    
    [HttpGet]
    [ApiVersion("1.0")]
    public async Task<IActionResult> GetAllUsers()
    {
        var username = User.FindFirst("unique_name")?.Value;

        if (username is null)
            return Unauthorized();

        var result = await service.GetAllUsersAsync(username);

        return Ok(result);
    }
}