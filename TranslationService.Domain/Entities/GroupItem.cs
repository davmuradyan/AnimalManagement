using System.Text.RegularExpressions;
using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public class GroupItem : BaseEntity
{
    public int GroupId { get; set; }
    public int ItemCode { get; set; }
    public Group? GroupNavigation { get; set; }
}