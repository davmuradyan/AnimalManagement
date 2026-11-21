using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public class TranslationGroup : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool IsCustom { get; set; } = false;
    
    public List<GroupItem> GroupItems { get; set; } = new List<GroupItem>();
}