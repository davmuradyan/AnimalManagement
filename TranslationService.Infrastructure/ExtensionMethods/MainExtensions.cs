using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TranslationService.Application.Interfaces.Repositories;
using TranslationService.Domain.Configs;
using TranslationService.Infrastructure.Database;
using TranslationService.Infrastructure.Repositories;

namespace TranslationService.Infrastructure.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Configuration configuration)
    {
        services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(configuration.ConnectionStrings.MainConnectionString));
        
        services.AddScoped<ITranslationRepository, TranslationRepository>();
        
        return services;
    }
}