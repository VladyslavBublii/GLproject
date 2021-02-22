using BL.DTO;
using BL.Services.Interfaces;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;

namespace BL.Services
{
    public class UserService : IUserService
    {
        public  IUnitOfWork _unitOfWork;

        public UserService()
        {   
            _unitOfWork = new UnitOfWork();
        }
         
        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO)
        {
            User user = new User
            {
                Role     = 0,
                Email    = userDTO.Email,
                Password = userDTO.Password,
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
