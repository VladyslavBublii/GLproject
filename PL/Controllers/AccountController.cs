using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using PL.Models;
using BL.Services.Interfaces;
using BL.DTO;
using AutoMapper;
using BL.Services;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        //!!!!!!
        public IPasswordService _password;
        //!!!!!

        public AccountController(IUserService serv)
        {
            _userService = serv;
            //!!!!!!
            _password = new PasswordService();
            //!!!!!
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<UserDTO> userDtos = _userService.GetUsers();

                foreach(UserDTO userDto in userDtos)
                {
                    if (userDto.Email == model.Email && userDto.Password == _password.GetHashString(model.Password))
                    {
                        await Authenticate(userDto); // аутентификация
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Incrorrect login and(or) password");
                //var mapperUser = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserViewModel>()).CreateMapper();

                //var users = mapperUser.Map<IEnumerable<UserDTO>, List<UserViewModel>>(userDtos);

                //if (user != null)
                //{
                //    await Authenticate(model.Email); // аутентификация

                //    return RedirectToAction("Index", "Home");
                //}
                //ModelState.AddModelError("", "Incrorrect login and(or) password");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<UserDTO> userDtos = _userService.GetUsers();

                UserDTO user = null;
                foreach (UserDTO userDto in userDtos)
                {
                    if (userDto.Email == model.Email)
                    {
                        user = userDto;
                        break;
                    }
                }
                //User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    //db.Users.Add(new User { Email = model.Email, Password = model.Password });
                    //await db.SaveChangesAsync();

                    //await Authenticate(model.Email); // аутентификация
                    var userDto = new UserDTO { Email = model.Email, Password = model.Password, RoleName = "user" };
                    var customerDto = new CustomerDTO { Name = model.Name, SurName = model.SurName, City = model.City, PostIndex = model.PostIndex };
                    _userService.SaveUser(userDto, customerDto);

                    await Authenticate(userDto);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incrorrect login and(or) password");
                }
            }
            return View(model);
        }

        private async Task Authenticate(UserDTO user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
