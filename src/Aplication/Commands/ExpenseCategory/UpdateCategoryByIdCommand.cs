using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class UpdateCategoryCommand(ICategoryRepository categoryRepository, Category category, Guid authenticatedUserId) : ICommand<Task>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly Category _category = category;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        if (_category.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("Acces deniend.");
        }

        await _categoryRepository.UpdateAsync( _category );
    }
}
