using FluentValidation;
using UserService.Domain.Contracts.UserRequests;

namespace UserService.Application.Services.UserServices.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("First name is required");
        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage("Last name is required");
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(8)
            .WithMessage("Username must be at least 8 characters long")
            .Must(x => x.All(c =>
                char.IsLetterOrDigit(c) || c is '-' or '_' or '.'))
            .WithMessage("Username can contain only letters, numbers, \"-\", \"_\" and \".\" characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(12)
            .WithMessage("Password must be at least 12 characters long.")
            .Must(x => x.Any(char.IsUpper))
            .Must(x => x.Any(char.IsLower))
            .Must(x => x.Any(char.IsDigit))
            .Must(x => x.Any(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)))
            .WithMessage(
                "Password must contain at least one uppercase letter, one lowercase letter, one number and one special character.");
    }
}