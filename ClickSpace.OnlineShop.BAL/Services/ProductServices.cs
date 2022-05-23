using AutoMapper;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;

namespace ClickSpace.OnlineShop.BAL.Services
{
    public class ProductServices : BaseService<Product, ProductModel>, IProductServices
    {
        protected IMapper Mapper;
        public ProductServices(IRepository<Product> repository, IMapper mapper) : base(repository)
        {
            this.Mapper = mapper;   // Initializing AutoMapper Using InBuilt DI Service Container
        }

        //Private Methods
        private IQueryable<Product> SortByPrice(string priceOrder = "Default")
        {
            IQueryable<Product> products = this.Repository.GetAll();
            if (priceOrder == "HF")
            {
                return (from o in products
                            orderby o.Price descending
                            select o);
            }
            else if(priceOrder == "LF")
            {
                return (from o in products
                        orderby o.Price ascending
                        select o);
            }
            else
            {
                return products;
            }
        }

        // Public Methods
        public IEnumerable<ProductModel> GetProductsSorted(int quantity, int skip, string order = "Asc", string priceOrder = "Default")
        {
            IQueryable<Product> products = SortByPrice(priceOrder);
            if (order == "Asc")
            {
                products = (from o in products
                            orderby o.Id ascending
                            select o);
            }
            else
            {
                products = (from o in products
                            orderby o.Id descending
                            select o);
            }
            products = products.Skip(skip).Take(quantity);
            return this.ConvertToModels(products);
        }

        public IEnumerable<ProductModel> GetProductsByTitle(string s="") => this.ConvertToModels(this.Repository.Get(o => o.Title.Contains(s)).AsQueryable());

        public ProductModel GetProductByID(long id)
        {
            IQueryable<Product> products = this.Repository.Get(o => o.Id == id).AsQueryable();
            Product product = products.First();
            ProductModel productModel = this.ConvertToModel(product);
            return productModel;
        }

        //Protected Methods
        protected override Product ConvertToDTO(ProductModel model) => Mapper.Map<Product>(model);

        protected override ProductModel ConvertToModel(Product entity) => Mapper.Map<ProductModel>(entity);

        protected override IEnumerable<ProductModel> ConvertToModels(IQueryable<Product> entities)
        {
            foreach (var item in entities)
            {
                yield return Mapper.Map<ProductModel>(item);
            }
        }
    }
}
