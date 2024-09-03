using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetHub02.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string Title { get; set; } = string.Empty; 

        public string Content { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        
        public int? ArticleId { get; set; }

        public virtual Article? Article { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; } 

       public int? QuestionId { get; set; }

       public virtual Question? question { get; set; } 

    }
}
