using System.Net;

namespace AnimalManagement.Shared.Exceptions.Application;

public class AuthenticationException(string message)
    : BaseException(message, HttpStatusCode.Unauthorized, ErrorCodes.AUTHENTICATION_ERROR);