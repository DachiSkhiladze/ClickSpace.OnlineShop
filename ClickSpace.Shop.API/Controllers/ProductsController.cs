using Microsoft.AspNetCore.Mvc;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ProductsController : ControllerBase
    {
        IProductServices Service;
        public ProductsController(IProductServices Service)
        {
            this.Service = Service;
        }

        [Route("GetByID")]
        [HttpGet]
        public ProductModel Get(int ID)
        {
            var res = Service.GetProductByID(ID);
            return res;
        }

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

        [Route("GetProductsByTitle")]
        [HttpGet]
        public IEnumerable<ProductModel> GetByTitle(string title="")
        {
            var res = Service.GetProductsByTitle(title);
            return res;
        }

        [Route("UpdateProduct")]
        [HttpPost]
        public async Task<ProductModel> UpdateProduct(ProductModel model)
        {
            var res = await Service.UpdateAsync(model);
            return res;
        }

        [Route("InsertProduct")]
        [HttpPost]
        public async Task<ProductModel> InsertProduct(ProductModel model)
        {
            var res = await Service.InsertAsync(model);
            return res;
        }

        [Route("DeleteProduct")]
        [HttpPost]
        public async Task DeleteProduct(ProductModel model)
        {
            await Service.InsertAsync(model);
        }
    }
}