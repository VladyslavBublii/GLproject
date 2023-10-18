using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using BL.Services.Interfaces;
using BL.DTO;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("registration")]
    public class RegistrationController : ControllerBase
    {
        IUserService _userService;
        IPasswordService _passwordService;
        IEmailService _emailService;

        public RegistrationController(IUserService user, IPasswordService password, IEmailService email) 
        {
            _userService = user;
            _passwordService = password;
            _emailService = email;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                bool existErrors = false;
                if (!_userService.IsEmailFree(registerModel.Email))
                {
                    ModelState.AddModelError("", "This email is already busy");
                    existErrors = true;
                }
                if (!_emailService.ValideEmail(registerModel.Email))
                {
                    ModelState.AddModelError("", "This email is not valid");
                    existErrors = true;
                }
                if (!_passwordService.IsPasswordStrong(registerModel.Password))
                {
                    ModelState.AddModelError("", "Password is too weak");
                    existErrors = true;
                }

                if (existErrors)
                {
                    return Ok(registerModel);
                }

                var userDto = new UserDTO { Email = registerModel.Email, Password = registerModel.Password, RoleName = "user" };
                var customerDto = new CustomerDTO { Name = registerModel.Name, SurName = registerModel.SurName, City = registerModel.City, PostIndex = registerModel.PostIndex };
                _userService.SaveUser(userDto, customerDto);

                var user = _userService.GetUserLog(registerModel.Email, registerModel.Password);
            }
            return Ok(registerModel);
        }
    }
}
