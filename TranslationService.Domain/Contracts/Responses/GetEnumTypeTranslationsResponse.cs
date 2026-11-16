namespace TranslationService.Domain.Contracts.Responses;

public record EnumTypeTranslation(int EnumType, string Translation);
public record GetEnumTypeTranslationsResponse(List<EnumTypeTranslation> EnumTypeTranslations);