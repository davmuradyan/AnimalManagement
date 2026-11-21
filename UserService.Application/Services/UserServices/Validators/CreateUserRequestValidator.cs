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
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(12).WithMessage("Password must be at least 12 characters long.")
            .Matches(@"\p{Lu}").WithMessage("Password must contain an uppercase letter.")
            .Matches(@"\p{Ll}").WithMessage("Password must contain a lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain a number.")
            .Matches(@"[^\p{L}\d\s]").WithMessage("Password must contain a special character.");
    }
}