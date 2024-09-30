using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[ApiController]
[Route("api/budgets")]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IBudgetService _budgetService;
    private readonly IMapper _mapper;

    public BudgetController(IBudgetService budgetService, IMapper mapper)
    {
        _budgetService = budgetService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBudget([FromBody] CreateUpdateBudgetDto dto)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        if (currentUser == Guid.Empty) return Forbid();

        var budget = _mapper.Map<Budget>(dto);
        budget.UserId = currentUser;

        await _budgetService.AddAsync(budget);
        return CreatedAtAction(nameof(GetUserBudgets), new { id = budget.Id }, dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserBudgets()
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        if (currentUser == Guid.Empty) return Forbid();

        var budgetsWithExpenses = await _budgetService.GetBudgetsAsync(currentUser);

        return Ok(budgetsWithExpenses);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] CreateUpdateBudgetDto dto)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        var existingBudget = await _budgetService.GetByIdAsync(id, currentUser);

        if (existingBudget == null) return NotFound();

        existingBudget.Month = dto.Month;
        existingBudget.BudgetAmount = dto.BudgetAmount;

        await _budgetService.UpdateAsync(existingBudget, currentUser);
        return Ok(existingBudget);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBudget(Guid id)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        var existingBudget = await _budgetService.GetByIdAsync(id, currentUser);

        if (existingBudget == null) return NotFound();

        await _budgetService.DeleteAsync(id, currentUser);
        return NoContent();
    }
}
