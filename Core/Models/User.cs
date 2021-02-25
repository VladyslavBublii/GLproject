using System;

namespace Core.Models
{
    public class User
    {
        public Guid Id { set; get; }

        public int Role { set; get; }

        public string Email { set; get; } 

        public string Password { set; get; }
    }
}
