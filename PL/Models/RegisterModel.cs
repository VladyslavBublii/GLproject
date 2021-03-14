using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is not specified")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password was not repeat correctly")]
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string City { get; set; }

        public string PostIndex { get; set; }
    }
}
