using Microsoft.AspNetCore.Mvc;
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
        await service.CreateUser(request);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await service.Login(request);
        return Ok(result);
    }

    [HttpPost("rotate")]
    public async Task<IActionResult> Rotate([FromBody] RotateRefreshTokenRequest request)
    {
        var result = await service.RotateRefreshToken(request);
        return Ok(result);
    }
}