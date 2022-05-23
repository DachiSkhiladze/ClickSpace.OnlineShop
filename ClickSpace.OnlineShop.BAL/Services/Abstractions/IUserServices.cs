using ClickSpace.DataAccess.Database;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.OnlineShop.BAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services.Abstractions
{
    public interface IUserServices : IBaseService<APIUser, UserModel>
    {
        Task<SignInResult> Login(UserModel model);
        Task<IdentityResult> Register(UserModel model);
    }
}
