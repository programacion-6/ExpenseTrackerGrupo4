using ExpenseTrackerGrupo4.src.Domain.Entities;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MaximumLength(150)
            .WithMessage("Email cannot exceed 150 characters.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Email must be a valid email address.");

        RuleFor(u => u.PasswordHash).NotEmpty().WithMessage("Password is required.");
    }
}
