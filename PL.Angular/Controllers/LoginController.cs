using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserLog(model.Email, model.Password);
                if (user != null)
                {
                    //await Authenticate(user);
                    //return RedirectToAction("Index", "Home");
                    return Ok(model);
                }
                ModelState.AddModelError("", "Incrorrect login and(or) password");
            }
            return Ok(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("signin", "Login");
        }
    }
}
