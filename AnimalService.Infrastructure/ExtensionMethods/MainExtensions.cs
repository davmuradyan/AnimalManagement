using AnimalService.Application.Interfaces;
using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Application.Interfaces.Repositories;
using AnimalService.Domain.Configs;
using AnimalService.Infrastructure.DAL.Database;
using AnimalService.Infrastructure.DAL.Repositories;
using AnimalService.Infrastructure.ExternalServices;
using AnimalService.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalService.Infrastructure.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Configuration configuration)
    {
        services.AddDbContext<MainDbContext>(options =>
            options.UseSqlServer(
                configuration.ConnectionStrings.MainConnectionString,
                sql => sql.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null)));
        
        services.AddScoped<IAnimalRepository, AnimalRepository>();

        services.AddScoped<ICurrentUser, CurrentUser>();
        
        services.AddHttpClient<ITranslationService, TranslationServiceClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.Services.TranslationService.BaseUrl);
        });
        
        return services;
    }
}