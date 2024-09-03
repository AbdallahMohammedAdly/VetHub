using VetHub02.Core.Entities;

namespace VetHub02.DTO
{
    public class CommentDto
    {

        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public UserDto User { get; set; } = null!;

    }
}
