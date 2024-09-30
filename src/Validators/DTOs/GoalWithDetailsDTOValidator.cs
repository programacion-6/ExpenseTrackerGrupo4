namespace ExpenseTrackerGrupo4.src.Validators.DTOs;

using FluentValidation;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class GoalWithDetailsDTOValidator : AbstractValidator<GoalWithDetailsDto>
{
    public GoalWithDetailsDTOValidator()
    {
        RuleFor(x => x.GoalAmount)
            .GreaterThan(0).WithMessage("Goal amount must be greater than zero.");

        RuleFor(x => x.CurrentAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Current amount cannot be negative.");

        RuleFor(x => x.TotalIncome)
            .GreaterThanOrEqualTo(0).WithMessage("Total income cannot be negative.");

        RuleFor(x => x.TotalExpenses)
            .GreaterThanOrEqualTo(0).WithMessage("Total expenses cannot be negative.");

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.Now).WithMessage("Deadline must be in the future.");
    }
}

