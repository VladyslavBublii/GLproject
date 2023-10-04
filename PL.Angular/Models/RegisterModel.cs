using System.ComponentModel.DataAnnotations;

namespace PL.Angular.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is not specified")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Name is not specified")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is not specified")]
        public string SurName { get; set; }

        public string City { get; set; }

        public string PostIndex { get; set; }
    }
}
