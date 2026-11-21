using AnimalManagement.Shared.Enums;
using AnimalService.Domain.Contracts.Requests.AnimalRequests;
using FluentValidation;

namespace AnimalService.Application.Services.AnimalServices.Validators;

public class UpdateAnimalRequestValidator : AbstractValidator<UpdateAnimalRequest>
{
    public UpdateAnimalRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Non-positive id.");

        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Name cannot be null or empty.")
            .MaximumLength(100)
            .WithMessage("Name length should be less than 100 characters.");
        
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Age must be greater or equal 0");

        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .Must(double.IsFinite)
            .WithMessage("Weight must be a finite number that is greater than or equals to 0.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .Must(x => Enum.IsDefined((Gender)x))
            .WithMessage("Gender must be a valid gender.");

        RuleFor(x => x.Color)
            .NotEmpty()
            .Must(x => Enum.IsDefined((Color)x))
            .WithMessage("Color must be a valid color.");
        
        RuleFor(x => x.RowVersion)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .Must(x => x.Length == 8)
            .WithMessage("RowVersion must contain exactly 8 bytes.");
    }
}