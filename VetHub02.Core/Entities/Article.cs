using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class Article : BaseEntity
    {
        
        public string Title { get; set; } = string.Empty; 

        public string Description { get; set; } = string.Empty; 

        public string Content { get; set; } = string.Empty; 

        public string? ImageUrl { get; set; }


        public DateTime TimeOfArticle { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public int UserId { get; set; }
        public User User { get; set; } 

    }
}
