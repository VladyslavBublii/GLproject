using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        public LoginController()
        {
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
            return Ok(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("signin", "Login");
        }
    }
}
