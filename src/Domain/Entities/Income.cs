using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public record Income : IEntity
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public DateTime Date { get; set; }
    public DateTime CreatedAt { get; init; }
}
