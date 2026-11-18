namespace AnimalManagement.Shared.Exceptions;

public class ErrorCodes
{
    // Application
    public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
    public const string REQUEST_VALIDATION_ERROR = "REQUEST_VALIDATION_ERROR";
    public const string NOT_FOUND_ERROR = "NOT_FOUND_ERROR";
    public const string AUTHENTICATION_ERROR = "AUTHENTICATION_ERROR";
    
    // Domain
    public const string DOMAIN_VALIDATION_ERROR = "DOMAIN_VALIDATION_ERROR";
}