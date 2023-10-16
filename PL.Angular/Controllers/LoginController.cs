using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PL.Angular.Models;
using BL.Services.Interfaces;
using BL.DTO;
using System.Security.Claims;

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

        [HttpGet("signin")]
        public IActionResult SignIn()
        {
            return Ok(200);
        }

        [HttpPost("signin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
             if (ModelState.IsValid)
            {
                var user = _userService.GetUserLog(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    //return RedirectToAction("Index", "Home");
                    return Ok(model);
                }
                ModelState.AddModelError("", "Incrorrect login and(or) password");
            }
            return Ok(model);
        }

        private async Task Authenticate(UserDTO user)
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultNameClaimType, Convert.ToString(user.Id)),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("signin", "Login");
        }
    }
}
