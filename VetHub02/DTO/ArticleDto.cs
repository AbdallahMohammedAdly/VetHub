using Org.BouncyCastle.Bcpg;
using VetHub02.Core.Entities;

namespace VetHub02.DTO
{
    public class ArticleDto 
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;  

        public string Description { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string ImageUrl { get; set; }

        public DateTime TimeOfArticle { get;} = DateTime.Now;

        public int UserId { get; set; }

        //public UserDto User { get; set; }

       // public ICollection<CommentDto>? comments { get; set; } 
    }
}
