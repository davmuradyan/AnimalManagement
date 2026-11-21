using System.Net;

namespace AnimalManagement.Shared.Exceptions.Application;

public class EntityNotFoundException(string message) 
    : BaseException(message, HttpStatusCode.NotFound, Enums.ErrorCode.NOT_FOUND_ERROR.ToString());