using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PL.Controllers
{
    public class HomeController : Controller
    {
        IUserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserService serv)
        {
            _logger = logger;
            _userService = serv;
        }

        //[Authorize]
        public IActionResult Index()
        {
            if (User.Identity.Name != null)
            {
                UserDTO userDtos = _userService.GetUser(new Guid(User.Identity.Name));
                CustomerDTO customerDtos = _userService.GetCustomerByUserId(new Guid(User.Identity.Name));

                var mapperCustomer = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>()).CreateMapper();
                var customer = mapperCustomer.Map<CustomerDTO, CustomerViewModel>(customerDtos);

                return View(customer);
            }
            else
            {
                return View();
            }
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
