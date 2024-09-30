using ExpenseTrackerGrupo4.src.Domain.Entities;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class ExpenseValidator : AbstractValidator<Expense>
{
    public ExpenseValidator()
    {
        RuleFor(e => e.UserId).NotEmpty().WithMessage("User ID is required.");

        RuleFor(e => e.Amount).GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(e => e.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(250)
            .WithMessage("Description cannot exceed 250 characters.");

        RuleFor(e => e.CategoryId)
            .NotNull()
            .WithMessage("Category ID is required.");

        RuleFor(e => e.Date)
            .NotEmpty()
            .WithMessage("Date is required.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Date cannot be in the future.");
    }
}
