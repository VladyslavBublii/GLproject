using BL.DTO;
using Microsoft.AspNetCore.Mvc;
using BL.Services.Interfaces;
using PL.Models;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace PL.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class UsersController : Controller
    {
        IUserService _userService;
        IPasswordService _password;
        IEmailService _email;
        IRoleService _role;

        public UsersController(IUserService serv, IPasswordService passwordService, IEmailService emailService, IRoleService roleService)
        {
            _userService = serv;
            _password = passwordService;
            _email = emailService;
            _role = roleService;
        }

        
        public ActionResult Index()
        {
            IEnumerable<CustomerDTO> customerDtos = _userService.GetCustomers();
            //IEnumerable<UserDTO> userDtos = _userService.GetUsers();

            var mapperCustomer = new MapperConfiguration(cfg => cfg.CreateMap<CustomerDTO, CustomerViewModel>()).CreateMapper();
            //var mapperUserDetails = new MapperConfiguration(cfg => {
            //    cfg.CreateMap<CustomerDTO, UserDetailsViewModel>()
            //    .ForMember(destination => destination.ContactDetails,
            //   opts => opts.MapFrom(source => source.Contact));
            //});

            var customer = mapperCustomer.Map<IEnumerable<CustomerDTO>, List<CustomerViewModel>>(customerDtos);

            if (customer != null)
            {
                return View(customer);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult Create()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Create(UserDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool existErrors = false;
                //if (model.Email == null || model.Password == null || model.RoleName == null || model.Name == null || model.SurName == null)
                //{
                //    ModelState.AddModelError("", "All fields are suppose to be filled");
                //    return View(model);
                //}
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
                //if (model.RoleName != "user" && model.RoleName != "admin")
                if(!_role.IsAdmin(model.RoleName) && !_role.IsUser(model.RoleName))
                {
                    ModelState.AddModelError("", "There are only this roles: 'user' and 'admin'");
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
                    return View(model);
                }

                var userDto = new UserDTO { Email = model.Email, Password = model.Password, RoleName = model.RoleName };
                var customerDto = new CustomerDTO { Name = model.Name, SurName = model.SurName, City = model.City, PostIndex = model.PostIndex };
                _userService.SaveUser(userDto, customerDto);

                TempData["message"] = string.Format("New user \"{0}\" \"{1}\" with email \"{2}\" has been saved", model.Name, model.SurName, model.Email);

                return View(model);
            }
            return View(model);
            
        }
    }
}
