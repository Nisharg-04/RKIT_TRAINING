using AuthDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthDemo.Services
{
    public static class UserStore
    {
        private static readonly List<UserModel> _users = new List<UserModel>();

        static UserStore()
        {
          
            _users.Add(new UserModel
            {
                Username = "admin",
                Password = "admin123",
                Role = "Admin"
            });
        }

        public static bool UserExists(string username)
        {
            return _users.Any(u => u.Username == username);
        }

        public static void AddUser(UserModel user)
        {
            _users.Add(user);
        }

        public static UserModel ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);
        }
    }
}