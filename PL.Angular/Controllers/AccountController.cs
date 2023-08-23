using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("account")]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost("login/{model}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            return Ok(model);
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost("register/{model}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool existErrors = false;
                if (existErrors)
                {
                    return new JsonResult(model);
                }
                return RedirectToAction("Index", "Home");
            }
            return Ok(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
