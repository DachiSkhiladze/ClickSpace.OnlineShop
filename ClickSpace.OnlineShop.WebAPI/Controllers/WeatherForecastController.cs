using ClickSpace.DataAccess;
using ClickSpace.DataAccess.Database;
using ClickSpace.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class WeatherForecastController : ControllerBase
    {
        IRepository<Product> Repository;
        public WeatherForecastController(IRepository<Product> repository)
        {
            this.Repository = repository;
        }

        [HttpGet(Name = "Get")]
        public List<Product> Get()
        {
            var res = Repository.GetAll();
            return res;
        }
    }
}