using AnimalService.Application.Mappers;
using AnimalService.Application.Services.AnimalServices;
using AnimalService.Application.Services.AnimalServices.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalService.Application.ExtensionMethods;

public static class MainExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAnimalService, Services.AnimalServices.AnimalService>();
        
        services.AddValidatorsFromAssemblyContaining<CreateAnimalRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<GetAllAnimalsRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateAnimalRequestValidator>();
        
        services.AddAutoMapper(_ => { }, typeof(AnimalMappingProfile).Assembly);
        
        return services;
    } 
}