﻿using Microsoft.AspNetCore.Mvc;
using PL.Angular.Models;
using BL.Services.Interfaces;
using BL.DTO;

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
                var user = _userService.GetUserLog(model.Email, model.Password);
                if (user != null)
                {
                    return Ok(model);
                }
            }
            return BadRequest("Incorrect login and(or) password");
        }
    }
}
