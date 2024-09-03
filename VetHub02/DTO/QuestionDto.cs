using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace VetHub02.DTO
{
    public class QuestionDto
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public UserDto User { get; set; } = null!;
    }
}
