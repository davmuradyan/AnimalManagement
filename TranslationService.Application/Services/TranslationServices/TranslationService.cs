using AnimalManagement.Shared.Enums;
using AnimalManagement.Shared.Exceptions.Application;
using Microsoft.Extensions.Logging;
using TranslationService.Application.Interfaces.Repositories;
using TranslationService.Domain.Contracts.Responses;

namespace TranslationService.Application.Services.TranslationServices;

public class TranslationService(ITranslationRepository translationRepository,
    ILogger<TranslationService> logger) : ITranslationService
{
    public async Task<GetEnumTypeTranslationsResponse> GetEnumTypeTranslations(string selectedEnumType, int selectedLanguage)
    {
        // Validation
        if (!Enum.IsDefined((Language)selectedLanguage))
        {
            logger.LogError($"Invalid language {selectedLanguage}");
            throw new RequestValidationException($"Selected language {selectedLanguage} is not valid.");
        }

        Language language = (Language)selectedLanguage;
        
        if (!Enum.TryParse<TranslationGroups>(selectedEnumType, out var enumType) || !Enum.IsDefined(enumType))
        {
            logger.LogError($"Invalid enum type {selectedEnumType}");
            throw new RequestValidationException($"Selected enum type {selectedEnumType} is not valid.");
        }
        
        // Implementation
        var resultFromRepo = await translationRepository.GetGroupTranslations(enumType.ToString(), language);
        
        var result = resultFromRepo.Select(t => new EnumTypeTranslation(t.Item1, t.Item2)).ToList();
        return new GetEnumTypeTranslationsResponse(result);
    }

    public async Task<int> GetEnumCode(string selectedEnumType, string translation)
    {
        // Verification
        if (!Enum.TryParse<TranslationGroups>(selectedEnumType, out var enumType) || !Enum.IsDefined(enumType))
        {
            logger.LogError($"Invalid enum type {selectedEnumType}");
            throw new RequestValidationException($"Selected enum type {selectedEnumType} is not valid.");
        }
        
        var result = await translationRepository.GetEnumCode(enumType.ToString(), translation);
        return result;
    }

    public async Task<GetAllTranslationsResponse> GetAllTranslations(int selectedLanguage)
    {
        // Validation
        if (!Enum.IsDefined((Language)selectedLanguage))
        {
            logger.LogError($"Invalid language {selectedLanguage}");
            throw new RequestValidationException($"Selected language {selectedLanguage} is not valid.");
        }

        Language language = (Language)selectedLanguage;
        
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

        return responseValue;
    }
}