using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;

namespace BL.Services
{
    public class UserService : IUserService
    {
        public  IUnitOfWork _unitOfWork;

        public IPasswordService _password;

        public IEmailService _email;

        public UserService()
        {    
            _unitOfWork = new UnitOfWork();
            _password = new PasswordService();
            _email = new EmailService();
        }

        public UserDTO GetUser(Guid id)
        {
            var user = _unitOfWork.Users.Get(id);

            return new UserDTO { Email = user.Email, Password = user.Password };
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(_unitOfWork.Users.GetAll());
        }

        public CustomerDTO GetCustomer(Guid id)
        {
            var customer = _unitOfWork.Customers.Get(id);

            return new CustomerDTO { Name = customer.Name, SurName = customer.SurName, 
                City = customer.SurName, PostIndex = customer.PostIndex };
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(_unitOfWork.Customers.GetAll());
        }

        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO)
        {
            if (!_email.ValideEmail(userDTO.Email))
            {
                throw new Exception("Invalide Email");
            }
            if (_password.PasswordStrength(userDTO.Password) < PassStrength.Medium)
            {
                throw new Exception("Pass not strong enough");
            }

            User user = new User
            {
                Role     = 0,
                Email    = userDTO.Email,
                Password = _password.GetHashString(userDTO.Password),
            };

            Customer customer = new Customer
            {
                Name      = customerDTO.Name,
                SurName   = customerDTO.SurName,
                City      = customerDTO.City,
                PostIndex = customerDTO.PostIndex,
                User      = user             
            };

            user.Customer = customer;

            _unitOfWork.Users.Create(user);
            _unitOfWork.Customers.Create(customer);

            _unitOfWork.Save();
        }
    }
}
