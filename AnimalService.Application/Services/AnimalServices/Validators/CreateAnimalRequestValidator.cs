using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using FluentValidation;

namespace AnimalService.Application.Services.AnimalServices.Validators;

public class CreateAnimalRequestValidator
    : AbstractValidator<CreateAnimalRequest>
{
    public CreateAnimalRequestValidator()
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
            .MaximumLength(100)
            .WithMessage("Name must be a valid name.");

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
        
        RuleFor(x => x.Language)
            .NotEmpty()
            .Must(x => Enum.TryParse<Language>(x, true, out var value)
                && Enum.IsDefined(value))
            .WithMessage("Language must be a valid language (Eng or Arm).");
    }
}