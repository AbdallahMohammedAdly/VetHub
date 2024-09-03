using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class User : BaseEntity 
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public ICollection<Article> Articles { get; set; } = new HashSet<Article>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Question> Questions { get; set; } = new HashSet<Question>();

      
    }
}
