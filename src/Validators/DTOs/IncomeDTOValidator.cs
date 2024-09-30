using FluentValidation;
using ExpenseTrackerGrupo4.src.Application.DTOs;

namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

public class IncomeDtoValidator : AbstractValidator<IncomeDto>
{
    public IncomeDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Source)
            .NotEmpty().WithMessage("Source is required.")
            .MaximumLength(100).WithMessage("Source cannot exceed 100 characters.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");
    }
}

