using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
namespace RegenerateTokenDemo.Helpers
{
   

    public static class RefreshTokenHelper
    {
        public static string Generate()
        {
            var bytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return Convert.ToBase64String(bytes);
        }

        public static string Hash(string token)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(token);
                return Convert.ToBase64String(sha.ComputeHash(bytes));
            }
        }
    }

}