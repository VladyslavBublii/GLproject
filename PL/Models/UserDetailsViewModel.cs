using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class UserDetailsViewModel
    {
        [Required(ErrorMessage = "Role is not specified")]
        public string RoleName { set; get; }

        [Required(ErrorMessage = "Email is not specified")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Password is not specified")]
        public string Password { set; get; }

        [Compare("Password", ErrorMessage = "Password was not repeat correctly")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Name is not specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is not specified")]
        public string SurName { get; set; }

        public string City { get; set; }

        public string PostIndex { get; set; }
    }
}
