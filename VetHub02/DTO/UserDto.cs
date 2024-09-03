using Twilio.Jwt.AccessToken;

namespace VetHub02.DTO
{
    public class UserDto
    {
        public string? DisplayName { get; set; }
        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Token { get; set; }
    }
}
