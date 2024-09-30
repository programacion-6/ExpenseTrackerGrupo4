namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

using FluentValidation;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class CreateUpdateBudgetDTOValidator : AbstractValidator<CreateUpdateBudgetDto>
{
    public CreateUpdateBudgetDTOValidator()
    {
        RuleFor(x => x.BudgetAmount)
            .GreaterThan(0).WithMessage("Budget amount must be greater than zero.");

        RuleFor(x => x.Month)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Month cannot be in the future.");
    }
}

