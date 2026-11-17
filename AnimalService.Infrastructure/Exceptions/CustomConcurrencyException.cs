using System.Net;
using AnimalService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Infrastructure.Exceptions;

public class CustomConcurrencyException(string message, DbUpdateConcurrencyException innerException) :
    BaseException(message, HttpStatusCode.Conflict, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);