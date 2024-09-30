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
    private readonly Guid _currentUser;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
        _currentUser = UserIdClaimer.GetCurrentUserId(User);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO categoryDTO)
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var category = _mapper.Map<Category>(categoryDTO);
        category.UserId = _currentUser;

        await _categoryService.AddAsync(category);

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, categoryDTO);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(Guid categoryId)
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var category = await _categoryService.GetByIdAsync(categoryId, _currentUser);

        if(category == null) return NotFound();	

        await _categoryService.DeleteAsync(categoryId, _currentUser);
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id, _currentUser);
        if(category == null) return NotFound();

        return Ok(category);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserCategories()
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var categories = await _categoryService.GetUserCategories(_currentUser);
        return Ok(categories);
    }

    [HttpGet]
    public async Task<IActionResult> GetDefaultCategories()
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var defaultCategories = await _categoryService.GetUserCategories(_currentUser);
        return Ok(defaultCategories);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDTO updateCategoryDTO)
    {
        if (_currentUser == Guid.Empty) return Forbid();

        var category = await _categoryService.GetByIdAsync(id, _currentUser);

        if(category == null) return NotFound();

        category.Name = updateCategoryDTO.Name;

        await _categoryService.UpdateAsync(category, _currentUser);
        return Ok();
    }
}
