using System.Linq.Expressions;

namespace SomeStoreAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);  // Get an entity by ID
        Task<IEnumerable<T>> GetAllAsync();  // Get all entities
        Task AddAsync(T entity);  // Add a new entity

        Task UpsertRangeAsync(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);  // Update an entity
        Task DeleteAsync(int id);  // Delete an entity by ID

        // Find entities with custom queries
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);  // You can also use Expression<Func<T, bool>>

    }

}
