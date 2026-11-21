namespace AnimalManagement.Shared.Enums;

public enum TranslationGroups
{
    Color = 1,
    Gender = 2,
    Language = 3
}

public enum Color
{
    Red = 1,
    Green = 2,
    Blue = 3
}

public enum Gender
{
    Male = 1,
    Female = 2
}

public enum Language
{
    En = 1,
    Hy = 2
}

public enum ErrorCode
{
    INTERNAL_SERVER_ERROR = 1,
    REQUEST_VALIDATION_ERROR = 2,
    NOT_FOUND_ERROR = 3,
    AUTHENTICATION_ERROR = 4,
}