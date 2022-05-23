using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    public class CartProductController : ControllerBase
    {
        ICartProductServices CartProductServices;
        public CartProductController(ICartProductServices cartProductServices)
        {
            CartProductServices = cartProductServices;
        }

        [Authorize]
        [Route("AddInCart")]
        [HttpPost]
        public async Task<CartProductModel> AddInCart(string userId, long productId)
        {
            var UserID = GetCurrentUserID();
            var res = await CartProductServices.AddInCart(UserID, productId);
            return res;
        }

        [Authorize(Roles = "User")]
        [Route("DeacreaseInQuantityCart")]
        [HttpPost]
        public async Task<CartProductModel> Deacrease(long Id, string userId)
        {
            var UserID = GetCurrentUserID();
            var res = await CartProductServices.DecreaseFromCart(Id, UserID);
            return res;
        }

        [Authorize(Roles = "User")]
        [Route("DeleteFromCart")]
        [HttpPost]
        public async Task DeleteFromCart(long Id, string userId)
        {
            var UserID = GetCurrentUserID();
            await CartProductServices.DeleteFromCart(Id, UserID);
        }

        [Authorize(Roles = "User")]
        private string GetCurrentUserID()
        {
            var id = (HttpContext.User.Identity as ClaimsIdentity).Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier).Value;
            return id;
        }
    }
}
