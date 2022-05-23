using AutoMapper;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services
{
    public class CartProductServices : BaseService<CartProduct, CartProductModel>, ICartProductServices
    {
        protected IMapper Mapper;
        public CartProductServices(IRepository<CartProduct> repository, IMapper mapper) : base(repository)
        {
            this.Mapper = mapper;   // Initializing AutoMapper Using InBuilt DI Service Container
        }

        //Private Methods
        private CartProduct get(string userId, long productId)
        {
            return Repository.Get(o => (o.UserId == userId) && (o.ProductId == productId)).FirstOrDefault(); // Possible null reference return.
        }

        //Public Methods
        public async Task<CartProductModel> AddInCart(string userId, long productId)
        {
            CartProduct cartProduct = get(userId, productId);
            if (cartProduct != null)
            {
                if (cartProduct.Quantity <= 300)
                {
                    cartProduct.Quantity++;
                    return ConvertToModel(await this.Repository.UpdateAsync(cartProduct));
                }
                return ConvertToModel(cartProduct);
            }
            else
            {
                return await this.InsertAsync(
                    new CartProductModel() {
                        ProductId = productId,
                        UserId = userId,
                        Quantity = 1
                    });
            }
        }

        public async Task<CartProductModel> DecreaseFromCart(long productId, string userId)
        {
            CartProduct cartProduct = this.Repository.Get(o => o.UserId == userId && o.ProductId == productId).First();

            if (cartProduct.Quantity > 1)
            {
                cartProduct.Quantity--;
                return ConvertToModel(await this.Repository.UpdateAsync(cartProduct));
            }
            return ConvertToModel(cartProduct);
        }

        public async Task DeleteFromCart(long productId, string userId)
        {
            CartProduct cartProduct = this.Repository.Get(o => o.UserId == userId && o.ProductId == productId).First();

            await this.Repository.DeleteAsync(cartProduct);
        }

        //Protected Methods
        protected override CartProduct ConvertToDTO(CartProductModel model) => Mapper.Map<CartProduct>(model);

        protected override CartProductModel ConvertToModel(CartProduct entity) => Mapper.Map<CartProductModel>(entity);

        protected override IEnumerable<CartProductModel> ConvertToModels(IQueryable<CartProduct> entities)
        {
            foreach (var item in entities)
            {
                yield return Mapper.Map<CartProductModel>(item);
            }
        }
    }
}
