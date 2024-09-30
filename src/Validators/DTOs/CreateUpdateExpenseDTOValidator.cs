using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators;

public class CreateUpdateExpenseDTOValidator : AbstractValidator<CreateUpdateExpenseDto>
{
    public CreateUpdateExpenseDTOValidator()
    {
        RuleFor(e => e.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");
        
        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
        
        RuleFor(x => x.CategoryId)
            .NotNull().WithMessage("Category ID is required.");
        
        RuleFor(e => e.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future.");
    }
}

