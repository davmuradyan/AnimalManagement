using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Interfaces.HashFunctions;
using UserService.Application.Interfaces.Repositories;
using UserService.Application.Interfaces.Tokens;
using UserService.Domain.Configurations;
using UserService.Infrastructure.DAL.Repositories;
using UserService.Infrastructure.Database;
using UserService.Infrastructure.Implementations.HashFunctions;
using UserService.Infrastructure.Implementations.Tokens;

namespace UserService.Infrastructure.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("MainConnectionString")));
        
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IPasswordHashService, BCryptHashService>();
        
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        
        return services;
    }
}