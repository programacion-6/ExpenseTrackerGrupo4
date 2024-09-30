using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Utils;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[ApiController]
[Route("api/goals")]
[Authorize]
public class GoalController : ControllerBase
{
    private readonly IGoalService _goalService;
    private readonly IMapper _mapper;
    private readonly Guid _currentUser;

    public GoalController(IGoalService goalService, IMapper mapper)
    {
        _goalService = goalService;
        _mapper = mapper;
        _currentUser = UserIdClaimer.GetCurrentUserId(User);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateUpdateGoalDto dto)
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var goal = _mapper.Map<Goal>(dto);
        goal.UserId = _currentUser;

        await _goalService.AddAsync(goal);
        return CreatedAtAction(nameof(GetUserGoals), new { id = goal.Id }, dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserGoals()
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var goalsWithDetails = await _goalService.GetGoalsWithDetailsAsync(_currentUser);

        return Ok(goalsWithDetails);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] CreateUpdateGoalDto dto)
    {
        var existingGoal = await _goalService.GetByIdAsync(id, _currentUser);

        if (existingGoal == null) return NotFound();

        existingGoal.GoalAmount = dto.GoalAmount;
        existingGoal.Deadline = dto.Deadline;
        existingGoal.CurrentAmount = dto.CurrentAmount;

        await _goalService.UpdateAsync(existingGoal, _currentUser);
        return Ok(existingGoal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var existingGoal = await _goalService.GetByIdAsync(id, _currentUser);

        if (existingGoal == null) return NotFound();

        await _goalService.DeleteAsync(id, _currentUser);
        return NoContent();
    }
}
