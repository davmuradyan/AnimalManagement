using System.Net;

namespace AnimalService.Domain.Exceptions.Application;

public class ExternalServiceException(string message, Exception? innerException = null) 
    : BaseException(message, HttpStatusCode.InternalServerError, ErrorCodes.INTERNAL_SERVER_ERROR, innerException);