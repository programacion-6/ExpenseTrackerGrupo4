namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class CreateUpdateExpenseDto
{
    public decimal? Amount { get; set; } 
    public string? Description { get; set; } 
    public Guid? CategoryId { get; set; } 
    public DateTime? Date { get; set; } 
}

