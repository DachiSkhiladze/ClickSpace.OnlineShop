using ClickSpace.DataAccess.DB.Database;
using ClickSpace.OnlineShop.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services.Abstractions
{
    public interface IProductPictureServices : IBaseService<ProductPicture, PictureModel>
    {
        string GetNameForNewPicture(string currentName);
        Task<ProductPicture> InsertAsync(long productId, string name);
        IQueryable<ProductPicture> GetPicturesByProductID(long productID);
        Task DeleteByNameAsync(string name);
    }
}
