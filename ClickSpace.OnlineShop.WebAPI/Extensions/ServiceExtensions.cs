using ClickSpace.DataAccess.Database;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using ClickSpace.OnlineShop.BAL.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ClickSpace.OnlineShop.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDBContext(this IServiceCollection services)
        {
            services.AddDbContext<OnlineshopContext>(
                  x => x.UseSqlServer("Data Source=localhost;Initial Catalog=ClickSpace.ShopOnline;Integrated Security=True")); // Adding DB Context Injection
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<APIUser>(q => q.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<OnlineshopContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureServicesInjections(this IServiceCollection services)
        {
            services.AddScoped<ProductServices>();
            services.AddScoped<UserManager<APIUser>>();

            services.AddTransient<IRepository<ProductPicture>, Repository<ProductPicture>>();
            services.AddTransient<IRepository<Product>, Repository<Product>>();
            services.AddTransient<IRepository<APIUser>, Repository<APIUser>>();
            services.AddTransient<IRepository<CartProduct>, Repository<CartProduct>>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<ICartProductServices, CartProductServices>();
            services.AddTransient<IProductPictureServices, ProductPictureServices>();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IAuthManager, AuthManager>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT");
            var key = jwtSettings.GetSection("Key").Value;

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.GetSection("Key").Value))
                };
            });
        }
    }
}
