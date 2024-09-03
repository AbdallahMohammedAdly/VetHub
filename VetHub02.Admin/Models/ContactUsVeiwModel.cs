using VetHub02.Core.Entities;

namespace VetHub02.Admin.Models
{
    public class ContactUsVeiwModel
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime DateOfCreate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
