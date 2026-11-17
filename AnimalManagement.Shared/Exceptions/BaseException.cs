using System.Net;

namespace AnimalService.Domain.Exceptions;

public class BaseException(string message, HttpStatusCode statusCode, string errorCode, Exception? innerException = null)
    : ApplicationException(message, innerException)
{
    public string ErrorCode { get; set; } = errorCode;
    public HttpStatusCode StatusCode { get; set; } = statusCode;
}