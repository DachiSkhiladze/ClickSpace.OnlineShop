using ClickSpace.DataAccess.DB.Database;
using ClickSpace.OnlineShop.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services.Abstractions
{
    public interface IProductServices : IBaseService<Product, ProductModel>
    {
        IEnumerable<ProductModel> GetProductsSorted(int quantity, int skip, string order = "Asc", string priceOrder = "Default");
        IEnumerable<ProductModel> GetProductsByTitle(string s="");
        ProductModel GetProductByID(long id);
    }
}
