using System.Net;
using AnimalService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnimalService.Infrastructure.Exceptions;

public class CustomRetryLimitExceededException(string message, RetryLimitExceededException innerException) : 
    BaseException(message, HttpStatusCode.InternalServerError, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);