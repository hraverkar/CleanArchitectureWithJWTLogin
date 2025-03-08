using hr.makemystamp.com.core.Entities;

namespace hr.makemystamp.com.core.Dtos
{
    public record TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public Role Role { get; set; }
        public User User { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
