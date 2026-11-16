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
    IPasswordHashService passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator,
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
        
        // Response
        var token = jwtTokenGenerator.GenerateToken(user);
        var response = new LoginResponse(token, ExpiresAt: DateTime.UtcNow.AddMinutes(15));
        return ErrorOr<LoginResponse>.Success(response);
    }
}