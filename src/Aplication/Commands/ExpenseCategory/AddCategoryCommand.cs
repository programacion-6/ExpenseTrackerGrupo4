using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class AddCategoryCommand(ICategoryRepository categoryRepository, Category category) : ICommand<Task>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly Category _category = category;

    public async Task Execute()
    {
        await _categoryRepository.AddAsync( _category );
    }
}
