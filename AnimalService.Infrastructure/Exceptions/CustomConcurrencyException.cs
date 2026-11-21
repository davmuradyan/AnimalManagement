using System.Net;
using AnimalManagement.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Infrastructure.Exceptions;

public class CustomConcurrencyException(DbUpdateConcurrencyException innerException) :
    BaseException("Oops, another user changed the animal. Please try again.", HttpStatusCode.Conflict, AnimalManagement.Shared.Enums.ErrorCode.INTERNAL_SERVER_ERROR.ToString(), innerException);