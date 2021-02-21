using BL.DTO;


namespace BL.Services.Interfaces
{
    interface IUserService
    {
        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO);
    }
}
