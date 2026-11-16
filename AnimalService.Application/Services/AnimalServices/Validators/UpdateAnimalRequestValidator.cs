using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using FluentValidation;

namespace AnimalService.Application.Services.AnimalServices.Validators;

public class UpdateAnimalRequestValidator : AbstractValidator<UpdateAnimalRequest>
{
    public UpdateAnimalRequestValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Age must be greater or equal 0");

        RuleFor(x => x.Weight)
            .GreaterThanOrEqualTo(0)
            .Must(double.IsFinite)
            .WithMessage("Weight must be a finite number that is greater than or equals to 0.");
        
        RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
            .WithMessage("Name cannot be null or empty.")
                .MaximumLength(100)
            .WithMessage("Name length should be less than 100 characters.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(x => Enum.TryParse<Gender>(x, true, out var value)
                       && Enum.IsDefined(value))
            .WithMessage("Gender must be a valid gender.");

        RuleFor(x => x.Color)
            .NotEmpty()
            .Must(x => Enum.TryParse<Color>(x, true, out var value)
                       && Enum.IsDefined(value))
            .WithMessage("Color must be a valid color.");
    }
}