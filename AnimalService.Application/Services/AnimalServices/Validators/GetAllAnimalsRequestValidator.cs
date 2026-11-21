using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using FluentValidation;

namespace AnimalService.Application.Services.AnimalServices.Validators;

public class GetAllAnimalsRequestValidator : AbstractValidator<GetAllAnimalsRequest>
{
    public GetAllAnimalsRequestValidator()
    {
        RuleFor(r => r.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than zero.");

        RuleFor(r => r.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than zero.");

        RuleFor(r => r.Count)
            .GreaterThan(0)
            .WithMessage("Count cannot be non-positive.");

        RuleFor(r => r.MinAge)
            .GreaterThanOrEqualTo(0)
            .When(r => r.MinAge.HasValue)
            .WithMessage("Minimum age cannot be negative.");

        RuleFor(r => r.MaxAge)
            .GreaterThanOrEqualTo(0)
            .When(r => r.MaxAge.HasValue)
            .WithMessage("Maximum age cannot be negative.");

        RuleFor(r => r.MinWeight)
            .GreaterThan(0)
            .When(r => r.MinWeight.HasValue)
            .WithMessage("Minimum weight must be greater than zero.");

        RuleFor(r => r.MaxWeight)
            .GreaterThan(0)
            .When(r => r.MaxWeight.HasValue)
            .WithMessage("Maximum weight must be greater than zero.");

        RuleFor(r => r)
            .Must(r => !r.MinAge.HasValue ||
                       !r.MaxAge.HasValue ||
                       r.MinAge <= r.MaxAge)
            .WithMessage("Minimum age cannot exceed maximum age.");

        RuleFor(r => r)
            .Must(r => !r.MinWeight.HasValue ||
                       !r.MaxWeight.HasValue ||
                       r.MinWeight <= r.MaxWeight)
            .WithMessage("Minimum weight cannot exceed maximum weight.");

        RuleForEach(r => r.Colors)
            .Must(color => Enum.TryParse<Color>(color, true, out var parsed)
                           && Enum.IsDefined(parsed))
            .WithMessage($"Invalid color.");

        RuleFor(r => r.Gender)
            .Must(gender => gender == null ||
                            (Enum.TryParse<Gender>(gender, true, out var parsed)
                             && Enum.IsDefined(parsed)))
            .WithMessage("Invalid gender.");
    }
}