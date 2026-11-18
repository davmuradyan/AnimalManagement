using AnimalManagement.Shared.Dtos;
using TranslationService.Domain.Contracts.Responses;

namespace TranslationService.Application.Services.TranslationServices;

public interface ITranslationService
{
    Task<GetEnumTypeTranslationsResponse> GetEnumTypeTranslations(string selectedEnumType, int selectedLanguage);
    Task<int> GetEnumCode(string selectedEnumType, string translation);
    Task<GetAllTranslationsResponse> GetAllTranslations(int language);
}