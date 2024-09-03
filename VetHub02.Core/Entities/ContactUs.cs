using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class ContactUs : BaseEntity
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime DateOfCreate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
