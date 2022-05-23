using ClickSpace.DataAccess.Database;
using ClickSpace.OnlineShop.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClickSpace.OnlineShop.BAL.Services.Auth
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(UserLoginModel user);
        Task<string> CreateToken();
        Task<APIUser> GetUser(ClaimsPrincipal user);
    }
}
