using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using BL.Services.Interfaces;
using BL.DTO;
using static Azure.Core.HttpHeader;

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
                var errorList = new List<string>();
                if (!_userService.IsEmailFree(registerModel.Email))
                {
                    errorList.Add("This email is already busy!");
                }
                if (!_emailService.ValideEmail(registerModel.Email))
                {
                    errorList.Add("This email is not valid!");
                }
                if (!_passwordService.IsPasswordStrong(registerModel.Password))
                {
                    errorList.Add("Password is too weak!");
                }

                if (errorList.Count != 0)
                {
                    return BadRequest("Errors: " + String.Join(" ", errorList.ToArray()));
                }

                var userDto = new UserDTO { Email = registerModel.Email, Password = registerModel.Password, UserRole = Core.Enums.Role.User };
                var customerDto = new CustomerDTO { Name = registerModel.Name, SurName = registerModel.SurName, City = registerModel.City, PostIndex = registerModel.PostIndex };
                _userService.SaveUser(userDto, customerDto);

                var user = _userService.GetUserLog(registerModel.Email, registerModel.Password, Core.Enums.Role.User);
            }
            return Ok(registerModel);
        }
    }
}
