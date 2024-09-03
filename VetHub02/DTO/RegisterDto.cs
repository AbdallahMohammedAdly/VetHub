using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VetHub02.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = null!;
  
        [Required]
        [EmailAddress]
        public string Email { get; set; } =null!;

        [Required]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password Not Matched , check it Again!")]
        public string RePassword { get; set; } = null!;
    }
}
