using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerGrupo4.src.Domain.Entities;
using ExpenseTrackerGrupo4.src.Infrastructure.Interfaces;
using ExpenseTrackerGrupo4.src.Application.DTOs;
using AutoMapper;
using ExpenseTrackerGrupo4.src.Utils;

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

            var currentUser = UserIdClaimer.GetCurrentUserId(User);

            if (currentUser == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var income = _mapper.Map<Income>(incomeDto);
            income.UserId = currentUser;

            await _incomeService.AddAsync(income);
            return CreatedAtAction(nameof(GetIncomeById), new { id = income.Id }, incomeDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncomeResponseDto>>> GetIncomes()
        {
            var currentUser = UserIdClaimer.GetCurrentUserId(User);
            if (currentUser == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

            var incomes = await _incomeService.GetIncomesByUserId(currentUser);
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
            var currentUser = UserIdClaimer.GetCurrentUserId(User);
            var income = await _incomeService.GetByIdAsync(id, currentUser);
            if (income == null)
            {
                return NotFound();
            }

            if (currentUser == Guid.Empty)
            {
                return Unauthorized("User ID is not valid or missing.");
            }

    
            if (income.UserId != currentUser)
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
            
            var currentUser = UserIdClaimer.GetCurrentUserId(User);

            var existingIncome = await _incomeService.GetByIdAsync(id, currentUser);
            if (existingIncome == null)
            {
                return NotFound();
            }
            
            if (existingIncome.UserId != currentUser)
            {
                return Forbid();
            }

            existingIncome.Amount = updatedIncomeDto.Amount;
            existingIncome.Source = updatedIncomeDto.Source;
            existingIncome.Date = updatedIncomeDto.Date;

            await _incomeService.UpdateAsync(existingIncome, currentUser);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(Guid id)
        {
            var currentUser = UserIdClaimer.GetCurrentUserId(User);
            
            var income = await _incomeService.GetByIdAsync(id, currentUser);
            if (income == null)
            {
                return NotFound();
            }

            if (income.UserId != currentUser)
            {
                return Forbid();
            }

            await _incomeService.DeleteAsync(id, currentUser);
            return NoContent();
        }
    }
}
