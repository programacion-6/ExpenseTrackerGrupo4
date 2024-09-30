namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

using FluentValidation;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class PasswordResetRequestDTOValidator : AbstractValidator<PasswordResetRequestDto>
{
    public PasswordResetRequestDTOValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email must be a valid email address.");
    }
}

