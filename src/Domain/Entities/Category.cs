using ExpenseTrackerGrupo4.src.Domain.Interfaces;

namespace ExpenseTrackerGrupo4.src.Domain.Entities;

public class Category : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; } = [];

    public Category(string name)
    {
        Name = name;
    }

    public Category(string name, Category parent)
    {
        Name = name;
        Parent = parent;
        ParentId = parent.Id;
    }
}
