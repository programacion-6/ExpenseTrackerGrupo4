using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Presentation.DTOs;

public class CreateCategoryDTO
{
    public required string Name { get; set; }
    public required Guid? ParentId { get; set; }
}
