using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public sealed class TranslationGroup : BaseEntity
{
    public string Name { get; set; }
    public bool IsCustom { get; set; }
    
    public List<GroupItem> GroupItems { get; set; } = new();

    public TranslationGroup(string name, bool isCustom)
    {
        Name = name;
        IsCustom = isCustom;
    }
}