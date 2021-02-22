using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System.Collections.Generic;

namespace BL.Services
{
    public class UserService : IUserService
    {
        public  IUnitOfWork _unitOfWork;

        public UserService()
        {   
            _unitOfWork = new UnitOfWork();
        }

        public CustomerDTO GetCustomer(int? id)
        {
            //if (id == null)
              //  throw new ValidationException("Не установлено id товара", "");
            var customer = _unitOfWork.Customers.Get(id.Value);
            //if (customer == null)
              //  throw new ValidationException("Товар не найден", "");

            return new CustomerDTO { Name = customer.Name, SurName = customer.SurName, City = customer.SurName, PostIndex = customer.PostIndex };
        }

        public IEnumerable<CustomerDTO> GetCustomers()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Customer>, List<CustomerDTO>>(_unitOfWork.Customers.GetAll());
        }

        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO)
        {
            User user = new User
            {
                Role = 0,
                Email = userDTO.Email,
                Password = userDTO.Password
            };

            _unitOfWork.Users.Create(user);

            Customer customer = new Customer
            {
                Name = customerDTO.Name,
                SurName = customerDTO.SurName,
                City = customerDTO.City,
                PostIndex = customerDTO.PostIndex,
            };

            _unitOfWork.Customers.Create(customer);

            _unitOfWork.Save();
        }
    }
}
