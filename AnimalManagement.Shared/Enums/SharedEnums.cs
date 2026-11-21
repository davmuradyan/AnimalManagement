namespace AnimalManagement.Shared.Enums;

public enum TranslationTypes
{
    Color = 1,
    Gender = 2,
    Language = 3
}

public enum Color
{
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Purple,
    Pink,
    Black,
    White,
    Brown
}

public enum Gender
{
    Male,
    Female
}

public enum Language
{
    Eng,
    Hy
}

public enum ErrorCode
{
    INTERNAL_SERVER_ERROR = 1,
    REQUEST_VALIDATION_ERROR = 2,
    NOT_FOUND_ERROR = 3,
    AUTHENTICATION_ERROR = 4,
}