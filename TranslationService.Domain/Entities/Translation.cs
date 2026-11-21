using AnimalManagement.Shared.Enums;
using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public sealed class Translation : BaseEntity
{
    public int GroupItemId { get; set; }
    public Language LanguageTag { get; set; }
    public string TranslatedText { get; set; }
    public GroupItem? GroupItemNavigation { get; set; }

    public Translation(int groupItemId, Language languageTag, string translatedText)
    {
        GroupItemId = groupItemId;
        LanguageTag = languageTag;
        TranslatedText = translatedText;
    }
}