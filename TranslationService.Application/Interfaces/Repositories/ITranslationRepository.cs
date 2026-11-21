using AnimalManagement.Shared.Enums;

namespace TranslationService.Application.Interfaces.Repositories;

public interface ITranslationRepository
{
    Task<List<(int, string)>> GetGroupTranslations(string translationGroup, Language language);
    Task<int> GetEnumCode(string enumType, string translation);
    Task<Dictionary<string, List<(int, string)>>> GetAllEnumTypes(Language language);
}