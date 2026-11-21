using System.Net;
using AnimalManagement.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace TranslationService.Infrastructure.Exceptions;

public class CustomConcurrencyException(DbUpdateConcurrencyException innerException) :
    BaseException("Oops, something went wrong, please retry.", HttpStatusCode.Conflict, AnimalManagement.Shared.Enums.ErrorCode.INTERNAL_SERVER_ERROR.ToString(), innerException);