using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class GetUserCategoriesCommand(ICategoryRepository categoryRepository, Guid userId) : ICommand<Task<IEnumerable<Category>>>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly Guid _userId = userId;

    public async Task<IEnumerable<Category>> Execute()
    {
        return await _categoryRepository.GetUserCategories( _userId );
    }
}
