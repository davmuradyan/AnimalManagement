using System.Net;

namespace AnimalService.Domain.Exceptions.Domain;

public class DomainValidationException(string message)
    : BaseException(message, HttpStatusCode.InternalServerError, ErrorCodes.DOMAIN_VALIDATION_ERROR);