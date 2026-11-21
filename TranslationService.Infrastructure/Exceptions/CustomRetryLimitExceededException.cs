using System.Net;
using AnimalManagement.Shared.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace TranslationService.Infrastructure.Exceptions;

public class CustomRetryLimitExceededException(RetryLimitExceededException innerException) : 
    BaseException("Sorry, something went wrong. We are already working on it!", HttpStatusCode.InternalServerError, AnimalManagement.Shared.Enums.ErrorCode.INTERNAL_SERVER_ERROR.ToString(), innerException);