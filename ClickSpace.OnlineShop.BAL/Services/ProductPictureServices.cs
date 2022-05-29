using AutoMapper;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;

namespace ClickSpace.OnlineShop.BAL.Services
{
    public class ProductPictureServices : BaseService<ProductPicture, PictureModel>, IProductPictureServices
    {
        private static Random random = new Random();
        protected IMapper Mapper;

        public async Task<ProductPicture> InsertAsync(long productId, string name)
        {
                var dto = new ProductPicture
                {
                    IsMainPicture = false,
                    ProductId = productId,
                    PictureUrl = name,
                };
                return await Repository.AddAsync(dto);
        }

        public async Task DeleteByNameAsync(string name)
        {
            var dto = this.Repository.Get(o => o.PictureUrl.Equals(name)).FirstOrDefault();
            if(dto == null)
            {
                return;
            }
            await this.Repository.DeleteAsync(dto);
        }

        public IQueryable<ProductPicture> GetPicturesByProductID(long productID)
        {
            return this.Repository.Get(o => o.ProductId.Equals(productID));
        }

        public ProductPictureServices(IRepository<ProductPicture> repository, IMapper mapper) : base(repository)
        {
            Mapper = mapper;
        }

        private string GetPhotoType(string[] arr)
        {
            var type = arr[arr.Length - 1];
            return type;
        }

        public string GetNameForNewPicture(string currentName)
        {
            var type = GetPhotoType(currentName.Split('.'));
            var length = 30;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray()) + "." + type;
        }

        protected override ProductPicture ConvertToDTO(PictureModel model)
        {
            throw new NotImplementedException();
        }

        protected override PictureModel ConvertToModel(ProductPicture entity)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<PictureModel> ConvertToModels(IQueryable<ProductPicture> entities)
        {
            throw new NotImplementedException();
        }
    }
}
