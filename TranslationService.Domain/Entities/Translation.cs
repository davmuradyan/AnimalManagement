using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public class Translation : BaseEntity
{
    public int GroupItemId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public GroupItem? GroupItemNavigation { get; set; }
}