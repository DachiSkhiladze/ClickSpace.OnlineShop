using ClickSpace.DataAccess.Database;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserServices services;
        private readonly UserManager<APIUser> _userManager;
        private readonly SignInManager<APIUser> _signInManager;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserServices userServices,
                                UserManager<APIUser> userManager,
                                SignInManager<APIUser> signInManager,
                                ILogger<UserController> logger)
        {
            services = userServices;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserModel request)
        {
            _logger.LogInformation($"Registration Attempt for {request.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await services.Register(request);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            }
            return Ok();
        }

        [HttpPost]
        [Route("LogIn")]
        public async Task<ActionResult<string>> Login([FromBody] UserModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await services.Login(request);
                if (!result.Succeeded)
                {
                    return Unauthorized(ModelState);
                }
                return Accepted();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
