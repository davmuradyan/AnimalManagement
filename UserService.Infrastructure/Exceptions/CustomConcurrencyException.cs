using System.Net;
using AnimalManagement.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace UserService.Infrastructure.Exceptions;

public class CustomConcurrencyException(DbUpdateConcurrencyException innerException) :
    BaseException("Oops, something went wrong, please retry.", HttpStatusCode.Conflict, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);