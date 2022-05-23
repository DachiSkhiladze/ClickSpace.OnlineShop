using ClickSpace.DataAccess.Database;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Services;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using ClickSpace.OnlineShop.BAL.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ClickSpace.OnlineShop.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<APIUser>(q => q.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<OnlineshopContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureServicesInjections(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Product>, Repository<Product>>();
            services.AddTransient<IRepository<APIUser>, Repository<APIUser>>();
            services.AddTransient<IRepository<CartProduct>, Repository<CartProduct>>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<ICartProductServices, CartProductServices>(); 
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IAuthManager, AuthManager>();


            services.AddScoped<ProductServices>();
            services.AddScoped<UserManager<APIUser>>();
            services.AddScoped<SignInManager<APIUser>>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JWT");
            var key = configuration.GetSection("JWT").GetSection("Key");

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key.Value))
                    };
                });
        }
    }
}
