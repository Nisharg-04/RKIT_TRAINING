using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegenerateTokenDemo.Models
{
    public class Users
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}