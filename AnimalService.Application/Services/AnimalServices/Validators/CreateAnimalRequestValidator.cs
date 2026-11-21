using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using FluentValidation;

namespace AnimalService.Application.Services.AnimalServices.Validators;

public class CreateAnimalRequestValidator
    : AbstractValidator<CreateAnimalRequest>
{
    public CreateAnimalRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Name must be a valid name.");

        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Age must be greater or equal 0");

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .Must(double.IsFinite)
            .WithMessage("Weight must be a finite number that is greater than or equals to 0.");
        
        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(x =>  Enum.IsDefined((Gender)x))
            .WithMessage("Gender must be a valid gender.");

        RuleFor(x => x.Color)
            .NotEmpty()
            .Must(x => Enum.IsDefined((Color)x))
            .WithMessage("Color must be a valid color.");
    }
}