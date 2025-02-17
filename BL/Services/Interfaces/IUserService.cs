using BL.DTO;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Core.Enums;

namespace BL.Services.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetUserAsync(Guid id);

    Task<IEnumerable<UserDTO>> GetUsersAsync();

    Task<CustomerDTO> GetCustomerAsync(Guid id);

    Task<CustomerDTO> GetCustomerByUserIdAsync(Guid userId);

    Task<IEnumerable<CustomerDTO>> GetCustomersAsync();

    Task SaveUserAsync(UserDTO userDTO, CustomerDTO customerDTO);

    Task<bool> IsPasswordSameAsync(string password);

    Task<bool> IsEmailFreeAsync(string email);

    Task<UserDTO> GetUserLogAsync(string email, string password, Role userRole);
}