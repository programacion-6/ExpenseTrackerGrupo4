using AutoMapper;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Presentation.DTOs;
using ExpenseTrackerGrupo4.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDTO)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);

        if (currentUser == Guid.Empty) return Forbid();

        var category = _mapper.Map<Category>(categoryDTO);
        category.UserId = currentUser;

        await _categoryService.AddAsync(category);

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDTO);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        
        if (currentUser == Guid.Empty) return Forbid();

        var category = await _categoryService.GetByIdAsync(categoryId, currentUser);

        if(category == null) return NotFound();	

        await _categoryService.DeleteAsync(categoryId, currentUser);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        var category = await _categoryService.GetByIdAsync(id, currentUser);
        if(category == null) return NotFound();

        return Ok(category);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserCategories()
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        if (currentUser == Guid.Empty) return Forbid();

        var categories = await _categoryService.GetUserCategories(currentUser);
        return Ok(categories);
    }

    [HttpGet("default")]
    public async Task<IActionResult> GetDefaultCategories()
    {
        var defaultCategories = await _categoryService.GetDefaultCategories();
        return Ok(defaultCategories);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
    {
        var currentUser = UserIdClaimer.GetCurrentUserId(User);
        if (currentUser == Guid.Empty) return Forbid();

        var category = await _categoryService.GetByIdAsync(id, currentUser);

        if(category == null) return NotFound();

        category.Name = updateCategoryDTO.Name;

        await _categoryService.UpdateAsync(category, currentUser);
        return Ok();
    }
}
