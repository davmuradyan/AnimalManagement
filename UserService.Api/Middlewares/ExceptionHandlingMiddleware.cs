using AnimalManagement.Shared.Enums;
using AnimalManagement.Shared.Exceptions;

namespace UserService.Api.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (BaseException ex)
        {
            logger.LogWarning(ex, "Application error: {ErrorCode}", ex.ErrorCode);

            await WriteErrorResponse(
                context,
                (int)ex.StatusCode,
                ex.ErrorCode,
                ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception occurred.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status500InternalServerError,
                ErrorCode.INTERNAL_SERVER_ERROR.ToString(),
                "An unexpected error occurred. Please try again later.");
        }
    }

    private static async Task WriteErrorResponse(
        HttpContext context,
        int statusCode,
        string errorCode,
        string message)
    {
        context.Response.Clear();
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            errorCode,
            message
        });
    }
}