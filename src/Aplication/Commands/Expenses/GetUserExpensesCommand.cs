using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetUserExpensesCommand(
    IExpenseRepository expenseRepository, 
    Guid userId, 
    DateTime? startDate, 
    DateTime? endDate, 
    Guid? categoryId
) : ICommand<Task<List<Expense>>>
{
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly Guid _userId = userId;
    private readonly DateTime? _startDate = startDate;
    private readonly DateTime? _endDate = endDate;
    private readonly Guid? _categoryId = categoryId;

    public async Task<List<Expense>> Execute()
    {
        return await _expenseRepository.GetUserExpensesAsync(_userId, _startDate, _endDate, _categoryId);
    }
}
