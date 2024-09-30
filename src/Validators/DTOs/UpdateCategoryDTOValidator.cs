using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using FluentValidation;

namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

public class UpdateCategoryDTOValidator : AbstractValidator<UpdateCategoryDTO>
{
    public UpdateCategoryDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
    }
}

