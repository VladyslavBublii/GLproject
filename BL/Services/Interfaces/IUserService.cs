using BL.DTO;
using System.Collections.Generic;
using System;
using Core.Enums;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(Guid id);

        IEnumerable<UserDTO> GetUsers();

        CustomerDTO GetCustomer(Guid id);

        CustomerDTO GetCustomerByUserId(Guid userId);

        IEnumerable<CustomerDTO> GetCustomers();

        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO);

        bool IsPasswordSame(string password);

        bool IsEmailFree(string email);

        UserDTO GetUserLog(string email, string password, Role UserRole);
    }
}
