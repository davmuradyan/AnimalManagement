using System.Net;

namespace AnimalManagement.Shared.Exceptions.Application;

public class ExternalServiceException(string message, Exception? innerException = null) 
    : BaseException(message, HttpStatusCode.InternalServerError, Enums.ErrorCode.INTERNAL_SERVER_ERROR.ToString(), innerException);