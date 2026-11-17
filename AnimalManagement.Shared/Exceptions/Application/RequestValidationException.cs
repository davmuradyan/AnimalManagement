using System.Net;

namespace AnimalService.Domain.Exceptions.Application;

public class RequestValidationException(string message)
    : BaseException(message, HttpStatusCode.BadRequest, ErrorCodes.REQUEST_VALIDATION_ERROR);