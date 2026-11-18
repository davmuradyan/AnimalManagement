using System.Net;
using AnimalService.Domain.Exceptions;

namespace AnimalManagement.Shared.Exceptions.Domain;

public class DomainValidationException(string message)
    : BaseException(message, HttpStatusCode.InternalServerError, ErrorCodes.DOMAIN_VALIDATION_ERROR);