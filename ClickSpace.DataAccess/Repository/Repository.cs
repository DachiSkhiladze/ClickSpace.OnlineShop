using ClickSpace.DataAccess.DB.Database;
using System.Linq.Expressions;

namespace ClickSpace.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly OnlineshopContext onlineshopDBContext;

        public Repository(OnlineshopContext clickspaceOnlineshopContext)
        {
            onlineshopDBContext = clickspaceOnlineshopContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return onlineshopDBContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not be returned: {ex.Message}");
            }
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile()).AsQueryable();
        }

        public async Task<TEntity> AddAsync(TEntity entity) 
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await onlineshopDBContext.AddAsync(entity);
                await onlineshopDBContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.InnerException}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                onlineshopDBContext.Update(entity);
                await onlineshopDBContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                onlineshopDBContext.Remove(entity);
                await onlineshopDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }
    }
}
