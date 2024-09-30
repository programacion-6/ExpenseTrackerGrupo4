namespace ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;

public class DeleteCategoryCommand(ICategoryRepository categoryRepository, Guid categoryId, Guid authenticatedUserId) : ICommand<Task>
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly Guid _categoryId = categoryId;
    private readonly Guid _authenticatedUserId = authenticatedUserId;

    public async Task Execute()
    {
        var category = await _categoryRepository.GetByIdAsync(_categoryId);

        if (category == null || category.UserId != _authenticatedUserId)
        {
            throw new UnauthorizedAccessException("Acces deniend.");
        }

        await _categoryRepository.DeleteAsync( _categoryId );
    }
}
