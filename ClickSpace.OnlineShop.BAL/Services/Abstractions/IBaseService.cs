using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services.Abstractions
{
    public interface IBaseService<TEntity, TModel>
    {
        IEnumerable<TModel> GetModels(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TModel> GetModels();
        Task<TModel> InsertAsync(TModel model);
        Task<TModel> UpdateAsync(TModel model);
        Task DeleteAsync(TModel model);
    }
}
