using VetHub02.Core.Entities;

namespace VetHub02.DTO
{
    public class ContactUsDto
    {
        public int? Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime DateOfCreate { get; } = DateTime.Now;

        public int? UserId { get; set; }
    }
}
