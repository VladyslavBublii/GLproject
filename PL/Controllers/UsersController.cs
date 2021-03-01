using BL.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using BL.Services.Interfaces;
using PL.Models;
using System.Collections.Generic;
using AutoMapper;
using Core.Enums;

namespace PL.Controllers
{
    public class UsersController : Controller
    {
        IUserService _userService;
        IEmailService _emailService;
        IPasswordService _passwordService;

        public UsersController(IUserService serv, IEmailService emailService, IPasswordService passwordService)
        {
            _userService = serv;
            _emailService = emailService;
            _passwordService = passwordService;
        }

        public ActionResult Index()
        {
            try
            {
               IEnumerable<CustomerDTO> customerDtos = _userService.GetCustomers();

                var mapperCustomer = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>()).CreateMapper();

                var customer = mapperCustomer.Map<IEnumerable<CustomerDTO>, List<CustomerViewModel>>(customerDtos);

                return View(customer);
            }
            catch (Exception ex)
            {
                return View();
            }
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
                if (!_emailService.ValideEmail(userdetails.Email))
                {
                    throw new Exception("Invalide Email");
                }
                if(_passwordService.PasswordStrength(userdetails.Password) < PassStrength.Medium)
                {
                    throw new Exception("Pass not strong enough");
                }

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
