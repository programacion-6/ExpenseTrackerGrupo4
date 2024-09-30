using ExpenseTrackerGrupo4.src.Domain.Entities;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class BudgetValidator : AbstractValidator<Budget>
{
    public BudgetValidator()
    {
        RuleFor(b => b.UserId).NotEmpty().WithMessage("User ID is required.");

        RuleFor(b => b.Month)
            .NotEmpty()
            .WithMessage("Month is required.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Month cannot be in the future.");

        RuleFor(b => b.BudgetAmount)
            .GreaterThan(0)
            .WithMessage("Budget amount must be greater than zero.");
    }
}
