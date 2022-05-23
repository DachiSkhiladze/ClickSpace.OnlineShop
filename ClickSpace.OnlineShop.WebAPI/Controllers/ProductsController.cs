using Microsoft.AspNetCore.Mvc;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ClickSpace.OnlineShop.BAL.Services.Auth;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProductsController : ControllerBase
    {
        IProductServices Service;
        IAuthManager AuthManager;
        public ProductsController(IProductServices Service, IAuthManager authManager)
        {
            this.Service = Service;
            this.AuthManager = authManager;
        }

        [Route("GetByID")]
        [HttpGet]
        public ProductModel Get(int ID)
        {
            var res = Service.GetProductByID(ID);
            return res;
        }

        [Authorize]
        [Route("GetAllProducts")]
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            var res = Service.GetModels();
            return res;
        }

        [Route("GetProductsByFilter")]
        [HttpGet]
        public IEnumerable<ProductModel> Get(int quantity, int skip, string order = "Desc", string priceOrder = "Default")
        {
            var res = Service.GetProductsSorted(quantity, skip, order, priceOrder);
            return res;
        }

        [Authorize(Roles = "Administrator")]
        [Route("GetProductsByTitle")]
        [HttpGet]
        public IEnumerable<ProductModel> GetByTitle(string title="")
        {
            var res = Service.GetProductsByTitle(title);
            return res;
        }

        [Authorize(Roles = "Administrator")]
        [Route("UpdateProduct")]
        [HttpPost]
        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var res = await Service.UpdateAsync(model);
            return res;
        }

        [Authorize(Roles = "Administrator")]
        [Route("InsertProduct")]
        [HttpPost]
        public async Task<ProductModel> InsertProduct(ProductModel model)
        {
            var res = await Service.InsertAsync(model);
            return res;
        }

        [Authorize(Roles = "Administrator")]
        [Route("DeleteProduct")]
        [HttpPost]
        public async Task DeleteProduct(ProductModel model)
        {
            await Service.InsertAsync(model);
        }
    }
}