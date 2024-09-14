using Core.Enums;
using System;

namespace Core.Models
{
    public class User
    {
        public Guid Id { set; get; }

        public Role UserRole { set; get; }

        public string Email { set; get; } 

        public string Password { set; get; }

        public Customer Customer { set; get; }
    }
}
