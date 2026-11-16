using Microsoft.EntityFrameworkCore.Storage;

namespace AnimalService.Infrastructure.DAL.Exceptions;

public class CustomRetryLimitExceededException : CustomException
{
    public CustomRetryLimitExceededException() { }
    public CustomRetryLimitExceededException(string message)
        : base(message) { }
    public CustomRetryLimitExceededException(
        string message, RetryLimitExceededException innerException)
        : base(message, innerException) { }
}