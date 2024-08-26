using Core.Enums;
using System;

namespace BL.DTO
{
    public class UserDTO
    {
        public Guid Id { set; get; }

        public Role UserRole { set; get; }

        public string Email { set; get; }

        public string Password { set; get; }
    }
}
