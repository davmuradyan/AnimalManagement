using AnimalManagement.Shared.Enums;
using TranslationService.Domain.Entities.Base;

namespace TranslationService.Domain.Entities;

public class EnumTranslation : BaseEntity
{
    public string EnumType { get; set; } = string.Empty;
    public int EnumCode { get; set; }
    public Language Language { get; set; } = Language.Eng;
    public string Translation { get; set; } = string.Empty;
}