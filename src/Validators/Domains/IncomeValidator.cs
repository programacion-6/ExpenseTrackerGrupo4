using ExpenseTrackerGrupo4.src.Domain.Entities;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class IncomeValidator : AbstractValidator<Income>
{
    public IncomeValidator()
    {
        RuleFor(i => i.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(i => i.Amount)
            .GreaterThan(0).WithMessage("Income amount must be greater than zero.");

        RuleFor(i => i.Source)
            .NotEmpty().WithMessage("Source is required.")
            .MaximumLength(100).WithMessage("Source cannot exceed 100 characters.");

        RuleFor(i => i.Date)
            .NotEmpty().WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");
    }
}