using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Application.DTOs;
using AutoMapper;

namespace ExpenseTrackerGrupo4.src.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IMapper _mapper;

        public IncomeController(IIncomeService incomeService, IMapper mapper)
        {
            _incomeService = incomeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody] IncomeDto incomeDto)
        {
            if (incomeDto == null)
            {
                return BadRequest("Income data is required.");
            }

            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var income = _mapper.Map<Income>(incomeDto);

            income = income with
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            await _incomeService.AddIncomeAsync(income);
            return CreatedAtAction(nameof(GetIncomeById), new { id = income.Id }, incomeDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeResponseDto>>> GetIncomes()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var incomes = await _incomeService.GetIncomesByUserIdAsync(userId);
            if (incomes == null || !incomes.Any())
            {
                return Ok(new List<IncomeResponseDto>());
            }

            var incomeDtos = _mapper.Map<IEnumerable<IncomeResponseDto>>(incomes);

            return Ok(incomeDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IncomeResponseDto>> GetIncomeById(Guid id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            if (income.UserId != userId)
            {
                return Forbid();
            }

            var incomeDto = _mapper.Map<IncomeResponseDto>(income);

            return Ok(incomeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(Guid id, [FromBody] IncomeDto updatedIncomeDto)
        {
            if (updatedIncomeDto == null)
            {
                return BadRequest("Income data is invalid.");
            }

            var existingIncome = await _incomeService.GetIncomeByIdAsync(id);
            if (existingIncome == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            if (existingIncome.UserId != userId)
            {
                return Forbid();
            }

            var updatedIncome = existingIncome with
            {
                Amount = updatedIncomeDto.Amount,
                Source = updatedIncomeDto.Source,
                Date = updatedIncomeDto.Date
            };

            await _incomeService.UpdateIncomeAsync(updatedIncome);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var income = await _incomeService.GetIncomeByIdAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            var userId = GetUserId();
            if (income.UserId != userId)
            {
                return Forbid();
            }

            await _incomeService.DeleteIncomeAsync(id);
            return NoContent();
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : Guid.Empty;
        }
    }
}
