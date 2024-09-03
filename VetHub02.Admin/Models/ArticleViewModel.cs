using VetHub02.Core.Entities;

namespace VetHub02.Admin.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }


        public DateTime TimeOfArticle { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public int UserId { get; set; }
       
    }
}
