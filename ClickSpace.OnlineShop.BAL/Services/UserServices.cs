using AutoMapper;
using ClickSpace.DataAccess.Database;
using ClickSpace.DataAccess.DB.Database;
using ClickSpace.DataAccess.Repository;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace ClickSpace.OnlineShop.BAL.Services
{
    public class UserServices : BaseService<APIUser, UserModel>, IUserServices
    {
        protected IMapper Mapper;
        private readonly UserManager<APIUser> _userManager;
        public UserServices(IRepository<APIUser> repository, IMapper mapper, UserManager<APIUser> userManager) : base(repository)
        {
            this.Mapper = mapper;   // Initializing AutoMapper Using InBuilt DI Service Container
            _userManager = userManager;
        }

        //Private Methods
        private string CreateToken(APIUser user)
        {
            List<Claim> claims = new List<Claim>();
            {
                new Claim(ClaimTypes.Email, user.Email);
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("AVASCAS$E@(!#@(SA(#(@#@#$!("));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPassword(APIUser user, string password)
        {
            return user.PasswordHash == password;
        }

        //Public Methods
        public async Task<SignInResult> Login(UserModel model)
        {
            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            //return result;
            return new SignInResult();
        }

        public async Task<IdentityResult> Register(UserModel model)
        {
            var user = ConvertToDTO(model);
            user.UserName = model.Email;
            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, model.Roles);
            }
            return result;
        }

        //Protected Methods
        protected override APIUser ConvertToDTO(UserModel model) => Mapper.Map<APIUser>(model);

        protected override UserModel ConvertToModel(APIUser entity) => Mapper.Map<UserModel>(entity);

        protected override IEnumerable<UserModel> ConvertToModels(IQueryable<APIUser> entities)
        {
            throw new NotImplementedException();
        }
    }
}
