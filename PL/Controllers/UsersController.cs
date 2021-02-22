using BL.DTO;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services.Interfaces;
using PL.Models;
using AutoMapper;

namespace PL.Controllers
{
    public class UsersController : Controller
    {
        IUserService _userService;

        public UsersController(IUserService serv)
        {
            _userService = serv;
        }

        // GET: Teachers
        //public async Task<IActionResult> Index()
        public ActionResult Index()
        {
            //IEnumerable<CustomerDTO> customerDtos = _userService.GetCustomers();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>()).CreateMapper();
            //var customers = mapper.Map<IEnumerable<CustomerDTO>, List<CustomerViewModel>>(customerDtos);

            //return View(customers);
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            //try
            //{
            //    CustomerDTO customerDto = _userService.GetCustomer(id);
            //    var user = new UserViewModel { };

            //    return View(user);
            //}
            //catch (Exception ex)
            //{
            //    return Content(ex.Message);
            //}
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(UserViewModel user, CustomerViewModel customer)
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
                //return Content(ex.Message);
            }

            return View(user);
        }
    }
}
