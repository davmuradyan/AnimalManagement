using System.Security.Authentication;
using AnimalService.Domain.Exceptions.Application;
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
    public async Task CreateUser(CreateUserRequest request)
    {
        // Validation
        var validation = await createUserRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            throw new RequestValidationException(validation.Errors[0].ErrorMessage);
        }

        // Main implementation
        var user = User.Create(
            request.Name,
            request.Surname,
            request.Username,
            passwordHasher.Hash(request.Password));
        
        await userRepository.Create(user);
        logger.LogInformation("User created successfully. Username: {username}", user.Username);
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        // Validation
        var validation = await loginRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            throw new RequestValidationException(validation.Errors[0].ErrorMessage);
        }

        // Main implementation
        var user = await userRepository.GetByUserName(request.Username);

        if (user is null)
        {
            logger.LogInformation("Failed login attempt. User with \"{username}\" not found.", request.Username);
            throw new AuthenticationException("Wrong username or password.");
        }

        var result = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (!result)
        {
            logger.LogInformation("Failed login attempt with password. Username is \"{username}\".", request.Username);
            throw new AuthenticationException("Wrong username or password.");
        }

        var accessToken = jwtTokenGenerator.GenerateToken(user);
        var refreshToken = refreshTokenGenerator.Generate();
        var refreshTokenHash = refreshTokenGenerator.Hash(refreshToken);
        await refreshTokenRepository.Create(RefreshToken.Create(refreshTokenHash, user.Id));

        // Response
        var response = new LoginResponse(accessToken, refreshToken);
        return response;
    }

    public async Task<LoginResponse> RotateRefreshToken(RotateRefreshTokenRequest request)
    {
        // Validation
        if (string.IsNullOrWhiteSpace(request.OldRefreshToken))
            throw new RequestValidationException("Old refresh token is required.");
        var oldHashTokenHash = refreshTokenGenerator.Hash(request.OldRefreshToken);
        var oldRefreshTokenEntity = await refreshTokenRepository.Get(oldHashTokenHash);

        if (oldRefreshTokenEntity is null)
            throw new AuthenticationException("Something went wrong. Try again later.");

        if (oldRefreshTokenEntity.RevokedAt is not null || oldRefreshTokenEntity.ExpiresAt < DateTime.UtcNow)
            throw new AuthenticationException("Something went wrong. Try again later.");
        
        if (oldRefreshTokenEntity.User is null)
            throw new AuthenticationException("Something went wrong. Try again later.");
        
        // Main implementation
        var newRefreshToken = refreshTokenGenerator.Generate();
        var newRefreshTokenHash = refreshTokenGenerator.Hash(newRefreshToken);
        var newRefreshTokenEntity = RefreshToken.Create(newRefreshTokenHash, oldRefreshTokenEntity.UserId);

        var accessToken = jwtTokenGenerator.GenerateToken(oldRefreshTokenEntity.User);
        var result = await refreshTokenRepository.TryRotate(oldRefreshTokenEntity, newRefreshTokenEntity);
        if (!result)
            throw new AuthenticationException("The refresh token is no longer valid.");
        
        return new LoginResponse(accessToken, newRefreshToken);
    }
}
