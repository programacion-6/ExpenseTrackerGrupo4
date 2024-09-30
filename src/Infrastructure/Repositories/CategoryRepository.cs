using System.Data;
using Dapper;
using ExpenseTrackerGrupo4.src.Domain.Entities;

namespace ExpenseTrackerGrupo4.src.Infrastructure.Repositories;

public class CategoryRepository(IDbConnection dbConnection) : ICategoryRepository
{
    private readonly IDbConnection _dbConnection = dbConnection;

    public async Task AddAsync(Category entity)
    {
        var query = @"
                    INSERT INTO Categories (Id, UserId, Name, ParentId)
                    VALUES (@Id, @UserId, @Name, @ParentId)";
                    
        await _dbConnection.ExecuteAsync(query, entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = "DELETE FROM Categories WHERE Id = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        var query = "SELECT * FROM Categories WHERE Id = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<Category>(query, new { Id = id });
    }

    public async Task<IEnumerable<Category>> GetDefaultCategories()
    {
        var query = "SELECT * FROM GlobalCategories";
        return await _dbConnection.QueryAsync<Category>(query);
    }

    public async Task UpdateAsync(Category entity)
    {
        var sql = @"
            UPDATE Categories 
            SET Name = @Name
            WHERE Id = @Id AND UserId = @UserId";
        
        await _dbConnection.ExecuteAsync(sql, entity);
    }

    public async Task<IEnumerable<Category>> GetUserCategories(Guid userId)
    {
        var query = "SELECT * FROM Categories WHERE UserId = @UserId::uuid";
        return await _dbConnection.QueryAsync<Category>(query, new { UserId = userId });
    }
}
