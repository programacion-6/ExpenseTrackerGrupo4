namespace ExpenseTrackerGrupo4.src.Application.DTOs
{
    public class IncomeDto
    {
        public decimal Amount { get; set; }
        public required string Source { get; set; }
        public DateTime Date { get; set; }
    }
}
