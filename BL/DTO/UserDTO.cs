using System;

namespace BL.DTO
{
    public class UserDTO
    {
        public Guid Id { set; get; }

        public string RoleName { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }
    }
}
