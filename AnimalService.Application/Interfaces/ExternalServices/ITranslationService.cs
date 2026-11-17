using AnimalManagement.Shared.Enums;

namespace AnimalService.Application.Interfaces.ExternalServices;

public interface ITranslationService
{
    Task<List<(int, string)>> GetEnumTypeTranslations(string enumType, Language language, CancellationToken cancellationToken = default);
    Task<int> GetEnumCode(string enumType, string translation, CancellationToken cancellationToken = default);
}