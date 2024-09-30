namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

using FluentValidation;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class PasswordResetConfirmDTOValidator : AbstractValidator<PasswordResetConfirmDto>
{
    public PasswordResetConfirmDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");

        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required.");
    }
}

