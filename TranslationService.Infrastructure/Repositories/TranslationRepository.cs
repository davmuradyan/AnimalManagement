using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using TranslationService.Application.Interfaces.Repositories;
using TranslationService.Infrastructure.Database;

namespace TranslationService.Infrastructure.Repositories;

public class TranslationRepository(MainDbContext context) : ITranslationRepository
{
    public async Task<string> Translate(string enumType, int enumValue, Language language)
    {
        var result = await context.EnumTranslations
            .Where(x => x.EnumType == enumType && language == x.Language)
            .SingleAsync(t => t.EnumCode == enumValue);
        
        return result.Translation;
    }

    public async Task<List<(int, string)>> GetEnumTypeTranslations(string enumType, Language language)
    {
        var entities = await context.EnumTranslations
            .Where(x => x.EnumType == enumType && language == x.Language)
            .ToListAsync();

        var result = entities.Select(e => (e.EnumCode, e.Translation)).ToList();
        return result;
    }

    public async Task<int> GetEnumCode(string enumType, string translation)
    {
        var entity = await context.EnumTranslations
            .SingleAsync(x => x.EnumType == enumType && translation == x.Translation);

        return entity.EnumCode;
    }

    public async Task<Dictionary<string, List<(int, string)>>> GetAllEnumTypes(Language language)
    {
        var translations = await context.EnumTranslations
            .Where(x => x.Language == language)
            .GroupBy(x => x.EnumType)
            .ToDictionaryAsync(
                group => group.Key,
                group => group
                    .Select(x => (x.EnumCode, x.Translation))
                    .ToList());

        return translations;
    }
}