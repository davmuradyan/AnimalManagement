using AnimalManagement.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using TranslationService.Application.Interfaces.Repositories;
using TranslationService.Infrastructure.Database;

namespace TranslationService.Infrastructure.Repositories;

public class TranslationRepository(MainDbContext context) : ITranslationRepository
{
    public async Task<List<(int, string)>> GetGroupTranslations(
        string translationGroup,
        Language language)
    {
        var entities = await context.Translations
            .Where(t => t.LanguageTag == language)
            .Where(t => t.GroupItemNavigation!.GroupNavigation!.Name == translationGroup)
            .Select(t => new
            {
                t.GroupItemNavigation!.ItemCode,
                t.TranslatedText
            })
            .ToListAsync();

        return entities
            .Select(e => (e.ItemCode, e.TranslatedText))
            .ToList();
    }
    
    public async Task<int> GetEnumCode(string enumType, string translation)
    {
        var entity = await context.Translations
            .Include(t => t.GroupItemNavigation)
            .SingleAsync(t => t.TranslatedText == translation);
            
    
        return entity.GroupItemNavigation!.ItemCode;
    }
    
    public async Task<Dictionary<string, List<(int, string)>>> GetAllEnumTypes(Language language)
    {
        return await context.Translations
            .Where(t => t.LanguageTag == language)
            .Include(t => t.GroupItemNavigation)
            .ThenInclude(g => g!.GroupNavigation)
            .GroupBy(x => x.GroupItemNavigation!.GroupNavigation!.Name)
            .ToDictionaryAsync(
                group => group.Key,
                group => group
                    .Select(x => (
                        x.GroupItemNavigation!.ItemCode,
                        x.TranslatedText
                    ))
                    .ToList());
    }
}