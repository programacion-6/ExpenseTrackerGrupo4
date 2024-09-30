using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class Category : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public Guid? ParentId { get; set; }

    public Category()
    {}

    public Category(string name)
    {
        Name = name;
    }
}
