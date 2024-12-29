using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using BL.Services.Interfaces;
using Core.Extantion;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request body.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }

            try
            {
                var user = await _userService.GetUserLogAsync(
                    model.Email,
                    model.PasswordCache,
                    model.UserRole.ParseStringToRole()
                );

                if (user != null)
                {
                    model.Id = user.Id.ToString();
                    model.UserRole = user.UserRole.ParseRoleToString();
                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return Unauthorized("Incorrect login and/or password.");
        }
    }
}