using AnimalManagement.Shared.Dtos;
using TranslationService.Domain.Contracts.Responses;

namespace TranslationService.Application.Services.TranslationServices;

public interface ITranslationService
{
    Task<GetEnumTypeTranslationsResponse> GetEnumTypeTranslations(string selectedEnumType, string selectedLanguage);
    Task<int> GetEnumCode(string selectedEnumType, string translation);
    Task<ErrorOr<GetAllTranslationsResponse>> GetAllTranslations(string language);
}