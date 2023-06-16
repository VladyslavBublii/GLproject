using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BL.Services.Interfaces;
using BL.DTO;
using PL.Angular.Models;

namespace PL.Angular.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        IUserService _userService;
        IPasswordService _password;
        IEmailService _email;

        public AccountController(IUserService serv, IPasswordService password, IEmailService email)
        {
            _userService = serv;
            _password = password;
            _email = email;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUserLog(model.Email, model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incrorrect login and(or) password");
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return Ok();
        }

        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                bool existErrors = false;
                if (!_userService.IsEmailFree(model.Email))
                {
                    ModelState.AddModelError("", "This email is already busy");
                    existErrors = true;
                }
                if (!_email.ValideEmail(model.Email))
                {
                    ModelState.AddModelError("", "This email is not valid");
                    existErrors = true;
                }
                if (!_password.IsPasswordStrong(model.Password))
                {
                    ModelState.AddModelError("", "Password is too weak");
                    existErrors = true;
                }
                //if (model.Password != model.ConfirmPassword)
                //{
                //    ModelState.AddModelError("", "Password is not repeat correctly");
                //    existErrors = true;
                //}

                if (existErrors)
                {
                    return new JsonResult(model);
                }

                var userDto = new UserDTO { Email = model.Email, Password = model.Password, RoleName = "user" };
                var customerDto = new CustomerDTO { Name = model.Name, SurName = model.SurName, City = model.City, PostIndex = model.PostIndex };
                _userService.SaveUser(userDto, customerDto);

                var user = _userService.GetUserLog(model.Email, model.Password);
                //await Authenticate(userDto); //userDto нет ещё правильного id. Куки получат неверный id
                await Authenticate(user);
                return RedirectToAction("Index", "Home");

                //return RedirectToAction("Login", "Account");
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
            return RedirectToAction("Login", "Account");
        }
    }
}
