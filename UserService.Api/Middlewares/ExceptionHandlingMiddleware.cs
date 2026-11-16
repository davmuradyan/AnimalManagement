using UserService.Domain.Exceptions;
using UserService.Infrastructure.Exceptions;

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
        catch (DomainValidationException ex)
        {
            logger.LogWarning(ex, "Domain validation failed.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status400BadRequest,
                ex.Message);
        }
        catch (CustomConcurrencyException ex)
        {
            logger.LogWarning(ex, "Database concurrency conflict.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status409Conflict,
                "The resource was modified by another operation.");
        }
        catch (CustomRetryLimitExceededException ex)
        {
            logger.LogError(ex, "Database retry limit exceeded.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status503ServiceUnavailable,
                "The database is temporarily unavailable.");
        }
        catch (CustomDbUpdateException ex)
        {
            logger.LogError(ex, "Database update failed.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status500InternalServerError,
                "An error occurred while updating the database.");
        }
        catch (CustomException ex)
        {
            logger.LogError(ex, "A custom exception occurred.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.");
        }
        catch (NotImplementedException ex)
        {
            logger.LogError(ex, "Not implemented exception occurred.");
            
            await WriteErrorResponse(
                context,
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred. Please try again later.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred.");

            await WriteErrorResponse(
                context,
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred. Please try again later.");
        }
    }

    private static async Task WriteErrorResponse(
        HttpContext context,
        int statusCode,
        string message)
    {
        if (context.Response.HasStarted)
            throw new InvalidOperationException(
                "Cannot modify a response that has already started.");

        context.Response.Clear();
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            message
        });
    }
}
