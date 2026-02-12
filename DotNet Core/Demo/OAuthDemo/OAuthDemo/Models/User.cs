namespace OAuthDemo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string GoogleId { get; set; }
        public string TwoFactorSecret { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
