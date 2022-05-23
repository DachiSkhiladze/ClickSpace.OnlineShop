using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    public class CartProductController : ControllerBase
    {
        ICartProductServices CartProductServices;
        public CartProductController(ICartProductServices cartProductServices)
        {
            CartProductServices = cartProductServices;
        }

        [Route("AddInCart")]
        [HttpPost]
        public async Task<CartProductModel> AddInCart(string userId, long productId)
        {
            var res = await CartProductServices.AddInCart(userId, productId);
            return res;
        }

        [Route("DeacreaseInQuantityCart")]
        [HttpPost]
        public async Task<CartProductModel> Deacrease(long Id, string userId)
        {
            var res = await CartProductServices.DecreaseFromCart(Id, userId);
            return res;
        }

        [Route("DeleteFromCart")]
        [HttpPost]
        public async Task DeleteFromCart(long Id, string userId)
        {
            await CartProductServices.DeleteFromCart(Id, userId);
        }
    }
}
