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

        public CustomerDTO GetCustomer(int? id)
        {
            var customer = _unitOfWork.Customers.Get(id.Value);

            return new CustomerDTO { Name = customer.Name, SurName = customer.SurName, City = customer.SurName, PostIndex = customer.PostIndex };
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(_unitOfWork.Customers.GetAll());
        }

        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO)
        {
            // Валидация,если не нужна - закоментируйте 
            if (!_email.ValideEmail(userDTO.Email))
            {
                throw new Exception("Invalide Email");
            }
            if (_password.PasswordStrength(userDTO.Email) > Strength.Medium)
            {
                throw new Exception("Pass not strong enough");
            }

            User user = new User
            {
                Role     = 0,
                Email    = userDTO.Email,
                Password = _password.GetHashString(userDTO.Password),
            };

            _unitOfWork.Users.Create(user);

            Customer customer = new Customer
            {
                Name      = customerDTO.Name,
                SurName   = customerDTO.SurName,
                City      = customerDTO.City,
                PostIndex = customerDTO.PostIndex
            };

            _unitOfWork.Customers.Create(customer);

            _unitOfWork.Save();
        }
    }
}
