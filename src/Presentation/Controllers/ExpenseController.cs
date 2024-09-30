using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[ApiController]
[Route("api/expenses")]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;
    private readonly IMapper _mapper;
    private readonly Guid _currentUser;

    public ExpenseController(IExpenseService expenseService, IMapper mapper)
    {
        _expenseService = expenseService;
        _mapper = mapper;
        _currentUser = UserIdClaimer.GetCurrentUserId(User);
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateUpdateExpenseDto dto)
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var expense = _mapper.Map<Expense>(dto);
        expense.UserId = _currentUser;

        await _expenseService.AddAsync(expense);
        return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserExpenses(
        [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate, [FromQuery] string? category
    )
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var expenses = await _expenseService.GetUserExpensesCommand(_currentUser, startDate, endDate, category);

        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetExpenseById(Guid id)
    {
        var expense = await _expenseService.GetByIdAsync(id, _currentUser);
        if (expense == null) return NotFound();
        return Ok(expense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateExpense(Guid id, [FromBody] CreateUpdateExpenseDto dto)
    {
        var existingExpense = await _expenseService.GetByIdAsync(id, _currentUser);

        if (existingExpense == null) return NotFound();

        existingExpense.Amount = dto.Amount ?? existingExpense.Amount;
        existingExpense.Description = dto.Description ?? existingExpense.Description;
        existingExpense.Category = dto.Category ?? existingExpense.Category;
        existingExpense.Date = dto.Date ?? existingExpense.Date;

        await _expenseService.UpdateAsync(existingExpense, _currentUser);
        return Ok(existingExpense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var existingExpense = await _expenseService.GetByIdAsync(id, _currentUser);

        if (existingExpense == null) return NotFound();

        await _expenseService.DeleteAsync(id, _currentUser);
        return Ok();
    }
}
