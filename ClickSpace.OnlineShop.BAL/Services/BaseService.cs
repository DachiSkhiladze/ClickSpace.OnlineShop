using AutoMapper;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services
{
    public abstract class BaseService<TEntity, TModel> : IBaseService<TEntity, TModel> where TEntity : class, new() where TModel : class, new()
    {
        protected IRepository<TEntity> Repository;
        public BaseService(IRepository<TEntity> repository)
        {
            Repository = repository;    // Initializing Repository Using InBuilt DI Service Container
        }

        public IEnumerable<TModel> GetModels()
        {
            foreach (var item in Repository.GetAll())
            {
                yield return ConvertToModel(item); // Returning Type Models
            }
        }

        public IEnumerable<TModel> GetModels(Expression<Func<TEntity, bool>> predicate)
        {
            foreach (var item in Repository.Get(predicate))
            {
                yield return ConvertToModel(item); // Returning Type Models
            }
        }

        public async Task<TModel> InsertAsync(TModel model)
        {
            var dto = ConvertToDTO(model);
            return ConvertToModel(await Repository.AddAsync(dto));  // Inserting New Data Into The Table Asynchronously
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            var dto = ConvertToDTO(model);
            return ConvertToModel(await Repository.UpdateAsync(dto)); // Updating Old Data Into The Table Asynchronously
        }

        public async Task DeleteAsync(TModel model)
        {
            var dto = ConvertToDTO(model);
            await Repository.DeleteAsync(dto); // Deleting Old Data From The Table Asynchronously
        }

        protected abstract TEntity ConvertToDTO(TModel model);

        protected abstract TModel ConvertToModel(TEntity entity);

        protected abstract IEnumerable<TModel> ConvertToModels(IQueryable<TEntity> entities);
    }
}
