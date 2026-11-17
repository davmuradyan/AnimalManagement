using System.Net;

namespace AnimalService.Domain.Exceptions.Application;

public class EntityNotFoundException(string message) 
    : BaseException(message, HttpStatusCode.NotFound, ErrorCodes.NOT_FOUND_ERROR);