using BL.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using BL.Services.Interfaces;
using PL.Models;

namespace PL.Controllers
{
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService serv)
        {
            _userService = serv;
        }

        // GET: Get Users
        public ActionResult Index()
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        public ActionResult Create(UserViewModel user, CustomerViewModel customer)
        {
            try
            {
                var userDto = new UserDTO { Email = user.Email, Password = user.Password };
                //var customerDto = new CustomerDTO { Name = customer.Name, SurName = customer.SurName, City = customer.City, PostIndex = customer.PostIndex };
                var customerDto = new CustomerDTO { Name = "Alexander", SurName = "Trunov", City = "Kharkov", PostIndex = "PI123456" };
                _userService.SaveUser(userDto, customerDto);
                return Content("Юзер и Кастомер успешно добвалены.");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return View(user);
        }
    }
}
