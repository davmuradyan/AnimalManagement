using AnimalManagement.Shared.Dtos;
using AnimalManagement.Shared.Enums;
using AnimalService.Application.Interfaces.ExternalServices;
using AnimalService.Application.Interfaces.Repositories;
using AnimalService.Domain.Contracts.Dtos;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using AnimalService.Domain.Entities;
using AnimalService.Domain.HelperClasses;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace AnimalService.Application.Services.AnimalServices;

public sealed class AnimalService(
    IValidator<CreateAnimalRequest> createAnimalRequestValidator,
    IValidator<GetAllAnimalsRequest> getAllAnimalsRequestValidator,
    IValidator<UpdateAnimalRequest> updateAnimalRequestValidator,
    IAnimalRepository animalRepository,
    ITranslationService translationService,
    ILogger<AnimalService> logger) : IAnimalService
{
    public async Task<ErrorOr<AnimalDto>> Create(CreateAnimalRequest request)
    {
        // Validation
        var validation = await createAnimalRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            return ErrorOr<AnimalDto>.Failure(validation.Errors[0].ErrorMessage, ErrorType.Validation);
        }
        
        // Main implementation
        if (!Enum.TryParse<Language>(request.Language, out var language) || !Enum.IsDefined(typeof(Language), language))
            language = Language.Eng;

        var genderColor = await ConvertToGenderAndColor(request.Gender, request.Color, language);

        if (!genderColor.IsSuccess)
            return ErrorOr<AnimalDto>.Failure(genderColor.Error!, ErrorType.Validation);

        (Gender genderEnum, Color colorEnum) = genderColor.Value;

        var animal = Animal.Create(
            name: request.Name,
            color: colorEnum,
            gender: genderEnum,
            age: request.Age,
            weight: request.Weight);
        
        await animalRepository.Create(animal);
        
        // Response
        return ErrorOr<AnimalDto>.Success(animal.MapToAnimalDto(request.Gender, request.Color));
    }

    public async Task<ErrorOr<List<AnimalDto>>> GetAll(GetAllAnimalsRequest request)
    {
        // Validation
        var validation = await getAllAnimalsRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            return ErrorOr<List<AnimalDto>>.Failure(validation.Errors[0].ErrorMessage, ErrorType.Validation);
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
        
        if (!Enum.TryParse<Language>(request.Language, out var language) || !Enum.IsDefined(typeof(Language), language))
        {
            language = Language.Eng;
        }

        var colors = await translationService.GetEnumTypeTranslations(nameof(Color), language);
        var genders = await translationService.GetEnumTypeTranslations(nameof(Gender), language);

        var result = animals.Select(a =>
        {
            string gender = a.Gender == 0 ? genders[0].Item2 : genders[1].Item2;
            string color = colors.First(c => c.Item1 == (int)a.Color).Item2;
            return a.MapToAnimalDto(gender, color);
        }).ToList();
        return ErrorOr<List<AnimalDto>>.Success(result);
    }

    public async Task<ErrorOr<AnimalDto>> Update(UpdateAnimalRequest request)
    {
        // Validation
        var validation = await updateAnimalRequestValidator.ValidateAsync(request);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
            {
                logger.LogError(error.ErrorMessage);
            }
            return ErrorOr<AnimalDto>.Failure(validation.Errors[0].ErrorMessage, ErrorType.Validation);
        }

        var animal = await animalRepository.GetAnimal(request.Id);

        if (animal is null)
            return ErrorOr<AnimalDto>.Failure($"Animal with id {request.Id} doesn't exist.", ErrorType.NotFound);

        // Main implementation
        if (!Enum.TryParse<Language>(request.SelectedLanguage, out var language) || !Enum.IsDefined(typeof(Language), language))
            language = Language.Eng;

        var genderColor = await ConvertToGenderAndColor(request.Gender, request.Color, language);

        if (!genderColor.IsSuccess)
            return ErrorOr<AnimalDto>.Failure(genderColor.Error!, ErrorType.Validation);

        (Gender genderEnum, Color colorEnum) = genderColor.Value; 

        animal.Update(request.Name, request.Age, request.Weight, genderEnum, colorEnum);
        await animalRepository.Update(animal);

        // Response
        return ErrorOr<AnimalDto>.Success(animal.MapToAnimalDto(request.Gender, request.Color));
    }

    public async Task<ErrorOr<bool>> SoftDelete(int id)
    {
        var animal = await animalRepository.GetAnimal(id);

        if (animal is null)
        {
            logger.LogError($"Animal with id {id} doesn't exist.");
            return ErrorOr<bool>.Failure($"Animal with id {id} doesn't exist.", ErrorType.NotFound);
        }

        animal.Close();

        await animalRepository.Update(animal);
        return ErrorOr<bool>.Success(true);
    }

    private async Task<ErrorOr<(Gender, Color)>> ConvertToGenderAndColor(string requestedGender, string requestedColor, Language language)
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
        {
            return ErrorOr<(Gender, Color)>.Failure(
                $"Invalid gender: {requestedGender}",
                ErrorType.Validation);
        }

        if (!Enum.TryParse(requestedColor, true, out Color colorEnum) ||
            !Enum.IsDefined(colorEnum))
        {
            return ErrorOr<(Gender, Color)>.Failure(
                $"Invalid color: {requestedColor}",
                ErrorType.Validation);
        }

        return ErrorOr<(Gender, Color)>.Success((genderEnum, colorEnum));
    }
}