using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetHub02.DTO
{
    public class JoinDto
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool Gender { get; set; } 
        public string Specialization { get; set; } = string.Empty;
        [NotMapped]
        public List<IFormFile> CV { get; set; } = new List<IFormFile>();
        public DateTime DateOfBirth { get; } = DateTime.Now;
        public DateTime DateTime { get;} = DateTime.Now;
        public int? UserId { get; set; }
    }
}
