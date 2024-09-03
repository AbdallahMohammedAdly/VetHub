using System.ComponentModel.DataAnnotations.Schema;

namespace VetHub02.Admin.Models
{
    public class JoinViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public string Specialization { get; set; }
        public IList<IFormFile> CV { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateTime { get; set; } 
    }
}
