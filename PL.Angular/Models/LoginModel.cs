﻿using System.ComponentModel.DataAnnotations;

namespace PL.Angular.Models
{
    public class LoginModel
    {
        public string Id { set; get; }
        
        [Required(ErrorMessage = "Email is not specified")]
        public string Email { get; set; }
         
        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public string PasswordCache { get; set; }

        [Required(ErrorMessage = "User role is not specified")]
        public string UserRole { get; set; }
    }
}