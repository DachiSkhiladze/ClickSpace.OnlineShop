using AutoMapper;
using ClickSpace.DataAccess.Database;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.OnlineShop.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<APIUser, UserModel>();
            CreateMap<UserModel, APIUser>();
            CreateMap<CartProduct, CartProductModel>();
            CreateMap<CartProductModel, CartProduct>();
        }
    }
}
