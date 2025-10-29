using Microsoft.AspNetCore.Mvc;
using OyoAgro.Api.Authorizations;
using OyoAgro.BusinessLogic.Layer.Interfaces;
using OyoAgro.BusinessLogic.Layer.Services;
using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] string userName, [FromQuery] string password)
        {
            var response = await _userService.CheckLogin(userName, password);

            return Ok(new { success = true, Data = response });
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> LoginOff()
        {
            try
            {
                var response = new TData<string>();
                var token = string.Empty;
                var header = (string)HttpContext.Request.Headers["Authorization"]!;
                if (header != null) token = header.Substring(7);
                var user = await _userService.GetUserByToken(token);

                if (user.Data != null)
                {
                    await _userService.Logout(user.Data.Userid);
                    return Ok(new { success = true, Data = "User logged out successfully"  });

                }
                else
                {
                    return Ok(new { success = false, Data = "User not recognized" });
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserParam model)
        {
            var response = await _userService.SaveForm(model);

            if (response.Tag == 1)
            {
                return Ok(new { success = true, Data = response });

            }
            else
            {
                return Ok(new { success = false, Data = response });
            }
        }

        [Authorize]
        [HttpGet("GetOfficers")]
        public async Task<IActionResult> GetList()
        {
            var response = await _userService.GetList();


            return Ok(new { success = true, Data = response });

        }

        [Authorize]
        [HttpGet("GetOfficer/{userId}")]
        public async Task<IActionResult> GetOfficer(int userId) 
        {
            var response = await _userService.GetOfficer(userId);


            return Ok(new { success = true, Data = response });

        }




    }
}
