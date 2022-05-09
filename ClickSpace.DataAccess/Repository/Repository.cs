using ClickSpace.DataAccess.Database;
namespace ClickSpace.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ClickspaceOnlineshopContext onlineshopDBContext;

        public Repository(ClickspaceOnlineshopContext clickspaceOnlineshopContext)
        {
            onlineshopDBContext = clickspaceOnlineshopContext;
        }

        public List<TEntity> GetAll()
        {
                var res = onlineshopDBContext.Set<TEntity>();
                return res.ToList();
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
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
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
