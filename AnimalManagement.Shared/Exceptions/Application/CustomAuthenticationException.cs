using System.Net;

namespace AnimalManagement.Shared.Exceptions.Application;

public class CustomAuthenticationException(string message)
    : BaseException(message, HttpStatusCode.Unauthorized, Enums.ErrorCode.AUTHENTICATION_ERROR.ToString());