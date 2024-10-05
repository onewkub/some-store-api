using Microsoft.EntityFrameworkCore;
using SomeStoreAPI.Data;
using SomeStoreAPI.Models;
using System.Linq.Expressions;

namespace SomeStoreAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();  // Get the corresponding DbSet for the entity
        }

        // Get entity by ID
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Get all entities
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Add a new entity
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Update an entity
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Delete an entity by ID
        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Find entities by a query (predicate)
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsQueryable().Where(predicate).AsQueryable();
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        // Implementing UpsertRangeAsync
        public async Task UpsertRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                var keyValue = GetKeyValue(entity);
                var keyPropertyName = GetKeyPropertyName(entity);
                // Check if the entity already exists in the database
                var exists = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => EF.Property<object>(e, keyPropertyName).Equals(keyValue));

                if (exists != null)
                {
                    // If entity exists, update it
                    _dbSet.Update(entity);
                }
                else
                {
                    // If entity doesn't exist, add it
                    await _dbSet.AddAsync(entity);
                }
            
            }

            // Save all changes (both inserts and updates)
            await _context.SaveChangesAsync();
        }

        private object GetKeyValue(T entity)
        {
            var keyPropertyName = GetKeyPropertyName(entity);
            return entity.GetType().GetProperty(keyPropertyName)?.GetValue(entity, null);
        }

        private string GetKeyPropertyName(T entity)
        {
            // Retrieve the key name from the entity type
            var key = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey();
            return key.Properties.Select(x => x.Name).Single();
        }
    }

}
