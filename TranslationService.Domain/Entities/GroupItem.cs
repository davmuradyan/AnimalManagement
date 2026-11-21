using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public sealed class GroupItem : BaseEntity
{
    public int GroupId { get; set; }
    public int ItemCode { get; set; }
    public TranslationGroup? GroupNavigation { get; set; }
    public List<Translation> Translations { get; set; } = new();

    public GroupItem(int groupId, int itemCode)
    {
        GroupId = groupId;
        ItemCode = itemCode;
    }
}