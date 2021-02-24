using BL.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using BL.Services.Interfaces;
using PL.Models;
using System.Collections.Generic;
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

        public ActionResult Index()
        {
            IEnumerable<CustomerDTO> customerDtos = _userService.GetCustomers();

            var mapperCustomer = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>()).CreateMapper();

            var customer = mapperCustomer.Map<IEnumerable<CustomerDTO>, List<CustomerViewModel>>(customerDtos);

            return View(customer);      
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserDetailsViewModel userdetails)
        {
            try
            {
                var userDto = new UserDTO { Email = userdetails.Email, Password = userdetails.Password };
                var customerDto = new CustomerDTO { Name = userdetails.Name, SurName = userdetails.SurName, City = userdetails.City, PostIndex = userdetails.PostIndex };
                _userService.SaveUser(userDto, customerDto);
                return View(userdetails);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
