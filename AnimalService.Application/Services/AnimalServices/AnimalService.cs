using AnimalManagement.Shared.Dtos;
using AnimalManagement.Shared.Enums;
using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Application.Interfaces.Repositories;
using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using AnimalService.Domain.Entities;
using AnimalService.Domain.Exceptions.Application;
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

        var language = !Enum.IsDefined((Language)request.Language) ? Language.Eng : (Language)request.Language;
        
        // Main implementation
        (Gender genderEnum, Color colorEnum) = await ConvertToGenderAndColor(request.Gender, request.Color, language);
        
        var animal = Animal.Create(
            name: request.Name,
            color: colorEnum,
            gender: genderEnum,
            age: request.Age,
            weight: request.Weight);
        
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
        
        var language = !Enum.IsDefined((Language)request.Language) ? Language.Eng : (Language)request.Language;

        var colors = await translationService.GetEnumTypeTranslations(nameof(Color), language);
        var genders = await translationService.GetEnumTypeTranslations(nameof(Gender), language);

        var result = animals.Select(a =>
        {
            // TODO: mapper cannot translate
            string gender = a.Gender == 0 ? genders[0].Item2 : genders[1].Item2;
            string color = colors.First(c => c.Item1 == (int)a.Color).Item2;
            return mapper.Map<AnimalDto>(a);
        }).ToList();
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

        // Main implementation
        var language = !Enum.IsDefined((Language)request.SelectedLanguage) ? Language.Eng : (Language)request.SelectedLanguage;

        (Gender genderEnum, Color colorEnum) = await ConvertToGenderAndColor(request.Gender, request.Color, language);
        
        animal.Update(request.Name, request.Age, request.Weight, genderEnum, colorEnum);
        await animalRepository.Update(animal);

        // Response
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

        animal.Close();

        await animalRepository.Update(animal);
    }

    private async Task<(Gender, Color)> ConvertToGenderAndColor(string requestedGender, string requestedColor, Language language)
    {
        if (language != Language.Eng)
        {
            requestedGender = ((Gender)await translationService
                .GetEnumCode(nameof(Gender), requestedGender)).ToString();

            requestedColor = ((Color)await translationService
                .GetEnumCode(nameof(Color), requestedColor)).ToString();
        }

        if (!Enum.TryParse(requestedGender, true, out Gender genderEnum) ||
            !Enum.IsDefined(genderEnum))
            throw new ExternalServiceException("Sorry, something went wrong. We are already working on it!");

        if (!Enum.TryParse(requestedColor, true, out Color colorEnum) ||
            !Enum.IsDefined(colorEnum))
            throw new ExternalServiceException("Sorry, something went wrong. We are already working on it!");


        return (genderEnum, colorEnum);
    }
}