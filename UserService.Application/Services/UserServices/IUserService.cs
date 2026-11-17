using AnimalManagement.Shared.Dtos;
using UserService.Domain.Contracts.UserRequests;
using UserService.Domain.Contracts.UserResponses;

namespace UserService.Application.Services.UserServices;

public interface IUserService
{
    Task<ErrorOr<bool>> CreateUser(CreateUserRequest request);
    Task<ErrorOr<LoginResponse>> Login(LoginRequest request);
    Task<LoginResponse> RotateRefreshToken(RotateRefreshTokenRequest request);
}