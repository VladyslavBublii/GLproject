using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
    public class UserDetailsViewModel
    {
        public string Email { set; get; }

        public string Password { set; get; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string City { get; set; }

        public string PostIndex { get; set; }
    }
}
