using BL.DTO;
using Core.Models;
using DAL;
using System;

namespace BL
{
    public class UserBL
    {
        public  UnitOfWork _unitOfWork;

        public UserBL()
        {   
            _unitOfWork = new UnitOfWork();
        }
         
        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO)
        {
            User user = new User
            {
                Role = 0,
                Email = userDTO.Email,
                Password = userDTO.Password,
            };

            _unitOfWork.Users.Create(user);

            Customer customer = new Customer
            {
                Name = customerDTO.Name,
                SurName = customerDTO.SurName,
                City = customerDTO.City,
                PostIndex = customerDTO.PostIndex
            };

            _unitOfWork.Customers.Create(customer);

            _unitOfWork.Save();
        }
    }
}
