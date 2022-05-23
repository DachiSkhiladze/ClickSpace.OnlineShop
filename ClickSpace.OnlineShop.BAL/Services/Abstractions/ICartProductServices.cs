using ClickSpace.DataAccess.DB.Database;
using ClickSpace.OnlineShop.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services.Abstractions
{
    public interface ICartProductServices : IBaseService<CartProduct, CartProductModel>
    {
        Task<CartProductModel> AddInCart(string userId, long productId);

        Task DeleteFromCart(long id, string userId);

        Task<CartProductModel> DecreaseFromCart(long id, string userId);
    }
}
