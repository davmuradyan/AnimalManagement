using AnimalManagement.Shared.Enums;
using AnimalManagement.Shared.Exceptions.Application;
using AnimalService.Application.Interfaces;
using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Application.Interfaces.Repositories;
using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using AnimalService.Domain.Entities;
using AnimalService.Domain.HelperClasses;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalService.Application.Services.AnimalServices;

public sealed class AnimalService(
    IValidator<CreateAnimalRequest> createAnimalRequestValidator,
    IValidator<GetAllAnimalsRequest> getAllAnimalsRequestValidator,
    IValidator<UpdateAnimalRequest> updateAnimalRequestValidator,
    IMapper mapper,
    IAnimalRepository animalRepository,
    ITranslationService translationService,
    ICurrentUser currentUser,
    ILogger<AnimalService> logger) : IAnimalService
{
    public async Task<AnimalDto> Create(CreateAnimalRequest request)
    {
        // Validation
        var validation = await createAnimalRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            throw new RequestValidationException(validation.Errors[0].ErrorMessage);
        }
        
        // Main implementation
        var username = currentUser.Username ?? throw new CustomAuthenticationException("Current user is not available.");
        var animal = mapper.Map<Animal>(request);
        animal.CreatedByUsername = username;
        animal.UpdatedByUsername = username;

        await animalRepository.Create(animal);

        // Response
        return mapper.Map<AnimalDto>(animal);
    }

    public async Task<List<AnimalDto>> GetAll(GetAllAnimalsRequest request)
    {
        // Validation
        var validation = await getAllAnimalsRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            throw new RequestValidationException(validation.Errors[0].ErrorMessage);
        }

        var args = new FilterArgs()
        {
            Count = request.Count,
            MinAge = request.MinAge,
            MaxAge = request.MaxAge,
            MinWeight = request.MinWeight,
            MaxWeight = request.MaxWeight,
            Colors = request.Colors
                .Select(c => Enum.Parse<Color>(c, true))
                .ToArray(),
            Gender = request.Gender == null ? null : Enum.Parse<Gender>(request.Gender, true),
            IncludeClosed = request.IncludeClosed
        };

        // Main implementation
        var animals = await animalRepository.GetAll(
            pageNumber: request.Page,
            pageSize: request.PageSize,
            args: args);
        
        var result = animals.Select(mapper.Map<AnimalDto>).ToList();
        return result;
    }

    public async Task<AnimalDto> Update(UpdateAnimalRequest request)
    {
        // Validation
        var validation = await updateAnimalRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            throw new RequestValidationException(validation.Errors[0].ErrorMessage);
        }

        var animal = await animalRepository.GetAnimal(request.Id);

        if (animal is null)
            throw new EntityNotFoundException($"Animal with id {request.Id} doesn't exist.");

        animalRepository.SetOriginalRowVersion(animal, request.RowVersion);

        // Main implementation
        var username = currentUser.Username ?? throw new CustomAuthenticationException("Current user is not available.");
        
        animal.Name = request.Name;
        animal.Age = request.Age;
        animal.Weight = request.Weight;
        animal.Gender = (Gender)request.Gender;
        animal.Color = (Color)request.Color;
        animal.UpdatedByUsername = username;
        animal.UpdatedAt = DateTime.UtcNow;

        await animalRepository.Update(animal);

        return mapper.Map<AnimalDto>(animal);
    }

    public async Task SoftDelete(int id)
    {
        var animal = await animalRepository.GetAnimal(id);

        if (animal is null)
        {
            logger.LogError($"Animal with id {id} doesn't exist.");
            throw new EntityNotFoundException($"Animal with id {id} doesn't exist.");
        }

        var updatedBy = currentUser.Username ?? throw new CustomAuthenticationException("Current user is not available.");
        
        animal.Close(updatedBy);
        await animalRepository.Update(animal);
    }
}