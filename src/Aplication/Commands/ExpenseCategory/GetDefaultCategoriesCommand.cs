using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetDefaultCategoriesCommand(ICategoryRepository categoryRepository) : ICommand<Task<IEnumerable<Category>>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<Category>> Execute()
    {
        return await _categoryRepository.GetDefaultCategories();
    }
}