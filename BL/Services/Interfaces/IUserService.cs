using BL.DTO;
using System.Collections.Generic;


namespace BL.Services.Interfaces
{
    public interface IUserService
    {
        CustomerDTO GetCustomer(int? id);
        IEnumerable<CustomerDTO> GetCustomers();
        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO);
    }
}
