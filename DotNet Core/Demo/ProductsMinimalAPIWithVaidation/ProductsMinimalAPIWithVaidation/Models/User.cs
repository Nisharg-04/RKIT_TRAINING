using System.ComponentModel.DataAnnotations;

namespace ProductsMinimalAPIWithVaidation.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
