using System.ComponentModel.DataAnnotations;

namespace PL.Angular.Models
{
    public class LoginModel
    {
        public required string Id { set; get; }
        
        [Required(ErrorMessage = "Email is not specified")]
        public required string Email { get; set; }
         
        [Required(ErrorMessage = "Password is not specified")]
        [DataType(DataType.Password)]
        public required string PasswordCache { get; set; }

        [Required(ErrorMessage = "User role is not specified")]
        public required string UserRole { get; set; }
    }
}