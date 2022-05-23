using ClickSpace.DataAccess.Database;
using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using ClickSpace.OnlineShop.BAL.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        IUserServices userServices;
        private readonly UserManager<APIUser> _userManager;
        private readonly IAuthManager _authManager;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserServices userServices,
                                UserManager<APIUser> userManager,
                                ILogger<UserController> logger,
                                IAuthManager authManager)
        {
            this.userServices = userServices;
            _userManager = userManager;
            _authManager = authManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserModel request)
        {
            request.Roles = new List<string>()
            {
                "User"
            };
            _logger.LogInformation($"Registration Attempt for {request.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await userServices.Register(request);

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
        public async Task<IActionResult> Login([FromBody] UserLoginModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _authManager.ValidateUser(request))
            {
                return Unauthorized();
            }

            return Accepted(new { Token = await _authManager.CreateToken() });
        }
    }
}
