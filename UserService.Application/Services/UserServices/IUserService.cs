using UserService.Domain.Contracts.UserRequests;
using UserService.Domain.Contracts.UserResponses;

namespace UserService.Application.Services.UserServices;

public interface IUserService
{
    Task CreateUserAsync(CreateUserRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<LoginResponse> RotateRefreshTokenAsync(RotateRefreshTokenRequest request);
    Task<GetAllUsersResult> GetAllUsersAsync(string username);
}