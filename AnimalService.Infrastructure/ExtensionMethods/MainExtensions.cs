using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Application.Interfaces.Repositories;
using AnimalService.Infrastructure.DAL.Data;
using AnimalService.Infrastructure.DAL.Database;
using AnimalService.Infrastructure.DAL.Repositories;
using AnimalService.Infrastructure.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalService.Infrastructure.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("MainConnectionString")));

        services.AddScoped<AnimalDataInitializer>();
        
        services.AddScoped<IAnimalRepository, AnimalRepository>();

        services.AddHttpClient<ITranslationService, TranslationServiceClient>(client =>
        {
            client.BaseAddress = new Uri(
                configuration["Services:TranslationService:BaseUrl"]!);
        });
        
        return services;
    }
}