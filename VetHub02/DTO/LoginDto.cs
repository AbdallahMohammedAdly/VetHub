using System.ComponentModel.DataAnnotations;

namespace VetHub02.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
