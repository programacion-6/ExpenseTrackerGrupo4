using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Interfaces;

 public interface ICategoryService : IService<Category>
{
    Task<IEnumerable<Category>> GetUserCategories(Guid userId);
    Task<IEnumerable<Category>> GetDefaultCategories();
}
