using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Services.UserServices;
using UserService.Application.Services.UserServices.Validators;

namespace UserService.Application.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, Services.UserServices.UserService>();
        
        services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        
        return services;
    } 
}