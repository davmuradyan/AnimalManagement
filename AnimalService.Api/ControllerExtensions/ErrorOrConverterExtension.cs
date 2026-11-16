using AnimalManagement.Shared.Dtos;
using AnimalService.Domain.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AnimalService.Api.ControllerExtensions;

public static class ErrorOrConverterExtension
{
    public static IActionResult ErrorOrConverter<T>(this ControllerBase controller, ErrorOr<T> result)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }
        return result.ErrorType switch
        {
            ErrorType.Validation =>
                controller.BadRequest(result.Error),

            ErrorType.NotFound =>
                controller.NotFound(new { error = result.Error }),
            
            ErrorType.Authentication =>
                controller.Unauthorized(new { error = result.Error }),
            

            _ => throw new Exception($"Unknown error type {result.ErrorType}.\nMessage: {result.Error}")
        };
    }
}