using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegenerateTokenDemo.Models
{
    public class RefreshTokens
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public string TokenHash { get; set; }
        public string DeviceId { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public string ReplacedByTokenHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}