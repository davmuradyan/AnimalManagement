namespace AnimalService.Domain.Contracts.Responses;

public record EnumTranslationResponse(int Code, string Translation);

public record GetAllTranslationsResponse(
    Dictionary<string, List<EnumTranslationResponse>> Translations);
    
public record EnumTypeTranslation(int EnumType, string Translation);
public record GetEnumTypeTranslationsResponse(List<EnumTypeTranslation> EnumTypeTranslations);