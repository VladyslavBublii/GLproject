using System.ComponentModel.DataAnnotations;

namespace PL.Angular.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is not specified")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is not specified")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Name is not specified")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Surname is not specified")]
        public required string SurName { get; set; }

        public required string City { get; set; }

        public required string PostIndex { get; set; }
    }
}
