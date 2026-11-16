using Microsoft.Extensions.DependencyInjection;
using TranslationService.Application.Services.TranslationServices;

namespace TranslationService.Application.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITranslationService, Services.TranslationServices.TranslationService>();
        
        return services;
    } 
}