using System.Net;

namespace AnimalManagement.Shared.Exceptions.Application;

public class EntityNotFoundException(string message) 
    : BaseException(message, HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND_ERROR);