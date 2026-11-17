using System.Net;
using AnimalService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Infrastructure.Exceptions;

public class CustomDbUpdateException(string message, DbUpdateException innerException)
    :  BaseException(message, HttpStatusCode.Conflict, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);