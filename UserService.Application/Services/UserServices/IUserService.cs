using AnimalManagement.Shared.Dtos;
using UserService.Domain.Contracts.UserRequests;
using UserService.Domain.Contracts.UserResponses;

namespace UserService.Application.Services.UserServices;

public interface IUserService
{
    Task CreateUser(CreateUserRequest request);
    Task<LoginResponse> Login(LoginRequest request);
    Task<LoginResponse> RotateRefreshToken(RotateRefreshTokenRequest request);
}