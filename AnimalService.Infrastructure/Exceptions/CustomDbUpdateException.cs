using System.Net;
using AnimalManagement.Shared.Exceptions;
using AnimalService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AnimalService.Infrastructure.Exceptions;

public class CustomDbUpdateException(DbUpdateException innerException)
    :  BaseException("Oops, something went wrong, please retry.", HttpStatusCode.Conflict, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);