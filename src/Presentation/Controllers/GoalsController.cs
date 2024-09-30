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

    public GoalController(IGoalService goalService, IMapper mapper)
    {
        _goalService = goalService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] CreateUpdateGoalDto dto)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        if (currentUser == Guid.Empty) return Forbid();

        var goal = _mapper.Map<Goal>(dto);
        goal.UserId = currentUser;

        await _goalService.AddAsync(goal);
        return CreatedAtAction(nameof(GetUserGoals), new { id = goal.Id }, dto);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserGoals()
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        if (currentUser == Guid.Empty) return Forbid();

        var goalsWithDetails = await _goalService.GetGoalsWithDetailsAsync(currentUser);

        return Ok(goalsWithDetails);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] CreateUpdateGoalDto dto)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        var existingGoal = await _goalService.GetByIdAsync(id, currentUser);

        if (existingGoal == null) return NotFound();

        existingGoal.GoalAmount = dto.GoalAmount;
        existingGoal.Deadline = dto.Deadline;
        existingGoal.CurrentAmount = dto.CurrentAmount;

        await _goalService.UpdateAsync(existingGoal, currentUser);
        return Ok(existingGoal);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(Guid id)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        var existingGoal = await _goalService.GetByIdAsync(id, currentUser);

        if (existingGoal == null) return NotFound();

        await _goalService.DeleteAsync(id, currentUser);
        return NoContent();
    }
}
