using ExpenseTrackerGrupo4.src.Domain.Entities;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class BudgetWithExpensesValidator : AbstractValidator<BudgetWithExpenses>
{
    public BudgetWithExpensesValidator()
    {
        RuleFor(b => b.BudgetId).NotEmpty().WithMessage("Budget ID is required.");

        RuleFor(b => b.UserId).NotEmpty().WithMessage("User ID is required.");

        RuleFor(b => b.Month)
            .NotEmpty()
            .WithMessage("Month is required.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Month cannot be in the future.");

        RuleFor(b => b.BudgetAmount)
            .GreaterThan(0)
            .WithMessage("Budget amount must be greater than zero.");

        RuleFor(b => b.Amount)
            .GreaterThan(0)
            .When(b => b.Amount.HasValue)
            .WithMessage("Expense amount must be greater than zero.");

        RuleFor(b => b.Description)
            .MaximumLength(250)
            .WithMessage("Description cannot exceed 250 characters.")
            .When(b => !string.IsNullOrEmpty(b.Description));

        RuleFor(b => b.Category)
            .NotEmpty()
            .WithMessage("Category is required.")
            .MaximumLength(100)
            .WithMessage("Category cannot exceed 100 characters.");
    }
}
