﻿using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using BL.Services.Interfaces;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        IUserService _userService;

        public LoginController(IUserService serv)
        {
            _userService = serv;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserLog(model.Email, model.PasswordCache);
                if (user != null)
                {
                    model.Id = user.Id.ToString();
                    model.UserRole = user.RoleName.ToString();
                    return Ok(model);
                }
            }
            return BadRequest("Incorrect login and(or) password");
        }
    }
}