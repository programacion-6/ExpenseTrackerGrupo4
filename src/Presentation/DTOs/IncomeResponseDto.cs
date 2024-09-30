public class IncomeResponseDto
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; set; }
}
