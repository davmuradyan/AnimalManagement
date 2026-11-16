using AnimalManagement.Shared.Dtos;
using AnimalManagement.Shared.Enums;
using Microsoft.Extensions.Logging;
using TranslationService.Application.Interfaces.Repositories;
using TranslationService.Domain.Contracts.Responses;

namespace TranslationService.Application.Services.TranslationServices;

public class TranslationService(ITranslationRepository translationRepository,
    ILogger<TranslationService> logger) : ITranslationService
{
    public async Task<GetEnumTypeTranslationsResponse> GetEnumTypeTranslations(string selectedEnumType, string selectedLanguage)
    {
        // Validation
        if (!Enum.TryParse<Language>(selectedLanguage, out var language) || !Enum.IsDefined(language))
        {
            logger.LogError($"Invalid language {selectedLanguage}");
            ErrorOr<GetAllTranslationsResponse>.Failure($"Selected language {selectedLanguage} is not valid.",
                ErrorType.Validation);
        }
        
        if (!Enum.TryParse<TranslationTypes>(selectedEnumType, out var enumType) || !Enum.IsDefined(enumType))
        {
            logger.LogError($"Invalid enum type {selectedEnumType}");
            ErrorOr<GetAllTranslationsResponse>.Failure($"Selected enum type {selectedEnumType} is not valid.",
                ErrorType.Validation);
        }
        
        var resultFromRepo = await translationRepository.GetEnumTypeTranslations(enumType.ToString(), language);
        
        var result = resultFromRepo.Select(t => new EnumTypeTranslation(t.Item1, t.Item2)).ToList();
        return new GetEnumTypeTranslationsResponse(result);
    }

    public async Task<int> GetEnumCode(string selectedEnumType, string translation)
    {
        if (!Enum.TryParse<TranslationTypes>(selectedEnumType, out var enumType) || !Enum.IsDefined(enumType))
        {
            logger.LogError($"Invalid enum type {selectedEnumType}");
            ErrorOr<GetAllTranslationsResponse>.Failure($"Selected enum type {selectedEnumType} is not valid.",
                ErrorType.Validation);
        }
        
        var result = await translationRepository.GetEnumCode(enumType.ToString(), translation);
        return result;
    }

    public async Task<ErrorOr<GetAllTranslationsResponse>> GetAllTranslations(string selectedLanguage)
    {
        // Validation
        if (!Enum.TryParse<Language>(selectedLanguage, out var language) || !Enum.IsDefined(language))
        {
            logger.LogError($"Invalid language {selectedLanguage}");
            ErrorOr<GetAllTranslationsResponse>.Failure($"Selected language {selectedLanguage} is not valid.",
                ErrorType.Validation);
        }

        // Main implementation
        var result = await translationRepository.GetAllEnumTypes(language);

        // Response
        var responseValue = new GetAllTranslationsResponse(
            result.ToDictionary(
                x => x.Key,
                x => x.Value
                    .Select(t => new EnumTranslationResponse(t.Item1, t.Item2))
                    .ToList()
            )
        );

        return ErrorOr<GetAllTranslationsResponse>.Success(responseValue);
    }
}