using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class Join : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public string Specialization { get; set; } = string.Empty;

        [NotMapped]
        public List<IFormFile> CV { get; set; } = new List<IFormFile>();
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public DateTime DateTime { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
