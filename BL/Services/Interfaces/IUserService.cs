﻿using BL.DTO;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(Guid id);
        IEnumerable<UserDTO> GetUsers();
        CustomerDTO GetCustomer(Guid id);
        IEnumerable<CustomerDTO> GetCustomers();
        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO);

        //Task<ClaimsIdentity> Authenticate(UserDTO userDto);
    }
}
