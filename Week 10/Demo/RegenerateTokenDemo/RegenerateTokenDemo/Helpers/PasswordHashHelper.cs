using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RegenerateTokenDemo.Helpers
{
    public static class PasswordHashHelper
    {
        public static string Hash(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}