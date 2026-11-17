using AnimalManagement.Shared.Dtos;
using FluentValidation;
using Microsoft.Extensions.Logging;
using UserService.Application.Interfaces.HashFunctions;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Tokens;
using UserService.Domain.Contracts.UserRequests;
using UserService.Domain.Contracts.UserResponses;
using UserService.Domain.Entities;

namespace UserService.Application.Services.UserServices;

public class UserService(IUserRepository userRepository,
    IRefreshTokenRepository refreshTokenRepository,
    IPasswordHashService passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
    IRefreshTokenGenerator refreshTokenGenerator,
    IValidator<CreateUserRequest> createUserRequestValidator,
    IValidator<LoginRequest> loginRequestValidator,
    ILogger<UserService> logger) : IUserService
{
    public async Task<ErrorOr<bool>> CreateUser(CreateUserRequest request)
    {
        // Validation
        var validation = await createUserRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            return ErrorOr<bool>.Failure(validation.Errors[0].ErrorMessage, ErrorType.Validation);
        }

        // Main implementation
        var user = User.Create(
            request.Name,
            request.Surname,
            request.Username,
            passwordHasher.Hash(request.Password));
        
        await userRepository.Create(user);
        logger.LogInformation("User created successfully. Username: {username}", user.Username);

        // Response
        return ErrorOr<bool>.Success(true);
    }

    public async Task<ErrorOr<LoginResponse>> Login(LoginRequest request)
    {
        // Validation
        var validation = await loginRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            return ErrorOr<LoginResponse>.Failure(validation.Errors[0].ErrorMessage, ErrorType.Validation);
        }

        // Main implementation
        var user = await userRepository.GetByUserName(request.Username);

        if (user is null)
        {
            logger.LogInformation("Failed login attempt. User with \"{username}\" not found.", request.Username);
            return ErrorOr<LoginResponse>.Failure($"Wrong username or password.", ErrorType.Authentication);
        }

        var result = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!result)
        {
            logger.LogInformation("Failed login attempt with password. Username is \"{username}\".", request.Username);
            return ErrorOr<LoginResponse>.Failure($"Wrong username or password.", ErrorType.Authentication);
        }

        var accessToken = jwtTokenGenerator.GenerateToken(user);
        var refreshToken = refreshTokenGenerator.Generate();
        var refreshTokenHash = refreshTokenGenerator.Hash(refreshToken);
        await refreshTokenRepository.Create(RefreshToken.Create(refreshTokenHash, user.Id));

        // Response
        var response = new LoginResponse(accessToken, refreshToken);
        return ErrorOr<LoginResponse>.Success(response);
    }

    public async Task<LoginResponse> RotateRefreshToken(RotateRefreshTokenRequest request)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(request.OldRefreshToken))
            throw new NotImplementedException();
        var oldHashTokenHash = refreshTokenGenerator.Hash(request.OldRefreshToken);
        var oldRefreshTokenEntity = await refreshTokenRepository.Get(oldHashTokenHash);
        
        if (oldRefreshTokenEntity is null)
            throw new NotImplementedException();

        if (oldRefreshTokenEntity.RevokedAt is not null || oldRefreshTokenEntity.ExpiresAt < DateTime.UtcNow)
            throw new NotImplementedException();
        
        if (oldRefreshTokenEntity.User is null)
            throw new NotImplementedException();
        
        // Main implementation
        var newRefreshToken = refreshTokenGenerator.Generate();
        var newRefreshTokenHash = refreshTokenGenerator.Hash(newRefreshToken);
        var newRefreshTokenEntity = RefreshToken.Create(newRefreshTokenHash, oldRefreshTokenEntity.UserId);

        var accessToken = jwtTokenGenerator.GenerateToken(oldRefreshTokenEntity.User);
        var result = await refreshTokenRepository.TryRotate(oldRefreshTokenEntity, newRefreshTokenEntity);
        if (!result)
            throw new NotImplementedException();
        
        return new LoginResponse(accessToken, newRefreshToken);
    }
}
