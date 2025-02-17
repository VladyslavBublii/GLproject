using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using BL.Services.Interfaces;
using BL.DTO;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("register")]
    public class RegistrationController(
        IUserService userService,
        IPasswordService passwordService,
        IEmailService emailService)
        : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state.");
            }

            var errorList = new List<string>();

            if (!await userService.IsEmailFreeAsync(registerModel.Email))
            {
                errorList.Add("This email is already in use.");
            }

            if (!emailService.ValidateEmail(registerModel.Email))
            {
                errorList.Add("This email is not valid.");
            }

            if (!passwordService.IsPasswordStrong(registerModel.Password))
            {
                errorList.Add("Password is too weak.");
            }

            if (errorList.Count != 0)
            {
                return BadRequest(new { Errors = errorList });
            }

            try
            {
                var userDto = new UserDTO
                {
                    Email = registerModel.Email,
                    Password = registerModel.Password,
                    UserRole = Core.Enums.Role.User
                };

                var customerDto = new CustomerDTO
                {
                    Name = registerModel.Name,
                    SurName = registerModel.SurName,
                    City = registerModel.City,
                    PostIndex = registerModel.PostIndex
                };

                await userService.SaveUserAsync(userDto, customerDto);

                var user = await userService.GetUserLogAsync(registerModel.Email, registerModel.Password, Core.Enums.Role.User);

                return Ok(new
                {
                    Message = "Registration successful.",
                    UserId = user.Id,
                    Email = user.Email,
                    Role = user.UserRole
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred during registration: {ex.Message}");
            }
        }
    }
}
