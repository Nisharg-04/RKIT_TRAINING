
namespace AuthDemo.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; } // plain for demo only
        public string Role { get; set; } // Admin / User
    }
}