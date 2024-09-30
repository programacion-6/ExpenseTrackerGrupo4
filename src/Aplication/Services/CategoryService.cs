using ExpenseTrackerGrupo4.src.Aplication.Commands;
using ExpenseTrackerGrupo4.src.Aplication.Commands.Expenses;
using ExpenseTrackerGrupo4.src.Aplication.Interfaces;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Aplication.Services;

public class CategoryService(ICategoryRepository categoryRepository, CommandInvoker commandInvoker) : BaseService(commandInvoker), ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task AddAsync(Category entity)
    {
        var command = new AddCategoryCommand(_categoryRepository, entity);
        await CommandInvoker.Execute(command);
    }

    public async Task DeleteAsync(Guid categoryId, Guid userId)
    {
        var command = new DeleteCategoryCommand(_categoryRepository, categoryId, userId);
        await CommandInvoker.Execute(command);
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId, Guid userId)
    {
        var command = new GetCategoryByIdCommand(_categoryRepository, categoryId, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task<IEnumerable<Category>> GetDefaultCategories()
    {
        var command = new GetDefaultCategoriesCommand(_categoryRepository);
        return await CommandInvoker.Execute(command);
    }

    public async Task<IEnumerable<Category>> GetUserCategories(Guid userId)
    {
        var command = new GetUserCategoriesCommand(_categoryRepository, userId);
        return await CommandInvoker.Execute(command);
    }

    public async Task UpdateAsync(Category category, Guid userId)
    {
        var command = new UpdateCategoryCommand(_categoryRepository, category, userId);
        await CommandInvoker.Execute(command);
    }
}
