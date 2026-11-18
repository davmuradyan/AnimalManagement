using System.Net;
using AnimalManagement.Shared.Exceptions;
using AnimalService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace AnimalService.Infrastructure.Exceptions;

public class CustomRetryLimitExceededException(RetryLimitExceededException innerException) : 
    BaseException("Sorry, something went wrong. We are already working on it!", HttpStatusCode.InternalServerError, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);