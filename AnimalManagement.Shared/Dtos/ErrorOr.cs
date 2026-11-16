namespace AnimalManagement.Shared.Dtos;

public class ErrorOr<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; private set; }
    public string? Error { get; private set; }
    public ErrorType? ErrorType { get; private set; }

    private ErrorOr() { }

    public static ErrorOr<T> Success(T value)
    {
        return new ErrorOr<T>
        {
            IsSuccess = true,
            Value = value
        };
    }

    public static ErrorOr<T> Failure(string error, ErrorType errorType)
    {
        return new ErrorOr<T>
        {
            IsSuccess = false,
            Error = error,
            ErrorType = errorType
        };
    }
}

public enum ErrorType
{
    Validation,
    NotFound,
    Authentication
}