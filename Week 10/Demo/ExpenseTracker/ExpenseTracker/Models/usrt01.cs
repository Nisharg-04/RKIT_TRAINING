using Microsoft.IdentityModel.Tokens;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Models
{
    public class usrt01
    {
        [AutoIncrement]
        [PrimaryKey]
        public int usrf01 { get; set; }

        [Index(Unique = true)]
        public string usrf02 { get; set; }

        public string usrf03 { get; set; }

        public DateTime usrf04 { get; set; }
    }
}