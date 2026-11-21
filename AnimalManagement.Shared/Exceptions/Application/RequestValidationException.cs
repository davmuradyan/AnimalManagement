using System.Net;

namespace AnimalManagement.Shared.Exceptions.Application;

public class RequestValidationException(string message)
    : BaseException(message, HttpStatusCode.BadRequest, Enums.ErrorCode.REQUEST_VALIDATION_ERROR.ToString());